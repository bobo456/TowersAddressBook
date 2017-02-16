using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AddressBook.Application.CommandParameters;
using AddressBook.Application.Services;
using AddressBook.Domain;
using AddressBook.web.api.Services;
using NLog;

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
		public HttpResponseMessage AddAddressBookEntry(AddAddressCommandParameters parameters)
		{
			try
			{
				Logger.Trace("AddAddressBookEntry started through Web API");

				if (parameters == null)
				{
					const string nullParametersErrorMsg = "AddAddressBookEntry called with null parameters";
					Logger.Info(nullParametersErrorMsg);
					return Request.CreateResponse(HttpStatusCode.BadRequest, new AddAddressBookEntryResult {Error = nullParametersErrorMsg });
				}

				if (!ModelState.IsValid)
				{
					var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();

					Logger.Info("AddAddressBookEntry called with invalid parameters: {0}", string.Join(", ", errors));
					return Request.CreateResponse(HttpStatusCode.BadRequest, new AddAddressBookEntryResult {ValidationErrors = errors});
				}

				var canAddAddresses = Authorization.IsAuthorized(UserId, ActivityEnum.Add);
				if (!canAddAddresses)
					return Request.CreateResponse(HttpStatusCode.Forbidden);

				var result =_addressBookService.AddAddressBookEntry(parameters);
				switch(result.ResultType)
				{ 
					case AddressBookCommandResultType.Duplicate:
						Logger.Trace(result.Error);
						return Request.CreateResponse(HttpStatusCode.BadRequest, "Duplicate address book entry found.");
					case AddressBookCommandResultType.Error:
						Logger.Error("AddAddressBookEntry failed with error: {0}", result.Error);
						return Request.CreateResponse(HttpStatusCode.InternalServerError, "Unexpected server error");
				}

				Logger.Trace("AddAddressBookEntry creating response");
				return Request.CreateResponse(HttpStatusCode.OK, result.NewAddressBookEntry);
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "AddAddressBookEntry failed");
				return Request.CreateResponse(HttpStatusCode.InternalServerError, "Unexpected server error");
			}
		}

		[AcceptVerbs("PUT")]
		public HttpResponseMessage UpdateAddressBookEntry(UpdateAddressCommandParameters parameters)
		{
			try
			{
				Logger.Trace("UpdateAddressBookEntry started through Web API");

				if (parameters == null)
				{
					const string nullParametersErrorMsg = "UpdateAddressBookEntry called with null parameters";
					Logger.Info(nullParametersErrorMsg);
					return Request.CreateResponse(HttpStatusCode.BadRequest, new AddAddressBookEntryResult { Error = nullParametersErrorMsg });
				}

				if (!ModelState.IsValid)
				{
					var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();

					Logger.Info("UpdateAddressBookEntry called with invalid parameters: {0}", string.Join(", ", errors));
					return Request.CreateResponse(HttpStatusCode.BadRequest, new AddAddressBookEntryResult { ValidationErrors = errors });
				}

				var canUpdate = Authorization.IsAuthorized(UserId, ActivityEnum.Update);
				if (!canUpdate)
					return Request.CreateResponse(HttpStatusCode.Forbidden);

				var result =_addressBookService.UpdateAddressBookEntry(parameters);
				if (result.ResultType == AddressBookCommandResultType.Error)
				{
					Logger.Error("UpdateAddressBookEntry failed with error: {0}", result.Error);
					return Request.CreateResponse(HttpStatusCode.InternalServerError, "Unexpected server error");
				}

				Logger.Trace("UpdateAddressBookEntry creating response");
				return Request.CreateResponse(HttpStatusCode.OK);
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "UpdateAddressBookEntry failed");
				return Request.CreateResponse(HttpStatusCode.InternalServerError, "Unexpected server error");
			}
		}

		[AcceptVerbs("DELETE")]
		public HttpResponseMessage DeleteAddressBookEntry(string id)
		{
			try
			{
				Logger.Trace("DeleteAddressBookEntry started through Web API for: {0}", id);

				if (id == null)
				{
					Logger.Info("DeleteAddressBookEntry called with invalid parameters");
					return Request.CreateResponse(HttpStatusCode.BadRequest, "addressBookEntryId cannot be null");
				}

				var canDeleteAddresses = Authorization.IsAuthorized(UserId, ActivityEnum.Delete);
				if (!canDeleteAddresses)
					return Request.CreateResponse(HttpStatusCode.Forbidden);

				var result =_addressBookService.DeleteAddressBookEntry(id);
				if (result.ResultType == AddressBookCommandResultType.Error)
				{
					Logger.Error("DeleteAddressBookEntry failed with error: {0}", result.Error);
					return Request.CreateResponse(HttpStatusCode.InternalServerError, "Unexpected server error");
				}

				Logger.Trace("DeleteAddressBookEntry creating response for: {0}", id);
				return Request.CreateResponse(HttpStatusCode.OK);
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "DeleteAddressBookEntry failed");
				return Request.CreateResponse(HttpStatusCode.InternalServerError, "Unexpected server error");
			}
		}
	}
}
