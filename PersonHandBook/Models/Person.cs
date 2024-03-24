using PersonHandBook.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace PersonHandBook.Models
{
	public class Person
	{
		[Key]
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public Gender Gender { get; set; }

		public string PersonalNumber { get; set; }
		public DateTime DateOfBirth { get; set; }
		public City City { get; set; }
		public List<Phone> Phones { get; set; } = new List<Phone>();
		public string ImagePath { get; set; }
		public List<RelatedPerson> RelatedPersons { get; set; } = new List<RelatedPerson>();
	}
}
