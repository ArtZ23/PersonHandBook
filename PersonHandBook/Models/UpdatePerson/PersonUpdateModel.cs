using PersonHandBook.Enums;
using PersonHandBook.Models.UpdatePerson;

namespace PersonHandBook.Models
{
	public class PersonUpdateModel
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public Gender? Gender { get; set; }

		public string? PersonalNumber { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public City? City { get; set; }
		public List<PhoneUpdateModel>? Phones { get; set; } = new List<PhoneUpdateModel>();
		public IFormFile? Image { get; set; }
		public List<RelatedPersonUpdateModel>? RelatedPersons { get; set; } = new List<RelatedPersonUpdateModel>();
	}
}
