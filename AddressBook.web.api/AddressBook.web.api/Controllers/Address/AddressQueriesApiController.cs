using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Cors;
using AddressBook.Application.Services;
using AddressBook.Domain;
using AddressBook.web.api.Services;
using NLog;

namespace AddressBook.web.api.Controllers.Address
{
    public class AddressQueriesApiController : BaseApiController
    {
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
	    private readonly IAddressBookQueries _addressQueries;

	    public AddressQueriesApiController(IAddressBookQueries addressQueries, ICurrentUserService currentUserService, IAuthorizationService authorizationService) 
			: base(currentUserService, authorizationService)
	    {
		    _addressQueries = addressQueries;
	    }

		[EnableCors("*", "*", "*")]
		public HttpResponseMessage GetAllAddresses()
		{
			try
			{
				Logger.Trace("GetAllAddresses started through AddressQueriesApiController");

				var canViewAddresses = Authorization.IsAuthorized(UserId, ActivityEnum.View);
				if (!canViewAddresses)
					return Request.CreateResponse(HttpStatusCode.Forbidden);

				var addresses = _addressQueries.GetAllAddresses();

				Logger.Trace("GetAllAddresses creating response object");
				return Request.CreateResponse(HttpStatusCode.OK, addresses);
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "GetAllAddresses failed and threw an exception");
				return Request.CreateResponse(HttpStatusCode.InternalServerError);
			}
		}
	}


}
