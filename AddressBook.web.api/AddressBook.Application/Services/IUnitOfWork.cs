using System;

namespace AddressBook.Application.Services
{
	/// <summary>
	/// Allows transaction scope to be controlled explicitly.
	/// </summary>
	public interface IUnitOfWork : IDisposable
	{
		/// <summary>
		/// Commits any pending changes to the underlying repositories.
		/// </summary>
		void Commit();

		IAddressBookRepository AddressBookEntries { get; }
	}
}
