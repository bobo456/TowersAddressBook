using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
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
		[EnableCors("*", "*", "*")]
		public HttpResponseMessage AddAddressBookEntry(AddAddressCommandParameters parameters)
		{
			try
			{
				Logger.Trace("AddAddressBookEntry started through Web API");

				if (parameters == null || !AddAddressCommandParameters.IsValid(parameters))
				{
					Logger.Info("AddAddressBookEntry called with invalid parameters");
					return Request.CreateResponse(HttpStatusCode.BadRequest, "Parameters were invalid.");
				}

				var canAddAddresses = Authorization.IsAuthorized(UserId, ActivityEnum.Add);
				if (!canAddAddresses)
					return Request.CreateResponse(HttpStatusCode.Forbidden);

				var result =_addressBookService.AddAddressBookEntry(parameters);
				switch(result.ResultType)
				{ 
					case AddressBookCommandResultType.Duplicate:
						Logger.Trace(result.ErrorMessage);
						return Request.CreateResponse(HttpStatusCode.BadRequest, "Duplicate address book entry found.");
					case AddressBookCommandResultType.Error:
						Logger.Error("AddAddressBookEntry failed with error: {0}", result.ErrorMessage);
						return Request.CreateResponse(HttpStatusCode.InternalServerError, "Unexpected server error");
				}

				Logger.Trace("AddAddressBookEntry creating response");
				return Request.CreateResponse(HttpStatusCode.OK);
			}
			catch (Exception e)
			{
				Logger.ErrorException("AddAddressBookEntry failed", e);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, "Unexpected server error");
			}
		}

		[AcceptVerbs("PUT")]
		[EnableCors("*", "*", "*")]
		public HttpResponseMessage UpdateAddressBookEntry(UpdateAddressCommandParameters parameters)
		{
			try
			{
				Logger.Trace("UpdateAddressBookEntry started through Web API");

				if (parameters == null || !UpdateAddressCommandParameters.IsValid(parameters))
				{
					Logger.Info("UpdateAddressBookEntry called with invalid parameters");
					return Request.CreateResponse(HttpStatusCode.BadRequest, "Parameters were invalid.");
				}

				var canUpdate = Authorization.IsAuthorized(UserId, ActivityEnum.Update);
				if (!canUpdate)
					return Request.CreateResponse(HttpStatusCode.Forbidden);

				var result =_addressBookService.UpdateAddressBookEntry(parameters);
				if (result.ResultType == AddressBookCommandResultType.Error)
				{
					Logger.Error("UpdateAddressBookEntry failed with error: {0}", result.ErrorMessage);
					return Request.CreateResponse(HttpStatusCode.InternalServerError, "Unexpected server error");
				}

				Logger.Trace("UpdateAddressBookEntry creating response");
				return Request.CreateResponse(HttpStatusCode.OK);
			}
			catch (Exception e)
			{
				Logger.ErrorException("UpdateAddressBookEntry failed", e);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, "Unexpected server error");
			}
		}

		[AcceptVerbs("POST")]
		[EnableCors("*", "*", "*")]
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

				var result =_addressBookService.DeleteAddressBookEntry(addressBookEntryId);
				if (result.ResultType == AddressBookCommandResultType.Error)
				{
					Logger.Error("DeleteAddressBookEntry failed with error: {0}", result.ErrorMessage);
					return Request.CreateResponse(HttpStatusCode.InternalServerError, "Unexpected server error");
				}

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
