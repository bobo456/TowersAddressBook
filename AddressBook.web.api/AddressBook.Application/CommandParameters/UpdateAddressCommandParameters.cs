
namespace AddressBook.Application.CommandParameters
{
	public class UpdateAddressCommandParameters
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

		public static bool IsValid(UpdateAddressCommandParameters parameters)
		{
			// Id should not be empty, everything else can be
			return !string.IsNullOrEmpty(parameters?.Id);
		}
	}
}
