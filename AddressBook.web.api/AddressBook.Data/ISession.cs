using System.Collections.Generic;
using AddressBook.Domain;

namespace AddressBook.Data
{
	public interface ISession
	{
		List<AddressBookEntry> GetAddressBookEntries();
	}
}
