using System.Collections.Generic;

namespace AddressBook.Application.CommandParameters
{
	public abstract class Result
	{
		public AddressBookCommandResultType ResultType { get; set; }
		public List<string> ValidationErrors { get; set; }
		public string Error { get; set; }
	}

	public enum AddressBookCommandResultType
	{
		Success,
		Error,
		Duplicate
	}
}
