using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using LTB_Database.Models;
using LTB_Database.ViewModels;
using LTB_Database.Repository;

namespace LTB_Database
{
	public class CategoriesController : Controller
	{
		private LtbRepository repo = new LtbRepository();
	
		public CategoriesController()
		{
			
		}
	
		public ActionResult Index ()
		{
			var categories = repo.GetCategories();
			
			var view = new ViewCategoriesModel { Category = categories };
			
			return View(view);
		}

		[ChildActionOnly]
		public ActionResult Get()
		{
			var categories = repo.GetCategories();

			var view = new ViewCategoriesModel {
				Category = categories
			};

			return View("Categories", view);
		}
		
		//[HttpGet]
		//public ActionResult List(int id, int? page)
		//{
		//    if (page == null || page == 0)
		//    {
		//        page = 1;
		//    }
			
		//    var books = repo.GetBooksFromCategory(50, (int)page, id);
		//    var pager = new Pager((int)page, 50, repo.GetBookCount());
			
		//    var view = new ViewCategoryModel { Books = books, Pager = pager, Query = id.ToString() };
			
		//    return View(view);
		//}

		
		//[HttpGet]
		//public ActionResult View(int id, int? page)
		//{
		//    if (page == null || page == 0)
		//    {
		//        page = 1;
		//    }

		//    var books = repo.GetBooksFromCategory(12, (int)page, id);
		//    var pager = new Pager((int)page, 12, repo.GetBookCount());

		//    var view = new ViewCategoryModel { Books = books, Pager = pager, Query = id.ToString() };

		//    return View(view);
		//}
		
		[HttpGet]
		public  ActionResult Create()
		{
			var category = new Category();
		
			var view = new CategoriesEditViewModel { Category = category };
			return View("Edit", view);
		}

		[HttpGet]
		public ActionResult Edit(int? id)
		{
			if (id == null || id == 0)
				throw new Exception("Book not found.");
			
			var category = repo.GetCategory((int)id);
			
			var view = new CategoriesEditViewModel { Category = category };
			
			return View(view);
		}
		
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Edit(int id, string name)
		{
			int retid = 0;
			
			if(String.IsNullOrEmpty(name))
				ModelState.AddModelError("name", "Bitte geben Sie einen Namen an.");
			
			if(ModelState.IsValid)
			{
				if(id != 0)
				{
					int res = repo.UpdateCategory(id, name);
					retid = id;
					return RedirectToAction("Edit", "Categories", new { id = retid });
				}
				else
				{
					int ret = 0;
					int res = repo.InsertCategory(name, out ret);
					retid = ret;
					return RedirectToAction("Edit", "Categories", new { id = retid });
				}
			}

			var category = repo.GetCategory((int)id);

			var view = new CategoriesEditViewModel { Category = category };
			
			return View(view);
		}
		
		[HttpGet]
		public ActionResult Delete(int id)
		{
			var category = repo.GetCategory(id);

			var view = new CategoriesDeleteViewModel { Category = category };

			return View(view);
		}

		[HttpPost]
		public ActionResult Delete(int id, FormCollection form)
		{
			int res = repo.DeleteCategory(id);

			return RedirectToAction("Index", "Home");
		}
		
		[HttpPost]
		public ActionResult Upload (HttpPostedFileBase file)
		{
			
			return View ();
		}
		
		//public ActionResult Image (long id)
//		{
//			Database db = new Database ();
//			db.Open ();
//			
//			Image image = db.Image (id);
//			
//			db.Close ();
//			
//			return new FileContentResult (image.Data, image.Mime);
		//}
	}
}

