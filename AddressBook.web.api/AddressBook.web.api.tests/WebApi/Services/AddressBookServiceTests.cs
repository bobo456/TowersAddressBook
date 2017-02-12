using System;
using AddressBook.Application.CommandParameters;
using AddressBook.Application.Services;
using AddressBook.Domain;
using AddressBook.web.api.tests.Builders;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace AddressBook.web.api.tests.WebApi.Services
{
	[TestFixture]
	public class AddressBookServiceTests
	{
		private AddressBookService _addressBookService;
		private Mock<IUnitOfWork> _unitOfWork;
		private Func<IUnitOfWork> _unitOfWorkFactory;

		[SetUp]
		public void Setup()
		{
			_unitOfWork = new Mock<IUnitOfWork> { DefaultValue = DefaultValue.Mock };
			_unitOfWorkFactory = () => _unitOfWork.Object;
			_addressBookService = new AddressBookService(_unitOfWorkFactory);
		}

		[Test]
		public void DeleteAddressBookEntry_ShouldReturnErrorResult_WhenEntryNotFound()
		{
			// Arrange
			const string addressBookEntryId = "AddressBookEntries/123";
			_unitOfWork.Setup(u => u.AddressBookEntries.Load(addressBookEntryId)).Returns((AddressBookEntry) null);
			_addressBookService = new AddressBookServiceBuilder().WithUnitOfWorkFactory(_unitOfWorkFactory).Build();

			// Act
			var result = _addressBookService.DeleteAddressBookEntry(addressBookEntryId);

			// Assert
			result.ResultType.Should().Be(AddressBookCommandResultType.Error);
			result.ErrorMessage.Should().Be("This address book entry has already been deleted.");
			_unitOfWork.Verify(m => m.Commit(), Times.Never);
			_unitOfWork.Verify(m => m.Dispose(), Times.Once);
		}

		[Test]
		public void DeleteAddressBookEntry_ShouldReturnSuccessResult_WhenDeleted()
		{
			// Arrange
			const string addressBookEntryId = "AddressBookEntries/123";
			var addressBookEntry = AddressBookEntry.Create(addressBookEntryId, "ln", "st1", "st2", "c", "st", "hp", "mp", "em");
			_unitOfWork.Setup(u => u.AddressBookEntries.Load(addressBookEntryId)).Returns(addressBookEntry);
			_addressBookService = new AddressBookServiceBuilder().WithUnitOfWorkFactory(_unitOfWorkFactory).Build();

			// Act
			var result = _addressBookService.DeleteAddressBookEntry(addressBookEntryId);
			
			// Assert
			result.ResultType.Should().Be(AddressBookCommandResultType.Success);
			_unitOfWork.Verify(m => m.AddressBookEntries.Delete(addressBookEntry.Id), Times.Once);
			_unitOfWork.Verify(m => m.Commit(), Times.Once);
			_unitOfWork.Verify(m => m.Dispose(), Times.Once);
		}
	}
}
