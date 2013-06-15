using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LTB_Database.Filters
{
	public class GlobalActionFilter : ActionFilterAttribute, IActionFilter, IResultFilter
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (!filterContext.IsChildAction)
			{
				var ms = filterContext.Controller.ViewData.ModelState;
				//filterContext.Result = new RedirectToRouteResult(
				//    new RouteValueDictionary { { "Controller", "Books" }, { "Action", "Index" } });
			}

			base.OnActionExecuting(filterContext);
		}
	}
}
