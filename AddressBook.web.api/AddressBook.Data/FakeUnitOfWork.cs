using System;
using AddressBook.Application.Services;
using AddressBook.Data.Repositories;

namespace AddressBook.Data
{
	public class FakeUnitOfWork: IUnitOfWork
	{
		private readonly ISession _session;
		private IAddressBookRepository _addressBookRepository;
		private bool _committed;

		public FakeUnitOfWork(ISession session)
		{
			_session = session;
		}

		public IAddressBookRepository AddressBookEntries
		{
			get { return _addressBookRepository ?? (_addressBookRepository = new AddressBookRepository(_session)); }
		}

		public void Commit()
		{
			try
			{
				_committed = true;
				Save();
			}
			catch (Exception)
			{
				throw new InvalidOperationException();
			}
		}

		private void Save()
		{
			// Normally where the save to DB would occur
		}
		
		public void Dispose()
		{
			if (!_committed)
			{
				// rollback/reset db
			}

			// dispose db connection
		}
	}
}
