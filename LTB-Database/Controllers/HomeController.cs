﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

using LTB_Database.Models;
using LTB_Database.ViewModels;

using System.IO;
using System.Diagnostics;
using LTB_Database.Filters;
using LTB_Database.Core.DataAccess;
using LTB_Database.Core.DataModel;
using LTB_Database.Core;

namespace LTB_Database.Controllers
{
    [GlobalExceptionFilter]
    public class HomeController : Controller
    {
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger (typeof(HomeController));
		private readonly HomeService _service;

		public HomeController()
		{
			_service = new HomeService(new ModelStateWrapper(this.ModelState));
		}

        public ActionResult Index ()
        {
			CategoryViewModel view = new CategoryViewModel
			{
				Books = _service.GetLatestBooks(6)
			};
			
			return View(view);
        }
		
		[HttpGet]
		public ActionResult Query(string term)
		{
			return new JsonResult { Data = _service.AjaxSearch(term), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
		}
		
		[ChildActionOnly]
		public ActionResult Statistic()
		{
			StatisticViewModel view = new StatisticViewModel
			{
				Books = _service.BookCount(),
				Stories = _service.StoryCount()
			};
			
			return View(view);
		}
    }
}
