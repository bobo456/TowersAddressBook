using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using AddressBook.Application.DTO;
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
	public class AddressQueriesApiControllerTests
    {
		private Mock<IAddressBookQueries> _mockAddressBookQueries;
		private Mock<IAuthorizationService> _mockAuthorizationService;
		private Mock<ICurrentUserService> _mockCurrentUserService;
	    private AddressQueriesApiController _addressQueriesApiController;
		private AddressQueriesApiControllerBuilder _addressQueriesApiControllerBuilder;

	    [SetUp]
		public void Setup()
		{
			_mockAddressBookQueries = new Mock<IAddressBookQueries>(MockBehavior.Strict);
			_mockAuthorizationService = new Mock<IAuthorizationService>(MockBehavior.Strict);
			_mockCurrentUserService = new Mock<ICurrentUserService>(MockBehavior.Strict);
			_addressQueriesApiControllerBuilder = new AddressQueriesApiControllerBuilder();
		}

		[Test]
	    public void GetAllAddresses_ShouldReturn403Forbidden_WhenUserIsNotFound()
	    {
			// Arrange
		    _mockCurrentUserService.Setup(cus => cus.GetCurrentUserId()).Returns((string) null);
			_addressQueriesApiController = _addressQueriesApiControllerBuilder.WithCurrentUserService(_mockCurrentUserService.Object).Build();

			// Act
			var response = _addressQueriesApiController.GetAllAddresses();

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
		}

		[Test]
	    public void GetAllAddresses_ShouldReturn403Forbidden_WhenUserIsNotAuthorized()
	    {
			// Arrange
		    var userId = "Users/007";
		    _mockCurrentUserService.Setup(cus => cus.GetCurrentUserId()).Returns(userId);
		    _mockAuthorizationService.Setup(a => a.IsAuthorized(userId, ActivityEnum.View)).Returns(false);
			_addressQueriesApiController = _addressQueriesApiControllerBuilder.WithCurrentUserService(_mockCurrentUserService.Object).Build();

			// Act
			var response = _addressQueriesApiController.GetAllAddresses();

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
		}

		[Test]
	    public void GetAllAddresses_ShouldReturn500Error_WhenUnhandledExceptionOccurs()
	    {
			// Arrange
		    _mockAddressBookQueries.Setup(ab => ab.GetAllAddresses()).Throws<NotImplementedException>();
			_addressQueriesApiController = _addressQueriesApiControllerBuilder
												.WithAddressBookQueries(_mockAddressBookQueries.Object)
												.WithUser("Users/007")
												.WithAllAuthority()
												.Build();

			// Act
			var response = _addressQueriesApiController.GetAllAddresses();

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
		}

		[Test]
	    public void GetAllAddresses_ShouldReturn200Ok_WhenAddressesReturned()
	    {
			// Arrange
		    var addressBookEntryDtoList = new List<AddressBookEntryDTO>
		    {
			    new AddressBookEntryDTO
			    {
				    Street1 = "s1",
				    Street2 = "s2",
				    City = "c1",
				    State = "st1",
				    Email = "em1",
				    MobilePhone = "m1",
				    HomePhone = "h1",
				    FirstName = "fn1",
				    LastName = "ln1",
				    Id = Guid.NewGuid().ToString()
			    },
				new AddressBookEntryDTO
				{
					Street1 = "s3",
					Street2 = "s4",
					City = "c2",
					State = "st2",
					Email = "em2",
					MobilePhone = "m2",
					HomePhone = "h2",
					FirstName = "fn2",
					LastName = "ln2",
					Id = Guid.NewGuid().ToString()
				}
			};

		    _mockAddressBookQueries.Setup(cus => cus.GetAllAddresses()).Returns(addressBookEntryDtoList);
			_addressQueriesApiController = _addressQueriesApiControllerBuilder
											.WithUser("Users/007")
											.WithAllAuthority()
											.WithAddressBookQueries(_mockAddressBookQueries.Object).Build();

			// Act
			var response = _addressQueriesApiController.GetAllAddresses();

			// Assert
			response.Should().NotBeNull();
			response.StatusCode.Should().Be(HttpStatusCode.OK);
			var addressBookEntries = response.Content
				.ReadAsAsync<List<AddressBookEntryDTO>>()
				.Result;

			addressBookEntries.Count.Should().Be(2);
			addressBookEntries.ShouldBeEquivalentTo(addressBookEntryDtoList);
		}
    }
}
