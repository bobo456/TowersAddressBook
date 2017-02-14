using System;
using System.Linq;
using AddressBook.Application.CommandParameters;
using AddressBook.Application.Services;
using AddressBook.Domain;

namespace AddressBook.Data.Repositories
{
	public class AddressBookRepository: IAddressBookRepository
	{
		private readonly ISession _session;
		public AddressBookRepository(ISession session)
		{
			_session = session;
		}

		public AddressBookEntry Load(string id)
		{
			return _session.GetAddressBookEntries().FirstOrDefault(e => e.Id == id);
		}

		public bool HasDuplicate(AddAddressCommandParameters parameters)
		{
			return _session.GetAddressBookEntries().Any(e =>
				string.Equals(e.FirstName, parameters.FirstName, StringComparison.CurrentCultureIgnoreCase)
				&& string.Equals(e.LastName, parameters.LastName, StringComparison.CurrentCultureIgnoreCase)
				&& string.Equals(e.Street1, parameters.Street1, StringComparison.CurrentCultureIgnoreCase)
				&& string.Equals(e.Street2, parameters.Street2, StringComparison.CurrentCultureIgnoreCase)
				&& string.Equals(e.City, parameters.City, StringComparison.CurrentCultureIgnoreCase)
				&& string.Equals(e.State, parameters.State, StringComparison.CurrentCultureIgnoreCase)
				&& e.ZipCode == parameters.ZipCode
				&& string.Equals(e.HomePhone, parameters.HomePhone, StringComparison.CurrentCultureIgnoreCase)
				&& string.Equals(e.MobilePhone, parameters.MobilePhone, StringComparison.CurrentCultureIgnoreCase)
				&& string.Equals(e.Email, parameters.Email, StringComparison.CurrentCultureIgnoreCase));
		}

		public void Add(AddressBookEntry addressBookEntry)
		{
			_session.GetAddressBookEntries().Add(addressBookEntry);
		}

		public void Update(AddressBookEntry addressBookEntry)
		{
			var entry = Load(addressBookEntry.Id);
			if (entry == null)
				throw new ArgumentException();

			_session.GetAddressBookEntries().Remove(entry);
			_session.GetAddressBookEntries().Add(addressBookEntry);
		}

		public void Delete(string id)
		{
			var entry = Load(id);
			if(entry == null)
				throw new ArgumentException();

			_session.GetAddressBookEntries().Remove(entry);
		}
	}
}
