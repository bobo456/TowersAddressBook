using System;
using AddressBook.Application.CommandParameters;

namespace AddressBook.Application.Services
{
	public class AddressBookService : IAddressBookService
	{
		private readonly Func<IUnitOfWork> _unitOfWorkFactory;

		public AddressBookService(Func<IUnitOfWork> unitOfWorkFactory)
		{
			_unitOfWorkFactory = unitOfWorkFactory;
		}

		public DeleteAddressBookEntryResult DeleteAddressBookEntry(string addressBookEntryId)
		{
			using (var uow = _unitOfWorkFactory.Invoke())
			{
				var addressBookEntry = uow.AddressBookEntries.Load(addressBookEntryId);
				if (addressBookEntry == null)
				{
					return new DeleteAddressBookEntryResult
					{
						ResultType = AddressBookCommandResultType.Error,
						ErrorMessage = "This address book entry has already been deleted."
					};
				}

				uow.AddressBookEntries.Delete(addressBookEntry.Id);
				uow.Commit();

				return new DeleteAddressBookEntryResult { ResultType = AddressBookCommandResultType.Success };
			}
		}
	}
}
