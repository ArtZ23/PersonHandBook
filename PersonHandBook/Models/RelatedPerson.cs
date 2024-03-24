using PersonHandBook.Helpers.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonHandBook.Models
{
	public class RelatedPerson
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public RelationshipType RelationshipType { get; set; }
		[ForeignKey("Person")]
		public int PersonId { get; set; }
	}
}
