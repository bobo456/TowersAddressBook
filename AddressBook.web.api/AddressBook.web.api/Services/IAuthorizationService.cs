using AddressBook.Domain;

namespace AddressBook.web.api.Services
{
	public interface IAuthorizationService
	{
		bool IsAuthorized(string userId, ActivityEnum activity);
	}
}