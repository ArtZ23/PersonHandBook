using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonHandBook.Dtos;
using PersonHandBook.Helpers.Enums;
using PersonHandBook.Models;

namespace PersonHandBook.Services
{

	public interface IPersonService
	{
		Task<PersonDto> GetPerson(int personId);
		Task<List<PersonDto>> FilterPersonsAsync(PersonFilterModel personFilter);
		Task<List<RelatedPersonReportDto>> GetRelatedPersonReport(int personId);
		Task AddPerson(PersonDto persondto);
		Task UpdatePerson(int personId, PersonUpdateModel persondto);
		Task DeletePerson(int personId);
		Task DeletePhone(int phoneId);
		Task DeleteRelatedPerson(int relatedPersonId);
	}
	public class PersonService : IPersonService
	{
		private readonly PersonContext _context;
		private readonly IMapper _mapper;
		public PersonService(PersonContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task<PersonDto> GetPerson(int personId)
		{
			var person = await _context.Persons
				.Where(x => x.Id == personId)
				.Include(x => x.Phones)
				.Include(x => x.RelatedPersons)
				.FirstOrDefaultAsync();
			var persondto = _mapper.Map<PersonDto>(person);
			return persondto;
		}

		public async Task<List<PersonDto>> FilterPersonsAsync(PersonFilterModel personFilter)
		{
			var skip = (personFilter.PageNumber - 1) * personFilter.PageSize;

			var query = await _context.Persons
			.Include(x => x.Phones)
			.Include(x => x.RelatedPersons)
			.Where(x =>
				(string.IsNullOrEmpty(personFilter.FirstName) || EF.Functions.Like(x.FirstName, $"{personFilter.FirstName}%")) &&
				(string.IsNullOrEmpty(personFilter.LastName) || EF.Functions.Like(x.LastName, $"{personFilter.LastName}%")) &&
				(string.IsNullOrEmpty(personFilter.PersonalNumber) || EF.Functions.Like(x.PersonalNumber, $"{personFilter.PersonalNumber}%")))
			.Skip(skip)
			.Take(personFilter.PageSize)
			.ToListAsync();

			var personsdto = _mapper.Map<List<PersonDto>>(query);
			return personsdto;
		}

		public async Task<List<RelatedPersonReportDto>> GetRelatedPersonReport(int personId)
		{
			var relatedPersonsCount = await _context.Persons
			.Where(x => x.Id == personId)
			.SelectMany(p => p.RelatedPersons)
			.GroupBy(rp => rp.RelationshipType)
				.Select(g => new RelatedPersonReportDto
				{
					RelationshipType = g.Key,
					Amount = g.Count()
				})
			.ToListAsync();

			return relatedPersonsCount;
		}

		public async Task AddPerson(PersonDto persondto)
		{
			var person = _mapper.Map<Person>(persondto);
			var imagePath = await SaveImageAsync(persondto.Image);
			person.ImagePath = imagePath;
			await _context.Persons.AddAsync(person);
			await _context.SaveChangesAsync();
		}

		public async Task UpdatePerson(int personId, PersonUpdateModel persondto)
		{
			var existingPerson = _context.Persons
				.Include(p => p.Phones)
				.Include(p => p.RelatedPersons)
				.FirstOrDefault(p => p.Id == personId);

			if (existingPerson is null)
			{
				throw new ArgumentException($"Person with ID {personId} not found.");
			}

			updatePersonProperties(persondto, existingPerson);
			await UpdatePersonImage(persondto, existingPerson);

			_context.Persons.Update(existingPerson);
			await _context.SaveChangesAsync();
		}

		public async Task DeletePerson(int personId)
		{
			var person = _context.Persons.FirstOrDefault(x => x.Id == personId);

			_context.Persons.Remove(person);
			await _context.SaveChangesAsync();
		}

		public async Task DeletePhone(int phoneId)
		{
			var phone = _context.Phones.FirstOrDefault(x => x.Id == phoneId);

			_context.Phones.Remove(phone);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteRelatedPerson(int relatedPersonId)
		{
			var relatedPerson = _context.RelatedPersons.FirstOrDefault(x => x.Id == relatedPersonId);

			_context.RelatedPersons.Remove(relatedPerson);
			await _context.SaveChangesAsync();
		}

		private void updatePersonProperties(PersonUpdateModel persondto, Person existingPerson)
		{
			existingPerson.FirstName = persondto.FirstName ?? existingPerson.FirstName;
			existingPerson.LastName = persondto.LastName ?? existingPerson.LastName;
			existingPerson.City = persondto.City ?? existingPerson.City;
			existingPerson.PersonalNumber = persondto.PersonalNumber ?? existingPerson.PersonalNumber;

			if (persondto.Gender is not null)
			{
				existingPerson.Gender = (Gender)persondto.Gender;
			}

			if (persondto.DateOfBirth is not null)
			{
				existingPerson.DateOfBirth = (DateTime)persondto.DateOfBirth;
			}


			if (persondto.Phones is not null)
			{
				if (existingPerson.Phones is not null)
				{
					existingPerson.Phones.Clear();
					var phone = _context.Phones.Where(x => x.PersonId == existingPerson.Id).ToList();
					_context.Phones.RemoveRange(phone);
				}

				var phoneList = persondto.Phones
					.Select(phone => new Phone
					{
						PhoneNumber = phone.PhoneNumber,
						PhoneType = (PhoneType)phone.PhoneType
					}).ToList();

				existingPerson.Phones.AddRange(phoneList);
			}

			if (persondto.RelatedPersons is not null)
			{
				if (existingPerson.Phones is not null)
				{
					existingPerson.RelatedPersons.Clear();
					var relatedPersons = _context.RelatedPersons.Where(x => x.PersonId == existingPerson.Id).ToList();
					_context.RelatedPersons.RemoveRange(relatedPersons);
				}

				var relatedPersonsList = persondto.RelatedPersons
					.Select(relatedPerson => new RelatedPerson
					{
						FirstName = relatedPerson?.FirstName,
						LastName = relatedPerson?.LastName,
						RelationshipType = (RelationshipType)relatedPerson?.RelationshipType
					}).ToList();

				existingPerson.RelatedPersons.AddRange(relatedPersonsList);
			}
		}

		private async Task UpdatePersonImage(PersonUpdateModel persondto, Person existingPerson)
		{
			if (persondto.Image is not null)
			{
				DeleteExistingImage(existingPerson);
				var imagePath = await SaveImageAsync(persondto.Image);
				existingPerson.ImagePath = imagePath;
			}
		}

		private static void DeleteExistingImage(Person? personToUpdate)
		{
			if (File.Exists(personToUpdate?.ImagePath))
			{
				File.Delete(personToUpdate.ImagePath);
			}
		}

		private async Task<string> SaveImageAsync(IFormFile image)
		{
			var extention = image.ContentType.Split('/')[1];
			var fileName = Path.GetFileName(image.FileName);
			var fileNameToSave = $"{fileName}_{Guid.NewGuid()}.{extention}";
			var imagePath = Path.Combine("Uploads", fileNameToSave);

			using (var stream = new FileStream(imagePath, FileMode.Create))
			{
				await image.CopyToAsync(stream);
			}

			return imagePath;
		}
	}
}
