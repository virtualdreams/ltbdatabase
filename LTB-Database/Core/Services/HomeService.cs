using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlDataMapper;
using System.Web.Mvc;
using LTB_Database.Core.DataAccess;
using LTB_Database.ViewModels;
using LTB_Database.Core.DataModel;
using LTB_Database.Models;

namespace LTB_Database.Core
{
	public class HomeService
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(HomeService));
		private SqlMapper _dal = null;
		private IValidationDictionary _validatonDictionary = null;

		public HomeService(IValidationDictionary validationDictionary)
		{
			_dal = new SqlMapper(GlobalConfig.Get().SqlMapperConfig);	
			_validatonDictionary = validationDictionary;	
		}
		
		public BookModel[] GetLatestBooks(int count)
		{
			BookRepository bRepo = new BookRepository(_dal);
			
			Book[] books = bRepo.List().OrderByDescending(x => x.added).Take(count).ToArray();
			
			List<BookModel> bookModel = new List<BookModel>();
			
			foreach(Book book in books)
			{
				bookModel.Add(new BookModel 
				{ 
					Id = book.id,
					Name = book.name,
					Number = book.number, 
					CategoryId = book.catid 
				});
			}
			
			return bookModel.ToArray();
		}
		
		public Search[] AjaxSearch(string query)
		{	
			BookRepository bRepo = new BookRepository(_dal);

			string q = query.Filter(@"%\^#").Escape().Trim();
			
			Search[] terms =  bRepo.LiveSearch(q);
			return terms;
		}
		
		public long StoryCount()
		{
			StoryRepository sRepo = new StoryRepository(_dal);
			return sRepo.List().Count();
		}
		
		public long BookCount()
		{
			BookRepository rRepo = new BookRepository(_dal);
			return rRepo.List().Count();
		}
	}
}
