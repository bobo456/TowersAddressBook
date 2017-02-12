using System;
using AddressBook.Application.Services;
using Moq;
using NUnit.Framework;

namespace AddressBook.web.api.tests.Builders
{
	public class AddressBookServiceBuilder
	{
		private Mock<IUnitOfWork> _unitOfWork;
		Func<IUnitOfWork> _unitOfWorkFactory;

		[SetUp]
		public void Setup()
		{
			_unitOfWork = new Mock<IUnitOfWork> { DefaultValue = DefaultValue.Mock };
			_unitOfWorkFactory = () => _unitOfWork.Object;
		}
		
		public AddressBookServiceBuilder WithUnitOfWorkFactory(Func<IUnitOfWork> unitOfWorkFactory)
		{
			_unitOfWorkFactory = unitOfWorkFactory;
			return this;
		}

		public AddressBookService Build()
		{
			return new AddressBookService(_unitOfWorkFactory);
		}
	}
}
