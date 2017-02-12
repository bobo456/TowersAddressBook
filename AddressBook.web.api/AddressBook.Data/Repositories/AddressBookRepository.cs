using System;
using System.Linq;
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

		public void Delete(string id)
		{
			var entry = Load(id);
			if(entry == null)
				throw new ArgumentException();

			_session.GetAddressBookEntries().Remove(entry);
		}
	}
}
