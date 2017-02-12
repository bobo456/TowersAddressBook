using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace AddressBook.web.api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
			// Web API configuration and services

			config.EnableCors();

			// Web API routes
			config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v1/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

			//Remove application/xml so that it defaults to returning JSON data. 
			var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
			config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

			// Register custom WebApi exception handler
			config.Services.Replace(typeof(IExceptionHandler), new WebApiUnhandledExceptionHandler());
		}
    }

	public class WebApiUnhandledExceptionHandler : ExceptionHandler
	{
		public override void Handle(ExceptionHandlerContext context)
		{
			// Causes exception to bubble up out of WebApi to ASP.NET. We need to catch it again in Global.asax Application_Error()
			context.Result = null;
		}
	}
}
