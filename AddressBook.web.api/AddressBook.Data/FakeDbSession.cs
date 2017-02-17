using System.Collections.Generic;
using AddressBook.Domain;

namespace AddressBook.Data
{
	public class FakeDbSession: ISession
	{
		private List<AddressBookEntry> _addressBookEntries;

		public FakeDbSession()
		{
			_addressBookEntries = InitializeAddressBookEntries();
		}

		public List<AddressBookEntry> InitializeAddressBookEntries()
		{
			var addressBookEntry1 = AddressBookEntry.Create("allen", "iverson", "123 All Star Way", "", "Philadelphia",
				"PA", "84001", "888-123-1234", "123-234-3456", "ai@nba.com");
			var addressBookEntry2 = AddressBookEntry.Create("larry", "brown", "456 Coach Circle", "", "Philadelphia",
				"PA", "84002","866-234-4567", "231-456-7890", "lb@nba.com");
			var addressBookEntry3 = AddressBookEntry.Create("jeremy", "clarkson", "789 Test Track", "", "Salt Lake City",
				"UT", "84003", "877-345-6789", "789-456-1320", "jc@topGear.com");
			var addressBookEntry4 = AddressBookEntry.Create("richard", "hammond", "007 Long Drive", "", "Salt Lake City",
				"UT", "84004", "800-456-7890", "753-951-1478", "rh@grandTour.com");

			return new List<AddressBookEntry> {addressBookEntry1, addressBookEntry2, addressBookEntry3, addressBookEntry4};
		}

		public List<AddressBookEntry> AddressBookEntries {
			get { return _addressBookEntries; }
			set { _addressBookEntries = value; }
		}

		public List<AddressBookEntry> GetAddressBookEntries()
		{
			return _addressBookEntries;
		}
	}
}