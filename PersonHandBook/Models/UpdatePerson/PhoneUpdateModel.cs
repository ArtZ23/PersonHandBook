using PersonHandBook.Helpers.Enums;

namespace PersonHandBook.Models.UpdatePerson
{
	public class PhoneUpdateModel
	{
		public PhoneType? PhoneType { get; set; }
		public string? PhoneNumber { get; set; }
	}
}
