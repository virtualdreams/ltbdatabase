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

			if (filterContext.HttpContext.Request.IsAjaxRequest())
			{
				filterContext.Result = new JsonResult
				{
					Data = new { success = "false", error = filterContext.Exception.Message },
					JsonRequestBehavior = JsonRequestBehavior.AllowGet 
				};
				
				filterContext.ExceptionHandled = true;
				
				if(filterContext.Exception is HttpException)
				{
					HttpException ex = filterContext.Exception as HttpException;
					filterContext.HttpContext.Response.StatusCode = ex.GetHttpCode();
				}
				else
				{
					filterContext.HttpContext.Response.StatusCode = 500;
				}

				log.Fatal(filterContext.Exception.Message);
				
				return;
			}
			
			if (!filterContext.IsChildAction)
			{	
				ErrorModel model = new ErrorModel { Error = filterContext.Exception.Message };
				
				filterContext.Result = new ViewResult
				{
					ViewName = "Error",
					ViewData = new ViewDataDictionary<ErrorModel>(model),
					TempData = filterContext.Controller.TempData
				};
				filterContext.ExceptionHandled = true;

				if (filterContext.Exception is HttpException)
				{
					HttpException ex = filterContext.Exception as HttpException;
					filterContext.HttpContext.Response.StatusCode = ex.GetHttpCode();
				}
				else
				{
					filterContext.HttpContext.Response.StatusCode = 500;
				}
				
				log.Fatal(filterContext.Exception.Message);
				
				return;
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

				if (filterContext.Exception is HttpException)
				{
					HttpException ex = filterContext.Exception as HttpException;
					filterContext.HttpContext.Response.StatusCode = ex.GetHttpCode();
				}
				else
				{
					filterContext.HttpContext.Response.StatusCode = 500;
				}

				log.Fatal(filterContext.Exception.Message);
				
				return;
				
				//Alternate - return nothing
				//filterContext.Result = new EmptyResult();
				//filterContext.ExceptionHandled = true;
			}
		}
	}
}
