using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.IO;

using LTB_Database.Controllers;
using LTB_Database.Core;

namespace LTB_Database
{
	public class MvcApplication : System.Web.HttpApplication
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger (typeof(MvcApplication));
		
		public static void RegisterRoutes (RouteCollection routes)
		{
			routes.RouteExistingFiles = false;
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.IgnoreRoute("Scripts/{*pathInfo}");
			routes.IgnoreRoute("Images/{*pathInfo}");
			//routes.IgnoreRoute("fancybox/{*pathInfo}");
			routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
			
			routes.MapRoute("AutoComplete", "AutoComplete/", new { controller = "Home", action = "Query" });
			routes.MapRoute("AddStory", "AddStory/", new { controller = "Home", action = "AddStory" });
			routes.MapRoute("RemStory", "RemStory/", new { controller = "Home", action = "RemoveStory" });
			
			routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = "" });
		}

		protected void Application_Start ()
		{
			RegisterRoutes (RouteTable.Routes);
			
			log4net.Config.XmlConfigurator.Configure(new FileInfo(ConvertToFullPath(GlobalConfig.Get().Log4Net)));
		
			log.Info("Starting application");
		}

		protected void Application_End(object sender, EventArgs e)
		{
			log.InfoFormat("Stopping application");
		}

		//public void Session_Start(object sender, EventArgs e)
		//{
		//	log.InfoFormat("Starting session with id '{0}'", Session.SessionID);
		//}
		
		//public void Session_End(object sender, EventArgs e)
		//{
		//	log.InfoFormat("End session with id '{0}'", Session.SessionID);
		//}

		protected void Application_Error(object sender, EventArgs e)
		{
			Exception exception = Server.GetLastError();
			HttpException httpException = exception as HttpException;
			
			log.Fatal(exception);

            if (log.IsDebugEnabled)
            {
                MvcApplication app = sender as MvcApplication;
                log.DebugFormat("Request path '{0}'", app.Request.Path);
            }
			
			Response.Clear();
			Server.ClearError();
			
			RouteData routeData = new RouteData();
			routeData.Values.Add("controller", "Error");
			routeData.Values.Add("action", "Index");
			routeData.Values.Add("exception", exception);
			
			Response.StatusCode = 500;
			
			if(httpException != null)
			{
				Response.StatusCode = httpException.GetHttpCode();
				switch(Response.StatusCode)
				{
					case 403:
						routeData.Values["action"] = "Http403";
                        Response.StatusCode = 403;
						break;
						
					case 404:
						routeData.Values["action"] = "Http404";
                        Response.StatusCode = 404;
						break;
				}
			}
			
			Response.TrySkipIisCustomErrors = true;
			IController errorController = new ErrorController();
			HttpContextWrapper wrapper = new HttpContextWrapper(Context);
			RequestContext rc = new RequestContext(wrapper, routeData);
			errorController.Execute(rc);
		}

		/// <summary>
		/// Get th full path for the given filename.
		/// </summary>
		/// <param name="filename">The filename as file, absolute filepath or relative filepath</param>
		/// <returns>The full path</returns>
		private string ConvertToFullPath(string filename)
		{
			if (String.IsNullOrEmpty(filename))
			{
				throw new ArgumentNullException("filename");
			}

			string tmp = "";
			try
			{
				string applicationBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
				if (applicationBaseDirectory != null)
				{
					Uri uri = new Uri(applicationBaseDirectory);
					if (uri.IsFile)
					{
						tmp = uri.LocalPath;
					}
				}
			}
			catch
			{
			}
			if (!String.IsNullOrEmpty(tmp))
			{
				return Path.GetFullPath(Path.Combine(tmp, filename));
			}
			return Path.GetFullPath(filename);
		}
	}
}

