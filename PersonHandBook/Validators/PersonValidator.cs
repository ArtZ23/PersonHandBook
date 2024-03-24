using FluentValidation;
using PersonHandBook.Dtos;
using System.Text.RegularExpressions;

namespace PersonHandBook.Validators
{
	public class PersonValidator : AbstractValidator<PersonDto>
	{
		public PersonValidator()
		{
			RuleFor(x => x.FirstName)
				.Length(2, 50)
				.NotEmpty().WithMessage("FirstName cannot be empty")
				.Must(ValidationHelpers.ContainsValidCharacters).WithMessage("Input field must contain either English or Georgian letters");

			RuleFor(x => x.LastName)
				.Length(2, 50)
				.NotEmpty().WithMessage("LastName cannot be empty")
				.Must(ValidationHelpers.ContainsValidCharacters).WithMessage("Input field must contain either English or Georgian letters");

			RuleFor(x => x.Gender)
				.IsInEnum().WithName("Gender")
				.WithMessage("Invalid {PropertyName} specified.")
				.NotEmpty().WithMessage("Gender cannot be empty");

			RuleFor(x => x.PersonalNumber)
				.NotEmpty().WithMessage("PersonalNumber cannot be empty")
				.Length(11).WithMessage("PersonalNumber must contain 11 character");

			RuleFor(x => x.DateOfBirth)
				.NotEmpty().WithMessage("PersonalNumber cannot be empty")
				.Must(IsEligibleForRegistration).WithMessage("You must be over 18 years old");

			RuleFor(x => x.City)
				.IsInEnum().WithName("City")
				.WithMessage("Invalid {PropertyName} specified.")
				.NotEmpty().WithMessage("City cannot be empty");

			RuleForEach(person => person.Phones).SetValidator(new PhoneValidator());
			RuleForEach(person => person.RelatedPersons).SetValidator(new RelatedPersonsValidator());
		}


		private bool IsEligibleForRegistration(DateTime dateOfBirth)
		{
			const int eligibleAge = 18;
			var today = DateTime.Today;
			var age = today.Year - dateOfBirth.Year;
			if (dateOfBirth.Date > today.AddYears(-age))
				age--;

			return age > eligibleAge || (age == eligibleAge && dateOfBirth.Month <= today.Month);
		}
	}
}
