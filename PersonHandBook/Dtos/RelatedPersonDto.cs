using PersonHandBook.Enums;

namespace PersonHandBook.Dtos
{
	public class RelatedPersonDto
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public RelationshipType RelationshipType { get; set; }
	}
}
