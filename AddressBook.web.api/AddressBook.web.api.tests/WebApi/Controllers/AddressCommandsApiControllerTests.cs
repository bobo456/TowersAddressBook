using System;
using System.Net;
using AddressBook.Application.CommandParameters;
using AddressBook.Application.Services;
using AddressBook.Domain;
using AddressBook.web.api.Controllers.Address;
using AddressBook.web.api.Services;
using AddressBook.web.api.tests.Builders;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace AddressBook.web.api.tests.WebApi.Controllers
{
	[TestFixture]
	public class AddressCommandsApiControllerTests
	{
		private Mock<IAddressBookService> _mockAddressBookService;
		private Mock<IAuthorizationService> _mockAuthorizationService;
		private Mock<ICurrentUserService> _mockCurrentUserService;
		private AddressCommandsApiController _addressCommandsApiController;
		private AddressCommandsApiControllerBuilder _addressCommandsApiControllerBuilder;

		[SetUp]
		public void Setup()
		{
			_mockAddressBookService = new Mock<IAddressBookService>(MockBehavior.Strict);
			_mockAuthorizationService = new Mock<IAuthorizationService>(MockBehavior.Strict);
			_mockCurrentUserService = new Mock<ICurrentUserService>(MockBehavior.Strict);
			_addressCommandsApiControllerBuilder = new AddressCommandsApiControllerBuilder();
		}

		[Test]
		public void DeleteAddressBookEntry_ShouldReturn400BadRequest_WhenEntryNotFound()
		{
			// Arrange
			_addressCommandsApiController = _addressCommandsApiControllerBuilder.Build();

			// Act
			var response = _addressCommandsApiController.DeleteAddressBookEntry(null);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
		}

		[Test]
		public void DeleteAddressBookEntry_ShouldReturn403Forbidden_WhenUserIsNotFound()
		{
			// Arrange
			_mockCurrentUserService.Setup(cus => cus.GetCurrentUserId()).Returns((string)null);
			_addressCommandsApiController = _addressCommandsApiControllerBuilder.WithCurrentUserService(_mockCurrentUserService.Object).Build();

			// Act
			var response = _addressCommandsApiController.DeleteAddressBookEntry("AddressBookEntries/123");

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
		}

		[Test]
		public void DeleteAddressBookEntry_ShouldReturn403Forbidden_WhenUserIsNotAuthorized()
		{
			// Arrange
			var userId = "Users/007";
			_mockCurrentUserService.Setup(cus => cus.GetCurrentUserId()).Returns(userId);
			_mockAuthorizationService.Setup(a => a.IsAuthorized(userId, ActivityEnum.View)).Returns(false);
			_addressCommandsApiController = _addressCommandsApiControllerBuilder.WithCurrentUserService(_mockCurrentUserService.Object).Build();

			// Act
			var response = _addressCommandsApiController.DeleteAddressBookEntry("AddressBookEntries/123");

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
		}

		[Test]
		public void DeleteAddressBookEntry_ShouldReturn500Error_WhenUnhandledExceptionOccurs()
		{
			// Arrange
			const string addressBookEntryId = "AddressBookEntries/123";
			_mockAddressBookService.Setup(ab => ab.DeleteAddressBookEntry(addressBookEntryId)).Throws<NotImplementedException>();
			_addressCommandsApiController = _addressCommandsApiControllerBuilder
												.WithAddressBookService(_mockAddressBookService.Object)
												.WithUser("Users/007")
												.WithAllAuthority()
												.Build();

			// Act
			var response = _addressCommandsApiController.DeleteAddressBookEntry(addressBookEntryId);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
		}


		[Test]
		public void DeleteAddressBookEntry_ShouldReturn200Ok_WhenDeleteResultIsSuccess()
		{
			// Arrange
			const string addressBookEntryId = "AddressBookEntries/123";
			var deleteAddressBookEntryResult = new DeleteAddressBookEntryResult {ResultType = AddressBookCommandResultType.Success};
			_mockAddressBookService.Setup(ab => ab.DeleteAddressBookEntry(addressBookEntryId)).Returns(deleteAddressBookEntryResult);
			_addressCommandsApiController = _addressCommandsApiControllerBuilder
												.WithAddressBookService(_mockAddressBookService.Object)
												.WithUser("Users/007")
												.WithAllAuthority()
												.Build();

			// Act
			var response = _addressCommandsApiController.DeleteAddressBookEntry(addressBookEntryId);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.OK);
		}
	}
}
