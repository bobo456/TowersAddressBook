﻿using System.Collections.Generic;
using System.Linq;
using AddressBook.Application.DTO;
using AddressBook.Application.Services;

namespace AddressBook.Data.Queries
{
	public class AddressBookQueries: IAddressBookQueries
	{
		private readonly ISession _session;

		public AddressBookQueries(ISession session)
		{
			_session = session;
		}

		public List<AddressBookEntryDTO> GetAllAddresses()
		{
			var addressBookEntries = _session.GetAddressBookEntries()
										.Select(a => new AddressBookEntryDTO
													{
															Id = a.Id,
															FirstName = a.FirstName,
															LastName = a.LastName,
															Street1 = a.Street1,
															Street2 = a.Street2,
															City = a.City,
															State = a.State,
															ZipCode = a.ZipCode,
															HomePhone = a.HomePhone,
															MobilePhone = a.MobilePhone,
															Email = a.Email
													})
										.OrderBy(a => a.LastName)
										.ThenBy(a => a.FirstName);

			return addressBookEntries.ToList();
		}
	}
}
