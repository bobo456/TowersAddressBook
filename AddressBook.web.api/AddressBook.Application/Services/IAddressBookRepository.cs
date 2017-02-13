using AddressBook.Application.CommandParameters;
using AddressBook.Domain;

namespace AddressBook.Application.Services
{
	public interface IAddressBookRepository
	{
		AddressBookEntry Load(string id);
		bool HasDuplicate(AddAddressCommandParameters parameters);
		void Add(AddressBookEntry addressBookEntry);
		void Update(AddressBookEntry addressBookEntry);
		void Delete(string id);
	}
}