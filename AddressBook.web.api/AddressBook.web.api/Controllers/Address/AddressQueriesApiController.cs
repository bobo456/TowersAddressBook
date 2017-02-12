using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using AddressBook.Application.Services;
using NLog;

namespace AddressBook.web.api.Controllers.Address
{
    public class AddressQueriesApiController : ApiController
    {
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
	    private readonly IAddressBookQueries _addressQueries;

	    public AddressQueriesApiController(IAddressBookQueries addressQueries)
	    {
		    _addressQueries = addressQueries;
	    }

		[EnableCors("*", "*", "*")]
		public HttpResponseMessage GetAllAddresses()
		{
			try
			{
				Logger.Trace("GetAllAddresses started through AddressQueriesApiController");
				var addresses = _addressQueries.GetAllAddresses();
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
