
using AddressBook.Application.Validators;
using FluentValidation.Attributes;

namespace AddressBook.Application.CommandParameters
{
	[Validator(typeof(AddAddressCommandParametersValidator))]
	public class AddAddressCommandParameters
	{
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Street1 { get; set; }
		public string Street2 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string ZipCode { get; set; }
		public string HomePhone { get; set; }
		public string MobilePhone { get; set; }
		public string Email { get; set; }
	}
}
