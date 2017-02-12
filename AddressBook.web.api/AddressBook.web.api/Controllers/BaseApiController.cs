using System.Web.Http;
using AddressBook.web.api.Services;

namespace AddressBook.web.api.Controllers
{
	public class BaseApiController: ApiController
	{
		private readonly ICurrentUserService mCurrentUserService;

		public BaseApiController(ICurrentUserService currentUserService, IAuthorizationService authorizationService)
		{
			mCurrentUserService = currentUserService;
			Authorization = authorizationService;
		}

		protected string UserId
		{
			get { return mCurrentUserService.GetCurrentUserId(); }
		}

		protected IAuthorizationService Authorization { get; }
	}
}
