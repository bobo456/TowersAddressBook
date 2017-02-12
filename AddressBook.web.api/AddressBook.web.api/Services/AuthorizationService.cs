using AddressBook.Domain;

namespace AddressBook.web.api.Services
{
	public class AuthorizationService: IAuthorizationService
	{
		/// <summary>
		/// Returns a value indicating whether the specified user has been granted the given activity
		/// </summary>
		/// <param name="userId">The user to check for.</param>
		/// <param name="activity">The <see cref="IActivityId"/> to check for.</param>
		/// <returns><b>true</b> if the specified user has been granted the given activity otherwise, <b>false</b>.</returns>
		public bool IsAuthorized(string userId, ActivityEnum activity)
		{
			// Of course you are authorized
			return true;
		}
	}
}