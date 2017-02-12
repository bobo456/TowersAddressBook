using AddressBook.Application.Services;
using AddressBook.web.api.Controllers.Address;
using AddressBook.web.api.tests.WebApi.Controllers;
using Moq;

namespace AddressBook.web.api.tests.Builders
{
	public class AddressQueriesApiControllerBuilder
	{
		private IAddressBookQueries _addressBookQueries;

		public AddressQueriesApiControllerBuilder()
		{
			_addressBookQueries = new Mock<IAddressBookQueries>().Object;
		}

		public AddressQueriesApiControllerBuilder WithAddressBookQueries(IAddressBookQueries addressBookQueries)
		{
			_addressBookQueries = addressBookQueries;
			return this;
		}
		
		public AddressQueriesApiController Build()
		{
			var controller = new AddressQueriesApiController(_addressBookQueries);
			controller.SetupForUnitTest();

			return controller;
		}
	}
}
