using AddressBook.Application.CommandParameters;

namespace AddressBook.Application.Services
{
	public interface IAddressBookService
	{
		DeleteAddressBookEntryResult DeleteAddressBookEntry(string addressBookEntryId);
		AddAddressBookEntryResult AddAddressBookEntry(AddAddressCommandParameters parameters);
		UpdateAddressBookEntryResult UpdateAddressBookEntry(UpdateAddressCommandParameters parameters);
	}
}
