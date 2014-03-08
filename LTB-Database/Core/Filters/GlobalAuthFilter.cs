using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTB_Database.ViewModels;

namespace LTB_Database.Filters
{
	public class GlobalAuthFilter : ActionFilterAttribute, IActionFilter
	{
		private string _allowedUsers = "*";
		
		public GlobalAuthFilter()
		{
			_allowedUsers = "*";
		}
		
		public GlobalAuthFilter(string allowedUsers)
		{
			_allowedUsers = allowedUsers;
		}
		
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			string userName = filterContext.HttpContext.User.Identity.Name;
			if(!String.IsNullOrEmpty(userName))
			{
				if(!AllowedUsers(userName))
				{
					ErrorModel model = new ErrorModel { Error = "Benutzer konnte nicht authentifiert werden." };
					ViewResult result = new ViewResult {
						ViewName = "Error",
						ViewData = new ViewDataDictionary<ErrorModel>(model),
						TempData = filterContext.Controller.TempData
					};
					
					filterContext.Result = result;
				}
			}
			else
				throw new Exception("Es konnte kein Benutzer ermittelt werden.");
			
			base.OnActionExecuting(filterContext);
		}
		
		private bool AllowedUsers(string user)
		{
			if(String.IsNullOrEmpty(user))
				throw new Exception("Empty username");
				
			string[] name = user.Split('\\');
			
			foreach(string u in _allowedUsers.Split(','))
			{
				if(u.ToLower().Equals(name[1].ToLower()))
					return true;
			}
			return false;
		}
	}
}
