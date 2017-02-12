using System;

namespace AddressBook.Domain
{
    public class AddressBookEntry
    {
	    public static AddressBookEntry Create(string firstName, string lastName, string street1, string street2, string city, 
											string state, string homePhone, string mobilePhone, string email)
	    {
			return new AddressBookEntry
			{
				Id = Guid.NewGuid().ToString(),
				FirstName = firstName,
				LastName = lastName,
				Street1 = street1,
				Street2 = street2,
				City = city,
				State = state,
				HomePhone = homePhone,
				MobilePhone = mobilePhone,
				Email = email
			};
	    }

	    public void Update(string firstName, string lastName, string street1, string street2, string city,
											string state, string homePhone, string mobilePhone, string email)
	    {
		    FirstName = firstName;
		    LastName = lastName;
		    Street1 = street1;
		    Street2 = street2;
		    City = city;
		    State = state;
		    HomePhone = homePhone;
		    MobilePhone = mobilePhone;
		    Email = email;
	    }

	    public string Id { get; private set; }
	    public string FirstName { get; private set; }
		public string LastName { get; private set; }
		public string Street1 { get; private set; }
		public string Street2 { get; private set; }
		public string City { get; private set; }
		public string State { get; private set; }
		public string HomePhone { get; private set; }
		public string MobilePhone { get; private set; }
		public string Email { get; private set; }
	}
}
