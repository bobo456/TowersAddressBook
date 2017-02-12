namespace AddressBook.web.api.Services
{
	public class CurrentUserService: ICurrentUserService
	{
		public string GetCurrentUserId()
		{
			// normally where to retrieve the user via the request identity from the db
			// returning a fake userId
			return "Users/007";
		}
	}
}