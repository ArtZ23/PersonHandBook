using FluentValidation;
using PersonHandBook.Dtos;
using PersonHandBook.Models;

namespace PersonHandBook.Validators
{
	public class RelatedPersonsValidator : AbstractValidator<RelatedPersonDto>
	{
		public RelatedPersonsValidator()
		{
			RuleFor(x => x.FirstName)
				.Length(2, 50)
				.NotEmpty().WithMessage("FirstName cannot be empty")
				.Must(ValidationHelpers.ContainsValidCharacters).WithMessage("Input field must contain either English or Georgian letters");

			RuleFor(x => x.LastName)
				.Length(2, 50)
				.NotEmpty().WithMessage("LastName cannot be empty")
				.Must(ValidationHelpers.ContainsValidCharacters).WithMessage("Input field must contain either English or Georgian letters");

			RuleFor(phone => phone.RelationshipType)
				.IsInEnum().WithName("RelationshipType")
				.WithMessage("Invalid {PropertyName} specified.");
		}
	}
}
