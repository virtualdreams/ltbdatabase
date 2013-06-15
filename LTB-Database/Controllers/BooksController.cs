using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Diagnostics;

using LTB_Database.Models;
using LTB_Database.ViewModels;
using LTB_Database.Repository;
using LTB_Database.Filters;
using LTB_Database.Core.DataModel;
using LTB_Database.Core;

namespace LTB_Database
{
	[GlobalExceptionFilter]
	[GlobalActionFilter]
	public class BooksController : Controller
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger (typeof(BooksController));
		private readonly BooksService _service;
		
		public BooksController()
		{
			_service = new BooksService(new ModelStateWrapper(this.ModelState));
		}
		
		//[HandleError(Master="LTBDB.Master", View="Index")]
		public ActionResult Index(int? page)
		{
			return View(_service.Index(page)); 
		}

		[HttpGet]
		public ActionResult Category(int id, int? page, string layout)
		{
			return View(_service.GetAllBooksFromCategory(id, page, layout));
			
			//TODO - find a methos to switch to list-view
			//if (shelf)
			//    return View(view);
			//else
			//    return View("List", view);
		}
		
		[HttpGet]
		public ActionResult View(int? id)
		{
			return View(_service.View(id));
		}
		
		[HttpGet]
		public ActionResult Create()
		{
			return View("Edit", _service.RebuildAdd());
		}

		[HttpGet]
		public ActionResult Edit(int? id)
		{
			return View(_service.Edit(id));
		}
		
		[GlobalActionFilter]
		[HttpPost]
		public ActionResult Edit(Book book, string stories, HttpPostedFileBase imagefile)
		{
			var viewData = _service.Save(book, stories, imagefile);
			
			//TODO - do it better, that's shit ;)
			int bookid = viewData.Book.id;
			if(bookid != 0)
				return RedirectToAction("View", "Books", new { id = bookid });
			
			return View(viewData);
		}
		
		[HttpGet]
		public ActionResult Delete(int id)
		{
			return View(_service.Delete(id, false));
		}

		[HttpPost]
		public ActionResult Delete(int id, FormCollection form)
		{
			_service.Delete(id, true);
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

