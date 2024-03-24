using FluentValidation;
using PersonHandBook.Models.UpdatePerson;

namespace PersonHandBook.Validators
{
	public class PhoneUpdateModelValidator : AbstractValidator<PhoneUpdateModel>
	{
		public PhoneUpdateModelValidator()
		{
			RuleFor(phone => phone.PhoneNumber)
				.NotEmpty()
				.Matches(@"^[0-9]+$").WithMessage("Phone number should contain only digits.");

			RuleFor(phone => phone.PhoneType)
				.IsInEnum().WithName("PhoneType")
				.WithMessage("Invalid {PropertyName} specified.");
		}
	}
}
