using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AddressBook.Application.CommandParameters;
using AddressBook.Application.Services;
using AddressBook.Domain;
using AddressBook.web.api.Services;
using NLog;
using NLog.Fluent;

namespace AddressBook.web.api.Controllers.Address
{
    public class AddressCommandsApiController : BaseApiController
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
		private readonly IAddressBookService _addressBookService;

		public AddressCommandsApiController(IAddressBookService addressBookService, ICurrentUserService currentUserService, IAuthorizationService authorizationService) 
			: base(currentUserService, authorizationService)
		{
			_addressBookService = addressBookService;
		}

		[AcceptVerbs("POST")]
		public HttpResponseMessage DeleteAddressBookEntry(string addressBookEntryId)
		{
			try
			{
				Logger.Trace("DeleteAddressBookEntry started through Web API for: {0}", addressBookEntryId);

				if (addressBookEntryId == null)
				{
					Logger.Info("DeleteAddressBookEntry called with invalid parameters");
					return Request.CreateResponse(HttpStatusCode.BadRequest, "addressBookEntryId cannot be null");
				}

				var canDeleteAddresses = Authorization.IsAuthorized(UserId, ActivityEnum.Delete);
				if (!canDeleteAddresses)
					return Request.CreateResponse(HttpStatusCode.Forbidden);

				DeleteAddressBookEntryResult result =_addressBookService.DeleteAddressBookEntry(addressBookEntryId);

				Logger.Trace("DeleteAddressBookEntry creating response for: {0}", addressBookEntryId);
				return Request.CreateResponse(HttpStatusCode.OK);
			}
			catch (Exception e)
			{
				Logger.ErrorException("DeleteAddressBookEntry failed", e);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, "Unexpected server error");
			}
		}
	}
}
