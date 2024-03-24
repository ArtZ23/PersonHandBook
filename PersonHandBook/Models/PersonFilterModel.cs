namespace PersonHandBook.Models
{
	public class PersonFilterModel
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? PersonalNumber { get; set; }
		public int PageNumber { get; set; } = 1;
		public int PageSize { get; set; } = 10;

	}
}
