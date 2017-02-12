namespace AddressBook.Application.CommandParameters
{
	public abstract class Result
	{
		public AddressBookCommandResultType ResultType { get; set; }

		public string ErrorMessage { get; set; }
	}

	public enum AddressBookCommandResultType
	{
		Success,
		Error
	}
}
