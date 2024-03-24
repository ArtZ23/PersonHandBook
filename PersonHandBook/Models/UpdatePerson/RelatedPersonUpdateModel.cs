using PersonHandBook.Enums;

namespace PersonHandBook.Models.UpdatePerson
{
	public class RelatedPersonUpdateModel
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public RelationshipType? RelationshipType { get; set; }
	}
}
