namespace AddressBook.Domain
{
	public class Activity
	{
		public string Id { get; set; }
	}

	public enum ActivityEnum
	{
		View,
		Update,
		Add,
		Delete
	}
}
