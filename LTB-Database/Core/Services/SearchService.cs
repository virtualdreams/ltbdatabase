using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SqlDataMapper;

using LTB_Database.ViewModels;
using LTB_Database.Core.DataAccess;
using LTB_Database.Core.DataModel;
using LTB_Database.Models;

namespace LTB_Database.Core
{
	public class SearchService
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(SearchService));
		private SqlMapper _dal = null;
		private IValidationDictionary _validatonDictionary = null;

		public SearchService(IValidationDictionary validationDictionary)
		{
			_dal = new SqlMapper(GlobalConfig.Get().SqlMapperConfig);
			_validatonDictionary = validationDictionary;	
		}
		
		public BookModel[] Search(string query)
		{
			BookRepository bRepo = new BookRepository(_dal);
			
			string q = query.Filter(@"%\^#").Escape().Trim();
			
			Book[] books = bRepo.Search(q);
			
			List<BookModel> bookModel = new List<BookModel>();
			foreach(Book book in books)
			{
				bookModel.Add(new BookModel
				{
					Id = book.id,
					Name = book.name,
					Number = book.number,
					CategoryId = book.catid,
					Image = book.image,
					Added = book.added
				});
			}
			
			return bookModel.ToArray();
		}
	}
}
