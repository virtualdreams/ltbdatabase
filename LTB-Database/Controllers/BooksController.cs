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

namespace LTB_Database
{
	public class BooksController : Controller
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger (typeof(BooksController));
		private LtbRepository repo = new LtbRepository();
		
		public BooksController()
		{
			
		}
		
		//[HandleError(Master="LTBDB.Master", View="Index")]
		public ActionResult Index(int? page)
		{
			if (page == null || page == 0)
			{
				page = 1;
			}

			int items = 12;

			var pager = new Pager((int)page, items, repo.GetBookCount());

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

			var books = repo.GetBooks(items, (int)page);
			
			var view = new BooksIndexViewModel { Books = books, Pager = pager };
			
			return View(view); 
		}

		[HttpGet]
		public ActionResult Category(int id, int? page, string layout)
		{
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

			var pager = new Pager((int)page, items, repo.GetBookCountForCategory(id));

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

			var categorie = repo.GetCategory(id);
			var books = repo.GetBooksFromCategory(items, (int)page, id);

			var view = new ViewCategoryModel {
				Books = books,
				Pager = pager,
				Category = categorie
			};

			if (shelf)
				return View(view);
			else
				return View("List", view);
		}
		
		[HttpGet]
		public ActionResult View(int id)
		{
			var book = repo.GetBook(id);
			var stories = repo.GetStories(id);
			
			if(book == null)
				throw new HttpException(404, "Resource not found.");
			
			var view = new ViewBookModel { Book = book, Stories = stories };
			
			return View(view);
		}
		
		[HttpGet]
		public ActionResult Create()
		{
			var book = new Book();
			var stories = new List<Story>();
			var categories = repo.GetCategories();

			var view = new BooksEditViewModel { Book = book, Stories = stories, Categories = categories };
			
			return View("Edit", view);
		}

		[HttpGet]
		public ActionResult Edit(int? id)
		{
			if(id == null || id == 0)
				throw new HttpException(404, "Resource not found.");
				
			var book = repo.GetBook((int)id);
			var stories = repo.GetStories((int)id);
			var categories = repo.GetCategories();
			
			var view = new BooksEditViewModel { Book = book, Stories = stories, Categories = categories };
			
			return View(view);
		}
		
		[HttpPost]
		public ActionResult Edit(int id, string name, string number, string category/* FormCollection form*/)
		{
			int retid = 0;
			
			if(String.IsNullOrEmpty(name))
			    ModelState.AddModelError("name", "Bitte geben Sie einen Namen an.");
				
			if(String.IsNullOrEmpty(number))
			    ModelState.AddModelError("number", "Bitte geben Sie einen Nummer an.");
			
			if(ModelState.IsValid)
			{	
				if(id != 0)
				{
					int res = repo.UpdateBook(id, name, Int32.Parse(number), Int32.Parse(category));
					retid = id;
					return RedirectToAction("Edit", "Books", new { id = retid });
				}
				else
				{
					int ret = 0;
					int res = repo.InsertBook(name, Int32.Parse(number), Int32.Parse(category), out ret);
					retid = ret;
					return RedirectToAction("Edit", "Books", new { id = retid });
				}
			}
		
			var book = repo.GetBook((int)id);
			var stories = repo.GetStories((int)id);
			var categories = repo.GetCategories();

			var view = new BooksEditViewModel { Book = book, Stories = stories, Categories = categories };
			
			return View(view);
		}
		
		[HttpPost]
		public ActionResult Stories(int id, string stories)
		{
			string[] names = stories.Replace("\t", "").Replace("\r", "").Split(new char[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);
			List<Story> story_names = new List<Story>();
			foreach(var name in names)
			{
				Story story = new Story();
				story.name = name.Trim();
				story.bookid = id;
				story_names.Add(story);
			}
			
			int res = repo.InsertStory(ref story_names);

			return RedirectToAction("Edit", "Books", new { id = id });
		}
		
		[HttpGet]
		public ActionResult Delete(int id)
		{
			var book = repo.GetBook(id);
			
			var view = new BooksDeleteViewModel { Book = book };
			
			return View(view);
		}

		[HttpPost]
		public ActionResult Delete(int id, FormCollection form)
		{
			Book book = repo.GetBook(id);
			int res = repo.DeleteBook(id);
			res = repo.DeleteStories(id);
			
			if(book != null)
			{
				if (!String.IsNullOrEmpty(book.image))
				{
					string filepath = HttpContext.Server.MapPath(String.Format("{0}{1}", GlobalConfig.Get().ImagePath, book.image));
					FileInfo file = new FileInfo(filepath);
					try
					{
						file.Delete();
					}
					catch { }
				}
			}

			return RedirectToAction("Index", "Home");
		}
		
		[HttpPost]
		public ActionResult Upload(int id, HttpPostedFileBase image)
		{
			if(image != null)
			{
				log.DebugFormat("Image mime-type: {0}", image.ContentType);
				///mime check
				string[] mime_types = GlobalConfig.Get().AllowMimeType.Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries);
				if(!mime_types.Contains(image.ContentType))
					return RedirectToAction("Edit", "Books", new { id = id });
				
				///filesize check
				
				
				string filename = String.Format("img_{0}{1}", id, Path.GetExtension(image.FileName));
				string filepath = HttpContext.Server.MapPath(String.Format("{0}{1}", GlobalConfig.Get().ImagePath, filename));
				image.SaveAs(filepath);
				log.DebugFormat("Imagepath: {0}", filepath);
				
				int res = repo.UpdateBookImage(id, filename);
				log.DebugFormat("Update image -> database returns: {0}", res);
			}
			else
			{
				Book book = repo.GetBook(id);
				if(book != null)
				{
					if(!String.IsNullOrEmpty(book.image))
					{
						string filepath = HttpContext.Server.MapPath(String.Format("{0}{1}", GlobalConfig.Get().ImagePath, book.image));
						FileInfo file = new FileInfo(filepath);
						try
						{
							file.Delete();
						}
						catch{}
					}
				}
				int res = repo.UpdateBookImage(id, null);
			}
			
			return RedirectToAction ("Edit", "Books", new { id = id });
		}
	}
}

