using AddressBook.Domain;

namespace AddressBook.Application.Services
{
	public interface IAddressBookRepository
	{
		AddressBookEntry Load(string id);
		void Delete(string id);
	}
}