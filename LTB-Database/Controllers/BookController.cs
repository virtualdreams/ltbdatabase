using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Diagnostics;

using LTB_Database.Models;
using LTB_Database.ViewModels;
using LTB_Database.Filters;
using LTB_Database.Core.DataModel;
using LTB_Database.Core;

namespace LTB_Database
{
	[GlobalExceptionFilter]
	public class BookController : Controller
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger (typeof(BookController));
		private readonly BookService _service;
		
		public BookController()
		{
			_service = new BookService(new ModelStateWrapper(this.ModelState));
		}

		[HttpGet]
		public ActionResult View(long? id)
		{
			BookModel book = _service.GetBook(id ?? 0);
			CategoryModel category = _service.GetCategory(book.CategoryId);
			
			ViewBookViewModel view = new ViewBookViewModel
			{
				Book = book,
				Category = category
			};
			
			return View(view);
		}	
		
		[HttpGet]
		public ActionResult Create()
		{
			EditBookViewModel view = new EditBookViewModel()
			{
				Book = new BookModel(),
				Categories = _service.GetCategories()
			};
			
			return View("edit", view);
		}

		[HttpGet]
		public ActionResult Edit(long? id)
		{
			EditBookViewModel view = new EditBookViewModel()
			{
				Book = _service.GetBook(id ?? 0),
				Categories = _service.GetCategories()
			};
			
			return View(view);
		}
		
		[HttpPost]
		public ActionResult Edit(BookModel model)
		{
			if (ModelState.IsValid)
			{
				long id = _service.SaveBook(model);

				return RedirectToAction("view", "book", new
				{
					id = id
				});
			}

			EditBookViewModel view = new EditBookViewModel()
			{
				Book = model,
				Categories = _service.GetCategories()
			};

			return View(view);
		}
		
		[HttpPost]
		public ActionResult RemoveImage(long? id)
		{
			_service.RemoveImage(id ?? 0);
			
			return new JsonResult { Data = new { success = "true", error = "" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
		}

		[HttpGet]
		public ActionResult Delete(long? id)
		{
			ViewBookViewModel view = new ViewBookViewModel
			{
				Book = _service.GetBook(id ?? 0)
			};
			
			return View(view);
		}

		[HttpPost]
		public ActionResult Delete(long? id, long? dummy)
		{
			_service.DeleteBook(id ?? 0);
			
			return RedirectToAction("Index", "Home");
		}
		
		#region Obsolete
		//[HttpPost]
		//public ActionResult Upload(int id, HttpPostedFileBase image)
		//{
		//    if(image != null)
		//    {
		//        ///mime check
		//        string[] mime_types = GlobalConfig.Get().AllowMimeType.Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries);
		//        if(!mime_types.Contains(image.ContentType))
		//            return RedirectToAction("Edit", "Books", new { id = id });
				
		//        ///filesize check
				
				
		//        string filename = String.Format("img_{0}{1}", id, Path.GetExtension(image.FileName));
		//        string filepath = HttpContext.Server.MapPath(String.Format("{0}{1}", GlobalConfig.Get().ImagePath, filename));
		//        image.SaveAs(filepath);
				
		//        int res = repo.UpdateBookImage(id, filename);
		//    }
		//    else
		//    {
		//        Book book = repo.GetBook(id);
		//        if(book != null)
		//        {
		//            if(!String.IsNullOrEmpty(book.image))
		//            {
		//                string filepath = HttpContext.Server.MapPath(String.Format("{0}{1}", GlobalConfig.Get().ImagePath, book.image));
		//                FileInfo file = new FileInfo(filepath);
		//                try
		//                {
		//                    file.Delete();
		//                }
		//                catch{}
		//            }
		//        }
		//        int res = repo.UpdateBookImage(id, null);
		//    }
			
		//    return RedirectToAction ("Edit", "Books", new { id = id });
		//}
		#endregion
	}
}

