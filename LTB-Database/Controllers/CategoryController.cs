using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using LTB_Database.Models;
using LTB_Database.ViewModels;
using LTB_Database.Filters;
using LTB_Database.Core.DataModel;
using LTB_Database.Core;

namespace LTB_Database
{
	[GlobalExceptionFilter]
	public class CategoryController : Controller
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(CategoryController));
		private readonly CategoryService _service;
	
		public CategoryController()
		{
			_service = new CategoryService(new ModelStateWrapper(this.ModelState));
		}
		
		#region version 2
		
		[HttpGet]
		public ActionResult Index(long? page)
		{
			BookModel[] books = _service.GetBooks();
			Pager pager = new Pager(page ?? 1, 12, books.Count());
			long newPage = pager.SetPageIndexToLimit(page ?? 1);
			
			CategoryViewModel view = new CategoryViewModel
			{
				Books = books.Skip((int)(newPage * 12) - 12).Take(12).ToArray(),
				Pager = pager
			};
			
			return View(view);
		}
		
		[HttpGet]
		public ActionResult View(long? id, long? page)
		{
			CategoryModel category = _service.GetCategory(id ?? 0);
			BookModel[] books = _service.GetBooks().Where(x => x.CategoryId == category.Id).ToArray();
			
			Pager pager = new Pager(page ?? 1, 12, books.Count());
			long newPage = pager.SetPageIndexToLimit(page ?? 1);

			CategoryViewModel view = new CategoryViewModel
			{
				Books = books.Skip((int)(newPage * 12) - 12).Take(12).ToArray(),
				Category = category,
				Pager = pager
			};
			
			return View(view);
		}
		
		[HttpGet]
		public ActionResult Create()
		{
			EditCategoryViewModel view = new EditCategoryViewModel
			{
				Category = new CategoryModel()
			};
			
			return View("edit", view);
		}
		
		[HttpGet]
		public ActionResult Edit(long? id)
		{
			EditCategoryViewModel view = new EditCategoryViewModel
			{
				Category = _service.GetCategory(id ?? 0)
			};
			
			return View(view);
		}
		
		[HttpPost]
		public ActionResult Edit(CategoryModel model)
		{
			if(ModelState.IsValid)
			{
				long id = _service.SaveCategory(model);

				return RedirectToAction("view", "category", new
				{
					id = id
				});
			}
			
			EditCategoryViewModel view = new EditCategoryViewModel
			{
				Category = model
			};
			
			return View(view);
		}
		
		[HttpGet]
		public ActionResult Delete(long? id)
		{
			CategoryViewModel view = new CategoryViewModel
			{
				Category = _service.GetCategory(id ?? 0)
			};
			
			return View(view);
		}
		
		[HttpPost]
		public ActionResult Delete(long? id, long? dummy)
		{
			_service.DeleteCategory(id ?? 0);

			return RedirectToAction("Index", "Home");
		}
		
		[ChildActionOnly]
		public ActionResult AvailableCategories()
		{
			CategoriesViewModel view = new CategoriesViewModel
			{
				Categories = _service.GetCategories()
			};
			
			return View("categories", view);
		}
		
		#endregion
		
		

//#region Categoires TODO	
		//[HttpGet]
		//public  ActionResult Create()
		//{
		//    var category = new Category();
		
		//    var view = new CategoriesEditViewModel { Category = category };
		//    return View("Edit", view);
		//}


//        [HttpGet]
//        public ActionResult Edit(int? id)
//        {
//            if (id == null || id == 0)
//                throw new Exception("Book not found.");
			
//            var category = repo.GetCategory((int)id);
			
//            var view = new CategoriesEditViewModel { Category = category };
			
//            return View(view);
//        }
		
//        [AcceptVerbs(HttpVerbs.Post)]
//        public ActionResult Edit(int id, string name)
//        {
//            int retid = 0;
			
//            if(String.IsNullOrEmpty(name))
//                ModelState.AddModelError("name", "Bitte geben Sie einen Namen an.");
			
//            if(ModelState.IsValid)
//            {
//                if(id != 0)
//                {
//                    int res = repo.UpdateCategory(id, name);
//                    retid = id;
//                    return RedirectToAction("Edit", "Categories", new { id = retid });
//                }
//                else
//                {
//                    int ret = 0;
//                    int res = repo.InsertCategory(name, out ret);
//                    retid = ret;
//                    return RedirectToAction("Edit", "Categories", new { id = retid });
//                }
//            }

//            var category = repo.GetCategory((int)id);

//            var view = new CategoriesEditViewModel { Category = category };
			
//            return View(view);
//        }
		
//        [HttpGet]
//        public ActionResult Delete(int id)
//        {
//            var category = repo.GetCategory(id);

//            var view = new CategoriesDeleteViewModel { Category = category };

//            return View(view);
//        }

//        [HttpPost]
//        public ActionResult Delete(int id, FormCollection form)
//        {
//            int res = repo.DeleteCategory(id);

//            return RedirectToAction("Index", "Home");
//        }
		
//        [HttpPost]
//        public ActionResult Upload (HttpPostedFileBase file)
//        {
			
//            return View ();
//        }
//#endregion
	}
}

