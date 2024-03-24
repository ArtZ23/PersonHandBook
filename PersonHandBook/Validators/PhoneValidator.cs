using FluentValidation;
using PersonHandBook.Dtos;

namespace PersonHandBook.Validators
{
	public class PhoneValidator : AbstractValidator<PhoneDto>
	{
		public PhoneValidator()
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
