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
			return _dal.QueryForList<Book>("getBooks").ToArray();
		}
		
		public Book Get(long id)
		{
			return _dal.QueryForObject<Book>("getBook", new SqlParameter("id", id));
		}

		public int Insert(Book book)
		{
			return _dal.Insert<Book>("insertBook", book);
		}

		public int Update(Book book)
		{
			return _dal.Update<Book>("updateBook", book);
		}

		public int Delete(long id)
		{
			return _dal.Delete("deleteBook", new SqlParameter("id", id));
		}

		public int Delete(Book book)
		{
			return _dal.Delete<Book>("deleteBook", book);
		}
		#endregion
		
		#region Extended methods
		
		public int SetImage(long id, string image)
		{
			SqlParameter param = new SqlParameter("id", id);
			param.Add("image", image);
			
			return _dal.Update("updateBookSetImage", param);
		}
		
		public Search[] LiveSearch(string query)
		{
			return _dal.QueryForList<Search>("getLiveSearch", new SqlParameter("term", String.Format("%{0}%", query))).ToArray();
		}

		public Book[] Search(string query)
		{
			return _dal.QueryForList<Book>("getBookSearch", new SqlParameter("term", String.Format("%{0}%", query))).ToArray();
		}
		
		#endregion
	}
}
