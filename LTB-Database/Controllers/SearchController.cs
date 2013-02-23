using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

using LTB_Database.Models;
using LTB_Database.ViewModels;
using LTB_Database.Repository;


namespace LTB_Database.Controllers
{
    public class SearchController : Controller
    {
		private LtbRepository repo = new LtbRepository();
	
		public SearchController()
		{
			
		}
		
        [HttpGet]
        public ActionResult Index(string q, int? page, string layout)
        {
			if(String.IsNullOrEmpty(q))
				throw new Exception("Die Suchanfrage muss mindestens 3 Buchstaben enthalten.");
			
			//basic
			if (page == null || page == 0)
			{
				page = 1;
			}

			int items = 12;
			bool shelf = true;

			if (!String.IsNullOrEmpty(layout))
			{
				if (layout.Equals("shelf"))
				{
					items = 12;
					shelf = true;
				}
				if (layout.Equals("list"))
				{
					items = 50;
					shelf = false;
				}
			}

			//get data
			var pager = new Pager((int)page, items, repo.GetBookCountForSearch(q));

			if (page > pager.TotalCount / items && pager.TotalCount != 0)
			{
				page = (int)pager.TotalPages;
				pager = new Pager((int)page, items, pager.TotalCount);
			}

			if (page < 1)
			{
				page = 1;
				pager = new Pager((int)page, items, pager.TotalCount);
			}

        	var books = repo.GetSearchBook(q, items, (int)page);
        	
        	var view = new SearchIndexModel { Books = books, Query = q, Pager = pager };
        	
        	return View(view);
        }
    }
}
