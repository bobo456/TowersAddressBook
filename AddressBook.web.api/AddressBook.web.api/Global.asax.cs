using System;
using System.Reflection;
using System.Web;
using System.Web.Http;
using AddressBook.Application.Services;
using AddressBook.Data;
using AddressBook.Data.Queries;
using AddressBook.web.api.Services;
using Autofac;
using Autofac.Integration.WebApi;
using Newtonsoft.Json;
using NLog;

namespace AddressBook.web.api
{
    public class WebApiApplication : HttpApplication
    {
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();


		protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
			var config = GlobalConfiguration.Configuration;
			var builder = new ContainerBuilder();
			builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

			// defined in the AddressBook.WebApi assembly
			builder.RegisterType<AuthorizationService>().AsImplementedInterfaces().InstancePerLifetimeScope();
			builder.RegisterType<CurrentUserService>().AsImplementedInterfaces().InstancePerLifetimeScope();

			// defined in the AddressBook.Application assembly
			builder.RegisterType<AddressBookQueries>().AsImplementedInterfaces().InstancePerLifetimeScope();
			builder.RegisterType<AddressBookService>().AsImplementedInterfaces().InstancePerLifetimeScope();

			// defined in the AddressBook.Data infrastructure assembly
			builder.Register(c => new FakeDbSession()).As<ISession>().SingleInstance();
			builder.RegisterType<FakeUnitOfWork>().AsImplementedInterfaces().InstancePerDependency();

			var container = builder.Build();

			// Set the WebAPI dependency resolver
			config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
		}

		protected void Application_Error(object sender, EventArgs e)
		{
			var ex = Server.GetLastError();
			var clientAddress = HttpContext.Current.Request.UserHostAddress;
			var requestUrl = HttpContext.Current.Request.RawUrl;
			var logMsg = string.Format("An unhandled exception occurred. Request from IP Address: {0} to URL: {1}", clientAddress, requestUrl);
			Logger.Error(ex, logMsg);
			Server.ClearError();

			if (Context.Handler is System.Web.Http.WebHost.HttpControllerHandler)
			{
				Response.StatusCode = 500;
				Response.TrySkipIisCustomErrors = true;
#if DEBUG
				Response.ContentType = "application/json";
				Response.Write(JsonConvert.SerializeObject(new { details = ex }));
#endif
			}
			else
			{
				Server.TransferRequest("/");
			}
		}
	}
}
