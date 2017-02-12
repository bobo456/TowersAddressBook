using AddressBook.Application.Services;
using AddressBook.Domain;
using AddressBook.web.api.Controllers.Address;
using AddressBook.web.api.Services;
using AddressBook.web.api.tests.WebApi.Controllers;
using Moq;

namespace AddressBook.web.api.tests.Builders
{
	public class AddressQueriesApiControllerBuilder
	{
		private IAddressBookQueries _addressBookQueries;
		private ICurrentUserService _currentUserService;
		private IAuthorizationService _authorizationService;

		public AddressQueriesApiControllerBuilder()
		{
			_addressBookQueries = new Mock<IAddressBookQueries>().Object;
			_authorizationService = new Mock<IAuthorizationService>().Object;
			_currentUserService = new Mock<ICurrentUserService>().Object;
		}

		public AddressQueriesApiControllerBuilder WithAddressBookQueries(IAddressBookQueries addressBookQueries)
		{
			_addressBookQueries = addressBookQueries;
			return this;
		}

		public AddressQueriesApiControllerBuilder WithAuthorizationService(IAuthorizationService authorizationService)
		{
			_authorizationService = authorizationService;
			return this;
		}

		public AddressQueriesApiControllerBuilder WithAllAuthority()
		{
			var mockAuthorizationService = new Mock<IAuthorizationService>();
			mockAuthorizationService.Setup(x => x.IsAuthorized(It.IsAny<string>(), It.IsAny<ActivityEnum>())).Returns(true);
			return WithAuthorizationService(mockAuthorizationService.Object);
		}

		public AddressQueriesApiControllerBuilder WithCurrentUserService(ICurrentUserService currentUserService)
		{
			_currentUserService = currentUserService;
			return this;
		}

		public AddressQueriesApiControllerBuilder WithUser(string userId)
		{
			var mockUserService = new Mock<ICurrentUserService>();
			mockUserService.Setup(x => x.GetCurrentUserId()).Returns(userId);
			return WithCurrentUserService(mockUserService.Object);
		}

		public AddressQueriesApiController Build()
		{
			var controller = new AddressQueriesApiController(_addressBookQueries, _currentUserService, _authorizationService);
			controller.SetupForUnitTest();

			return controller;
		}
	}
}
