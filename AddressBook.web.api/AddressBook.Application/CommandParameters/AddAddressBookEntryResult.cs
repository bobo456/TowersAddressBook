using AddressBook.Application.DTO;

namespace AddressBook.Application.CommandParameters
{
	public class AddAddressBookEntryResult : Result
	{
		public AddressBookEntryDTO NewAddressBookEntry { get; set; }
	}
}
