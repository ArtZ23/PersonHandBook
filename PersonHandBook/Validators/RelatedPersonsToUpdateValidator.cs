using FluentValidation;
using PersonHandBook.Models.UpdatePerson;

namespace PersonHandBook.Validators
{
	public class RelatedPersonUpdateModelValidator : AbstractValidator<RelatedPersonUpdateModel>
	{
		public RelatedPersonUpdateModelValidator()
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
