using AddressBook.web.api.Services;

namespace AddressBook.web.api.Controllers.Address
{
    public class AddressCommandsApiController : BaseApiController
	{
		public AddressCommandsApiController(ICurrentUserService currentUserService, IAuthorizationService authorizationService) 
			: base(currentUserService, authorizationService)
		{
		}
	}
}
