using AddressBook.Application.CommandParameters;

namespace AddressBook.Application.Services
{
	public interface IAddressBookService
	{
		DeleteAddressBookEntryResult DeleteAddressBookEntry(string addressBookEntryId);
	}
}
