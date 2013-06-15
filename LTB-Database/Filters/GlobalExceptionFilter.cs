using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using LTB_Database.ViewModels;

namespace LTB_Database.Filters
{
	public class GlobalExceptionFilter : ActionFilterAttribute, IExceptionFilter 
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(GlobalExceptionFilter));
		
		public void OnException(ExceptionContext filterContext)
		{
			if(filterContext.ExceptionHandled)
				return;
			
			if (!filterContext.IsChildAction)
			{	
				//filterContext.Controller.TempData["exception"] = filterContext.Exception;
				//filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "Error" }));
				//filterContext.ExceptionHandled = true;
				
				ErrorModel model = new ErrorModel { Error = filterContext.Exception.Message };
				
				filterContext.Result = new ViewResult
				{
					ViewName = "Error",
					ViewData = new ViewDataDictionary<ErrorModel>(model),
					TempData = filterContext.Controller.TempData
				};
				filterContext.ExceptionHandled = true;
				
				log.Fatal(filterContext.Exception.Message);
			}
			else
			{
				ErrorModel model = new ErrorModel { Error = filterContext.Exception.Message };

				filterContext.Result = new ViewResult
				{
					ViewName = "ErrorI", //a ascx file
					ViewData = new ViewDataDictionary<ErrorModel>(model),
					TempData = filterContext.Controller.TempData
				};
				filterContext.ExceptionHandled = true;

				log.Fatal(filterContext.Exception.Message);
				
				//Alternate - return nothing
				//filterContext.Result = new EmptyResult();
				//filterContext.ExceptionHandled = true;
			}
		}
	}
}
