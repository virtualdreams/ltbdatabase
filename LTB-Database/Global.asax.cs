using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.IO;

using LTB_Database.Controllers;

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
			routes.IgnoreRoute("fancybox/{*pathInfo}");
			
			routes.MapRoute("AutoComplete", "AutoComplete/", new { controller = "Home", action = "Query" });
			
			routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = "" });
		}

		protected void Application_Start ()
		{
			RegisterRoutes (RouteTable.Routes);
			
			log4net.Config.XmlConfigurator.Configure(new FileInfo(HttpContext.Current.Request.MapPath("~/App_Data/log4net.xml")));
			
			GlobalConfig conf = GlobalConfig.Get();

			LTB_Database.Repository.LtbRepositoryConfig.Config(
				conf.ConnectionString,
				conf.AssemblyName,
				conf.ConnectionClass,
				conf.MappingFile);
		
			log.Info("Starting application");
		}

		//public void Session_Start(object sender, EventArgs e)
		//{
		//    log.Info("Starting session width id: " + Session.SessionID);
		//}
		
		protected void Application_Error (object sender, EventArgs e)
		{
			Exception exception = Server.GetLastError();
			
			if (exception.Message == "Datei ist nicht vorhanden." || exception.Message == "File does not exist.")
			{
				//string text = string.Format("{0} {1}", ex.Message, HttpContext.Current.Request.Url.ToString());
				log.DebugFormat("{0}: {1}", exception.Message, HttpContext.Current.Request.Url.ToString());
				return;
			}
			
			Response.Clear ();
			HttpException httpException = exception as HttpException;
			
			RouteData routeData = new RouteData ();
			routeData.Values.Add ("controller", "Error");
			
			if (httpException == null)
			{
				routeData.Values.Add ("action", "Index");
			}
			else
			{
				switch (httpException.GetHttpCode ())
				{
					case 404:
						log.ErrorFormat("{0}: {1}", httpException.Message, HttpContext.Current.Request.Url.ToString());
						routeData.Values.Add ("action", "Http404");
						break;
					
					case 500:
						log.Fatal(HttpContext.Current.Request.Url.ToString(), httpException);
						routeData.Values.Add ("action", "Http500");
						break;
				
					default:
						log.Fatal(HttpContext.Current.Request.Url.ToString(), httpException);
						routeData.Values.Add ("action", "Index");
						break;
				}
			}
			
			routeData.Values.Add ("ex", exception);
			
			Server.ClearError ();
			//Response.TrySkipIisCustomErrors = true;
			
			IController errorController = new ErrorController ();
			errorController.Execute (new RequestContext (new HttpContextWrapper (Context), routeData));
		}
	}
}

