using System;

namespace AddressBook.Domain
{
    public class AddressBookEntry
    {
	    protected AddressBookEntry(string id, string firstName, string lastName, string street1, string street2, string city, string state, string zipCode, string homePhone, string mobilePhone, string email)
	    {
		    Id = id;
		    FirstName = firstName;
		    LastName = lastName;
		    Street1 = street1;
		    Street2 = street2;
		    City = city;
		    State = state;
		    ZipCode = zipCode;
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
	    public string ZipCode { get; private set; }
	    public string HomePhone { get; private set; }
	    public string MobilePhone { get; private set; }
	    public string Email { get; private set; }

		public static AddressBookEntry Create(string firstName, string lastName, string street1, string street2, string city,
											string state, string zipcode, string homePhone, string mobilePhone, string email)
		{
			return new AddressBookEntry(
				Guid.NewGuid().ToString(),
				firstName,
				lastName,
				street1,
				street2,
				city,
				state,
				zipcode,
				homePhone,
				mobilePhone,
				email
			);
		}

		public void Update(string firstName, string lastName, string street1, string street2, string city,
		    string state, string zipCode, string homePhone, string mobilePhone, string email)
	    {
		    FirstName = firstName;
		    LastName = lastName;
		    Street1 = street1;
		    Street2 = street2;
		    City = city;
		    State = state;
		    ZipCode = zipCode;
		    HomePhone = homePhone;
		    MobilePhone = mobilePhone;
		    Email = email;
	    }
    }
}
