using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

using LTB_Database.Models;
using LTB_Database.ViewModels;
using LTB_Database.Repository;

using System.IO;
using System.Diagnostics;

namespace LTB_Database.Controllers
{
    public class HomeController : Controller
    {
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger (typeof(HomeController));
		private LtbRepository repo = new LtbRepository();

		public HomeController()
		{
			
		}
		
        public ActionResult Index ()
        {
			var books = repo.GetLatestBooks();
			var categories = repo.GetCategories();

			var view = new HomeIndexModel { Latest = books, Categories = categories };

			return View(view);
        }
		
		[HttpGet]
		public ActionResult Query (string term)
		{
			var result = repo.GetLiveSearch(term);
			return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
		}
		
		[ChildActionOnly]
		public ActionResult Statistic()
		{
			int books = repo.GetBookCount();
			int stories = repo.GetStoryCount();
			
			var view = new StatisticViewModel { Books = books, Stories = stories };
			
			return View(view);
		}
    }
}
