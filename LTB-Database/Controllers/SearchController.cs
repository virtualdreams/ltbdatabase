using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

using LTB_Database.Core;
using LTB_Database.Models;
using LTB_Database.ViewModels;
using LTB_Database.Filters;

namespace LTB_Database.Controllers
{
	[GlobalExceptionFilter]
    public class SearchController : Controller
    {
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(SearchController));
		private readonly SearchService _service;
		
		public SearchController()
		{
			_service = new SearchService(new ModelStateWrapper(this.ModelState));
		}
		
        [HttpGet]
        public ActionResult Index(string q, long? page, string layout)
        {
			BookModel[] books = _service.Search(q);
			Pager pager = new Pager(page ?? 1, 12, books.Count());
			long newPage = pager.SetPageIndexToLimit(page ?? 1);
			
			SearchViewModel view = new SearchViewModel
			{
				Query = q,
				Books = books.Skip((int)(newPage * 12) - 12).Take(12).ToArray(),
				Pager = pager
			};
			
			return View(view);
        }
    }
}
