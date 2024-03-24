using System.Text.RegularExpressions;

namespace PersonHandBook.Validators
{
	public class ValidationHelpers
	{
		public static bool ContainsValidCharacters(string input) =>
				// Check if input contains only English or Georgian letters
				// and they should not be together
				Regex.IsMatch(input, @"^([a-zA-Z]*|[\u10D0-\u10FA]*)$");
	}
}
