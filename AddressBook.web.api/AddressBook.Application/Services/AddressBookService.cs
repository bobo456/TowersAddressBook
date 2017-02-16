using System;
using AddressBook.Application.CommandParameters;
using AddressBook.Application.DTO;
using AddressBook.Domain;

namespace AddressBook.Application.Services
{
	public class AddressBookService : IAddressBookService
	{
		private readonly Func<IUnitOfWork> _unitOfWorkFactory;

		public AddressBookService(Func<IUnitOfWork> unitOfWorkFactory)
		{
			_unitOfWorkFactory = unitOfWorkFactory;
		}

		public AddAddressBookEntryResult AddAddressBookEntry(AddAddressCommandParameters parameters)
		{
			using (var uow = _unitOfWorkFactory.Invoke())
			{
				var newEntry = AddressBookEntry.Create(parameters.FirstName, parameters.LastName, parameters.Street1, parameters.Street2, 
										parameters.City, parameters.State, parameters.ZipCode, parameters.HomePhone, parameters.MobilePhone, parameters.Email);

				if(uow.AddressBookEntries.HasDuplicate(parameters))
					return new AddAddressBookEntryResult { ResultType = AddressBookCommandResultType.Duplicate, Error = "Duplicate found."};

				uow.AddressBookEntries.Add(newEntry);
				uow.Commit();

				var newEntryDto = new AddressBookEntryDTO
									{
										Id = newEntry.Id,
										FirstName = newEntry.FirstName,
										LastName = newEntry.LastName,
										Street1 = newEntry.Street1,
										Street2 = newEntry.Street2,
										City = newEntry.City,
										State = newEntry.State,
										ZipCode = newEntry.ZipCode,
										HomePhone = newEntry.HomePhone,
										MobilePhone = newEntry.MobilePhone,
										Email = newEntry.Email
									};

				return new AddAddressBookEntryResult { ResultType = AddressBookCommandResultType.Success, NewAddressBookEntry = newEntryDto };
			}
		}

		public UpdateAddressBookEntryResult UpdateAddressBookEntry(UpdateAddressCommandParameters parameters)
		{
			using (var uow = _unitOfWorkFactory.Invoke())
			{
				var addressBookEntry = uow.AddressBookEntries.Load(parameters.Id);
				if (addressBookEntry == null)
				{
					return new UpdateAddressBookEntryResult
					{
						ResultType = AddressBookCommandResultType.Error,
						Error = "This address book entry was not found."
					};
				}

				addressBookEntry.Update(parameters.FirstName, parameters.LastName, parameters.Street1, parameters.Street2, parameters.City, 
										parameters.State, parameters.ZipCode, parameters.HomePhone, parameters.MobilePhone, parameters.Email);

				uow.AddressBookEntries.Update(addressBookEntry);
				uow.Commit();

				return new UpdateAddressBookEntryResult { ResultType = AddressBookCommandResultType.Success };
			}
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
						Error = "This address book entry has already been deleted."
					};
				}

				uow.AddressBookEntries.Delete(addressBookEntry.Id);
				uow.Commit();

				return new DeleteAddressBookEntryResult { ResultType = AddressBookCommandResultType.Success };
			}
		}
	}
}
