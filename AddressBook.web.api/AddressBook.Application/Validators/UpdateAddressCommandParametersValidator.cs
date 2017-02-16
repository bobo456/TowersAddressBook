using AddressBook.Application.CommandParameters;
using FluentValidation;

namespace AddressBook.Application.Validators
{
	public class UpdateAddressCommandParametersValidator : AbstractValidator<UpdateAddressCommandParameters>
	{
		public UpdateAddressCommandParametersValidator()
		{
			RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name cannot be blank.")
				.Length(2, 50).WithMessage("First Name must between 2 and 50 characters in length.");

			RuleFor(x => x.LastName).NotEmpty().WithMessage("First Name cannot be blank.")
				.Length(2, 50).WithMessage("First Name must between 2 and 50 characters in length.");

			RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be null.");
			RuleFor(x => x).Must(HasRequiredContactInfo).WithMessage("Either a phone number or email address are required.");
		}

		private bool HasRequiredContactInfo(UpdateAddressCommandParameters parameters)
		{
			return !string.IsNullOrEmpty(parameters.HomePhone)
					|| !string.IsNullOrEmpty(parameters.MobilePhone)
					|| !string.IsNullOrEmpty(parameters.Email);
		}
	}
}