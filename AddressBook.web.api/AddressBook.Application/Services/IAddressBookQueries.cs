using System.Collections.Generic;
using AddressBook.Application.DTO;

namespace AddressBook.Application.Services
{
	public interface IAddressBookQueries
	{
		List<AddressBookEntryDTO> GetAllAddresses();
	}
}
