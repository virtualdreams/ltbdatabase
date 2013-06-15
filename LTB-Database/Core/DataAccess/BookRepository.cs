using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SqlDataMapper;
using LTB_Database.Core.DataModel;
using System.Web.Mvc;

namespace LTB_Database.Core.DataAccess
{
	public class BookRepository: RepositoryBase
	{
		public BookRepository(SqlMapper dal)
		{
			_dal = dal;
		}
		
		#region RepositoryPattern
		public Book[] List()
		{
			return _dal.QueryForList<Book>("getAllBooks").ToArray();
		}
		
		public Book Get(int id)
		{
			return _dal.QueryForObject<Book>("getBook", new SqlParameter("id", id));
		}

		public int InsertBook(Book book)
		{
			return _dal.Insert<Book>("insertBook", book);
		}

		public int UpdateBook(Book book)
		{
			return _dal.Update<Book>("updateBook", book);
		}

		public int DeleteBook(int id)
		{
			return _dal.Delete("deleteBook", new SqlParameter("bookid", id));
		}

		public int DeleteBook(Book book)
		{
			return _dal.Delete<Book>("deleteBook", book);
		}
		#endregion
		
		#region Extended methods
		
		public Search[] SearchForTerm(string term)
		{
			return _dal.QueryForList<Search>("liveSearch", new SqlParameter("term", String.Format("%{0}%", Helpers.EscapeSpecialCharacters(term)))).ToArray();
		}
		
		#endregion
		
		//public Book[] GetAllBooks()
		//{
		//    List<Book> list = _dal.QueryForList<Book>("getAllBooks");
			
		//    return list.ToArray();
		//}
		
		//public Book[] GetAllBooks(int skip, int items)
		//{
		//    List<Book> list = _dal.QueryForList<Book>("getAllBooks");

		//    return list.Skip(skip).Take(items).ToArray();
		//}

		//public Book[] GetAllBooks(int category, int skip, int items)
		//{
		//    List<Book> list = _dal.QueryForList<Book>("getAllBooks");

		//    return list.Where(x => x.catid == category).Skip(skip).Take(items).ToArray();
		//}
		
		//public Book[] GetLatest(int items)
		//{
		//    List<Book> list = _dal.QueryForList<Book>("getAllBooks");
			
		//    return list.OrderByDescending(x => x.added).Take(items).ToArray();
		//}
		
		//public int GetCount()
		//{
		//    List<Book> list = _dal.QueryForList<Book>("getAllBooks");

		//    return list.Count;
		//}

		//public int GetCount(int id)
		//{
		//    List<Book> list = _dal.QueryForList<Book>("getAllBooks");

		//    return list.Where(x => x.catid == id).Count();
		//}
		
		//public Book GetBook(int id)
		//{
		//    Book obj = _dal.QueryForObject<Book>("getBook", new SqlParameter("bookid", id));
			
		//    return obj;
		//}
		
		
		
		
	}
}
