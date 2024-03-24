using PersonHandBook.Helpers.Enums;

namespace PersonHandBook.Dtos
{
	public class RelatedPersonReportDto
	{
		public RelationshipType RelationshipType { get; set; }
		public int Amount { get; set; }
	}
}
