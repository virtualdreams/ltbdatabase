using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using LTB_Database.Models;
using LTB_Database.ViewModels;
using SqlDataMapper;
using System.Data.Common;

namespace LTB_Database.Controllers
{
	public class ErrorController : Controller
	{
		public ActionResult Index (Exception ex)
		{
			Response.StatusCode = 500;
			var view = new ErrorModel { Error = ex.Message };
			
			if(ex is SqlDataMapperException)
			{
				if(Request.IsAjaxRequest())
				{
					return new EmptyResult();
				}
			}
			if(ex is DbException)
			{
				if(Request.IsAjaxRequest())
				{
					return new EmptyResult();
				}
			}
			return View(view);
		}
		
		public ActionResult Http404 (Exception ex)
		{
			Response.StatusCode = 404;
			var view = new ErrorModel { Error = "Die Ressource '" + HttpContext.Request.Url.ToString() + "' konnte nicht gefunden werden." };
			
			if(Request.IsAjaxRequest())
				return new EmptyResult();
			
			return View(view);
		}

		public ActionResult Http500(Exception ex)
		{
			Response.StatusCode = 500;
			var view = new ErrorModel { Error = "Entschuldigung, aber ein interner Fehler ist aufgetreten. Das hätte nicht passieren dürfen. Bitte versuchen Sie es nocheinaml. Sollte der Fehler weiterhin auftreten, kontaktieren Sie bitte den zuständigen Administrator." };
			
			if(Request.IsAjaxRequest())
				return new EmptyResult();
			
			return View(view);
		}
	}
}

