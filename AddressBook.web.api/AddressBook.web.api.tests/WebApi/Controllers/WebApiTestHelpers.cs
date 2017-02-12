using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;

namespace AddressBook.web.api.tests.WebApi.Controllers
{
	public static class WebApiTestHelpers
	{
		/// <summary>
		/// Establishes the Request and ControllerContext properties of the specified ApiController with test-specific values
		/// </summary>
		/// <param name="controller">The WebApiController to set up</param>
		/// <param name="method">Defaults to "GET"</param>
		/// <param name="controllerName">Defaults to "ctrl"</param>
		/// <param name="actionName">Defaults to "method"</param>
		/// <param name="id">Defaults to missing</param>
		public static void SetupForUnitTest(this ApiController controller, HttpMethod method = null, string controllerName = "ctrl", string actionName = "method", string id = null)
		{
			var config = new HttpConfiguration();
			WebApiConfig.Register(config);
			var url = string.Format("http://localhost/api/v1/{0}/{1}{2}", controllerName, actionName, id == null ? "" : "/" + id);

			var request = new HttpRequestMessage(method ?? HttpMethod.Get, url);
			var httpRouteValueDictionary = new HttpRouteValueDictionary { { "controller", controllerName }, { "action", "method" } };
			if (id != null)
				httpRouteValueDictionary.Add("id", id);

			var routeData = new HttpRouteData(config.Routes[0], httpRouteValueDictionary);

			controller.ControllerContext = new HttpControllerContext(config, routeData, request);
			controller.Request = request;
			controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
		}
	}
}