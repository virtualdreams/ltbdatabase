using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LTB_Database.Models;
using SqlDataMapper;
using System.Web.Mvc;
using LTB_Database.ViewModels;
using LTB_Database.Core.DataAccess;
using LTB_Database.Core.DataModel;

namespace LTB_Database.Core
{
	public class BooksService
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(BooksService));
		private SqlMapper _dal = null;
		private IValidationDictionary _validatonDictionary = null;
		
		private int _itemsPerShelf = 12;
		private int _itemsPerList = 50;
		
		public BooksService(IValidationDictionary validationDictionary)
		{
			_dal = new SqlMapper(GlobalConfig.Get().SqlMapperConfig);
			_validatonDictionary = validationDictionary;	
		}
		
		public BooksIndexViewModel Index(int? page)
		{
			BookRepository bRepo = new BookRepository(_dal);

			//create a pager
			if (page == null || page == 0)
			{
				page = 1;
			}
			var pager = new Pager((int)page, _itemsPerShelf, bRepo.List().Count());
			page = (int)pager.SetPageIndexToLimit((int)page);
			
			var viewData = new BooksIndexViewModel();
			viewData.Pager = pager;
			viewData.Books = bRepo.List().Skip((int)(page * _itemsPerShelf) - _itemsPerShelf).Take(_itemsPerShelf).ToArray();
			
			return viewData;
		}
		
		private int GetItemsFromLayout(string layout)
		{
			if (!String.IsNullOrEmpty(layout))
			{
				if (layout.Equals("shelf"))
				{
					return _itemsPerShelf;
				}
				if (layout.Equals("list"))
				{
					return _itemsPerList;
				}
			}
			return _itemsPerShelf;
		}
		
		public ViewCategoryModel GetAllBooksFromCategory(int categoryId, int? page, string layout)
		{
			BookRepository bRepo = new BookRepository(_dal);
			CategoryRepository cRepo = new CategoryRepository(_dal);
			
			int items = GetItemsFromLayout(layout);
			
			//create a pager
			if (page == null || page == 0)
			{
				page = 1;
			}
			var pager = new Pager((int)page, items, bRepo.List().Where(c => c.catid == categoryId).Count());
			page = (int)pager.SetPageIndexToLimit((int)page);
			
			var viewData = new ViewCategoryModel();
			viewData.Pager = pager;
			viewData.Category = cRepo.Get(categoryId);
			viewData.Books = bRepo.List().Where(c => c.catid == categoryId).Skip((int)(page * _itemsPerShelf) - _itemsPerShelf).Take(_itemsPerShelf).ToArray();
			
			return viewData;
		}
		
		public ViewBookModel View(int? id)
		{
			BookRepository bRepo = new BookRepository(_dal);
			StoryRepository sRepo = new StoryRepository(_dal);

			if (id == null || id == 0)
				throw new HttpException(404, "Ressource not found");
			
			var viewData = new ViewBookModel();
			viewData.Book = bRepo.Get((int)id);
			viewData.Stories = sRepo.List().Where(s => s.bookid == (int)id).ToArray();

			if (viewData.Book == null)
				throw new HttpException(404, "Ressource not found");
			
			return viewData;
		}
		
		public BooksEditViewModel RebuildAdd()
		{
			CategoryRepository cRepo = new CategoryRepository(_dal);
			
			var viewData = new BooksEditViewModel();
			viewData.Book = new Book();
			viewData.Stories = new Story[0];
			viewData.Categories = cRepo.List();
			
			return viewData;
		}
		
		public BooksEditViewModel Edit(int? id)
		{
			if(id == null || id == 0)
				return RebuildAdd();
			
			BookRepository bRepo = new BookRepository(_dal);
			StoryRepository sRepo = new StoryRepository(_dal);
			CategoryRepository cRepo = new CategoryRepository(_dal);

			var viewData = new BooksEditViewModel();
			viewData.Book = bRepo.Get((int)id);
			viewData.Stories = sRepo.List().Where(s => s.bookid == (int)id).ToArray();
			viewData.Categories = cRepo.List();

			return viewData;
		}
		
		public BooksEditViewModel Save(Book book, string stories, HttpPostedFileBase image)
		{
			_validatonDictionary.Clear();
			
			#region validation
			if(String.IsNullOrEmpty(book.name))
				_validatonDictionary.AddError("name", "Dieses Feld ist ein Pflichtfeld.");

			if(book.number == 0)
				_validatonDictionary.AddError("number", "Dieses Feld ist ein Pflichtfeld.");
			#endregion	
		
			#region stories
			string[] storyList = stories.Replace("\t", "").Replace("\r", "").Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
			List<Story> story_names = new List<Story>();
			foreach (var name in storyList)
			{
				Story story = new Story();
				story.name = name.Trim();
				story.bookid = book.id;
				story_names.Add(story);
			}
			#endregion
			
			#region insert or update a book
			if(_validatonDictionary.IsValid)
			{
				int res = 0;
				if(book.id == 0)
				{
					res = InsertBook(book, story_names.ToArray());
					book.id = res;
				}
				else
				{
				    //res = UpdateBook(book, story_names.ToArray();
				}
			}
			#endregion
			
			
			CategoryRepository cRepo = new CategoryRepository(_dal);
			
			var viewData = new BooksEditViewModel();
			viewData.Book = book;
			viewData.Stories = story_names.ToArray();
			viewData.Categories = cRepo.List();
			
			return viewData;
		}

		public BooksDeleteViewModel Delete(int? id, bool removeFromDatabase)
		{
			BookRepository bRepo = new BookRepository(_dal);
			StoryRepository sRepo = new StoryRepository(_dal);
			
			if(id == null || id == 0)
				throw new HttpException(404, "Ressource not found");
			
			var viewData = new BooksDeleteViewModel();
			viewData.Book = bRepo.Get((int)id);
			
			if(viewData.Book == null)
				throw new HttpException(404, "Ressource not found");
				
			if(removeFromDatabase)
			{
				_dal.BeginTransaction();
				int bRes = 0;
				bRes = bRepo.DeleteBook((int)id);
				
				int sRes = 0;
				sRes = sRepo.DeleteStory((int)id);

				//if (!String.IsNullOrEmpty(book.image))
				//{
				//    string filepath = HttpContext.Server.MapPath(String.Format("{0}{1}", GlobalConfig.Get().ImagePath, book.image));
				//    FileInfo file = new FileInfo(filepath);
				//    try
				//    {
				//        file.Delete();
				//    }
				//    catch { }
				//}
				_dal.CommitTransaction();
			}	
			
			
			return viewData;
		}
		
		private int InsertBook(Book book, Story[] stories)
		{
			BookRepository bRepo = new BookRepository(_dal);
			StoryRepository sRepo = new StoryRepository(_dal);
			
			_dal.BeginTransaction();
			try
			{
				List<int> ids = new List<int>();
				bRepo.InsertBook(book);
				int bookid = bRepo.GetLastInsertId();
				
				foreach(Story story in stories)
				{
					Story s = story;
					s.bookid = bookid;
					sRepo.InsertStory(s);
					int storyid = sRepo.GetLastInsertId();
					ids.Add(storyid);
				}

				_dal.CommitTransaction();
				return bookid;
			}
			catch (Exception ex)
			{
				_dal.RollbackTransaction();
			}
			
			return 0;
		}
		
		private int UpdateBook(Book book, Story[] stories)
		{
			BookRepository bRepo = new BookRepository(_dal);
			StoryRepository sRepo = new StoryRepository(_dal);
			
			_dal.BeginTransaction();
			try
			{
				int uRes = 0;
				uRes = bRepo.UpdateBook(book);
				
				int sRes = 0;
				sRes = sRepo.DeleteStory(book.id);
				
				//re-add the stories
				
				
				
				_dal.CommitTransaction();
			}
			catch(Exception ex)
			{
				_dal.RollbackTransaction();
			}
			return 0;
		}
	}
}
