using PersonHandBook.Helpers.Enums;

namespace PersonHandBook.Dtos
{
	public class PersonDto
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public Gender Gender { get; set; }

		public string PersonalNumber { get; set; }
		public DateTime DateOfBirth { get; set; }
		public City City { get; set; }
		public List<PhoneDto> Phones { get; set; } = new List<PhoneDto>();
		public IFormFile Image { get; set; }
		public List<RelatedPersonDto> RelatedPersons { get; set; } = new List<RelatedPersonDto>();
	}
}
