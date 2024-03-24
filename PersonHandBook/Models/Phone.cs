using PersonHandBook.Helpers.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonHandBook.Models
{
	public class Phone
	{
		public int Id { get; set; }
		public PhoneType PhoneType { get; set; }
		public string PhoneNumber { get; set; }

		[ForeignKey("Person")]
		public int PersonId { get; set; }
		public Person Person { get; set; }
	}
}
