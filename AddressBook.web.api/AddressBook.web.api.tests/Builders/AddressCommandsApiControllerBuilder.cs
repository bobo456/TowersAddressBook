using AddressBook.Application.Services;
using AddressBook.Domain;
using AddressBook.web.api.Controllers.Address;
using AddressBook.web.api.Services;
using AddressBook.web.api.tests.WebApi.Controllers;
using Moq;

namespace AddressBook.web.api.tests.Builders
{
	public class AddressCommandsApiControllerBuilder
	{
		private ICurrentUserService _currentUserService;
		private IAuthorizationService _authorizationService;
		private IAddressBookService _addressBookService;

		public AddressCommandsApiControllerBuilder()
		{
			_addressBookService = new Mock<IAddressBookService>().Object;
			_authorizationService = new Mock<IAuthorizationService>().Object;
			_currentUserService = new Mock<ICurrentUserService>().Object;
		}

		public AddressCommandsApiControllerBuilder WithAddressBookService(IAddressBookService addressBookService)
		{
			_addressBookService = addressBookService;
			return this;
		}

		public AddressCommandsApiControllerBuilder WithAuthorizationService(IAuthorizationService authorizationService)
		{
			_authorizationService = authorizationService;
			return this;
		}

		public AddressCommandsApiControllerBuilder WithAllAuthority()
		{
			var mockAuthorizationService = new Mock<IAuthorizationService>();
			mockAuthorizationService.Setup(x => x.IsAuthorized(It.IsAny<string>(), It.IsAny<ActivityEnum>())).Returns(true);
			return WithAuthorizationService(mockAuthorizationService.Object);
		}

		public AddressCommandsApiControllerBuilder WithCurrentUserService(ICurrentUserService currentUserService)
		{
			_currentUserService = currentUserService;
			return this;
		}

		public AddressCommandsApiControllerBuilder WithUser(string userId)
		{
			var mockUserService = new Mock<ICurrentUserService>();
			mockUserService.Setup(x => x.GetCurrentUserId()).Returns(userId);
			return WithCurrentUserService(mockUserService.Object);
		}
		
		public AddressCommandsApiController Build()
		{
			var controller = new AddressCommandsApiController(_addressBookService, _currentUserService, _authorizationService);
			controller.SetupForUnitTest();

			return controller;
		}
	}
}
