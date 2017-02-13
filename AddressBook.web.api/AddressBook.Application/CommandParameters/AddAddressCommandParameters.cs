
namespace AddressBook.Application.CommandParameters
{
	public class AddAddressCommandParameters
	{
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Street1 { get; set; }
		public string Street2 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string HomePhone { get; set; }
		public string MobilePhone { get; set; }
		public string Email { get; set; }
		
		public static bool IsValid(AddAddressCommandParameters parameters)
		{
			// We should not have an Id
			// Required:  First & Last name and either home, mobile or email
			return parameters != null
				&& string.IsNullOrEmpty(parameters.Id) 
				&& !string.IsNullOrEmpty(parameters.FirstName) 
				&& !string.IsNullOrEmpty(parameters.LastName) 
				&& (!string.IsNullOrEmpty(parameters.HomePhone) || !string.IsNullOrEmpty(parameters.MobilePhone) || !string.IsNullOrEmpty(parameters.Email));
		}
	}
}
