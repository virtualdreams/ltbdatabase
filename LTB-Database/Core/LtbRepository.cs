using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SqlDataMapper;
using System.Diagnostics;
using System.Web.Mvc;
using LTB_Database.Core.DataModel;

namespace LTB_Database.Repository
{
	public class LtbRepository
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(LtbRepository));
		
		private SqlMapper m_Mapper = null;
		
		public LtbRepository()
		{
			try
			{
				m_Mapper = new SqlMapper(GlobalConfig.Get().SqlMapperConfig);
			}
			catch(Exception ex)
			{
				log.Fatal(ex);
				throw;
			}
		}

		/// <summary>
		/// Escape special chars
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public string Escape(string str)
		{
			return str.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\"", "\\\"");
		}
		
		public List<string> GetLiveSearch(string term)
		{
			if(String.IsNullOrEmpty(term))
				throw new ArgumentNullException("term");
			
			SqlParameter param = new SqlParameter();
			param.Add("term", String.Format("%{0}%", Escape(term)));

			//Debug.WriteLine("Ajax: " + m_Mapper.GetStatement("liveSearch", param));
			
			List<Search> result = m_Mapper.QueryForList<Search>("liveSearch", param);
			
			List<string> ret = new List<string>();
			foreach(var name in result)
			{
				ret.Add(name.label);
			}
			return ret;
		}
		
		public List<Book> GetSearchBook(string term, int items, int page)
		{
			if (String.IsNullOrEmpty(term))
				throw new ArgumentNullException("term");
			
			SqlParameter param = new SqlParameter();
			param.Add("term", String.Format("%{0}%", Escape(term)));
			param.Add("items", items);
			param.Add("page", (page * items) - items);
			
			//Debug.WriteLine("Search: " + m_Mapper.GetStatement("searchBook", param));

			List<Book> result = m_Mapper.QueryForList<Book>("searchBook", param);

			return result;
		}
		
		public int GetBookCount()
		{
			return m_Mapper.QueryForScalar<int>("getBookCount");
		}

		public int GetBookCountForCategory(int catid)
		{
			SqlParameter param = new SqlParameter();
			param.Add("catid", catid);

			return m_Mapper.QueryForScalar<int>("getBookCountForCategory", param);
		}
		
		public int GetBookCountForSearch(string term)
		{
			SqlParameter param = new SqlParameter();
			param.Add("term", String.Format("%{0}%", Escape(term)));

			//Debug.WriteLine("Count: " + m_Mapper.GetStatement("getBookCountForSearch", param));
			
			return m_Mapper.QueryForScalar<int>("getBookCountForSearch", param);
		}
		
		public int GetStoryCount()
		{
			return m_Mapper.QueryForScalar<int>("getStoryCount");
		}
		
		public List<Book> GetLatestBooks()
		{
			List<Book> result = m_Mapper.QueryForList<Book>("getLatestEntries");

			return result;
		}
		
		public List<Book> GetBooks(int items, int page)
		{
			SqlParameter param = new SqlParameter();
			param.Add("items", items);
			param.Add("page", (page * items) - items);

			List<Book> result = m_Mapper.QueryForList<Book>("getBooks", param);
			
			//Debug.WriteLine(m_Mapper.GetStatement("getBooks", param));

			return result;
		}
		
		public List<Category> GetCategories()
		{
			List<Category> result = m_Mapper.QueryForList<Category>("getCategories");

			return result;
		}
		
		public Category GetCategory(int catid)
		{
			if(catid == 0)
			{
				return new Category();
			}
				
			SqlParameter param = new SqlParameter();
			param.Add("catid", catid);
			
			Category result = m_Mapper.QueryForObject<Category>("getCategory", param);

			return result;
		}
		
		public List<Book> GetBooksFromCategory(int items, int page, int catid)
		{
			SqlParameter param = new SqlParameter();
			param.Add("items", items);
			param.Add("page", (page * items) - items);
			param.Add("catid", catid);

			List<Book> result = m_Mapper.QueryForList<Book>("getBooksFromCategory", param);

			return result;
		}
		
		public Book GetBook(int bookid)
		{
			if(bookid == 0)
			{
				return new Book();
			}
			
			SqlParameter param = new SqlParameter();
			param.Add("bookid", bookid);

			Book result = m_Mapper.QueryForObject<Book>("getBook", param);

			return result;
		}
		
		public int InsertBook(string name, int number, int catid, out int id)
		{
			SqlParameter param = new SqlParameter();
			param.Add("name", Escape(name));
			param.Add("number", number);
			param.Add("catid", catid);
			param.Add("added", DateTime.Now);
			
			int result = m_Mapper.Insert("insertBook", param);
			id = m_Mapper.QueryForScalar<int>("getLastInsertId");
			
			return result;
		}
		
		public int UpdateBook(int bookid, string name, int number, int catid)
		{
			SqlParameter param = new SqlParameter();
			param.Add("id", bookid);
			param.Add("name", Escape(name));
			param.Add("number", number);
			param.Add("catid", catid);
			
			int result = m_Mapper.Update("updateBook", param);
			
			return result;
		}
		
		public int UpdateBookImage(int bookid, string name)
		{
			SqlParameter param = new SqlParameter();
			param.Add("id", bookid);
			param.Add("image", name);
			
			int result = m_Mapper.Update("updateBookImage", param);
			
			return result;
		}
		
		public int DeleteBook(int bookid)
		{
			SqlParameter param = new SqlParameter();
			param.Add("id", bookid);

			int result = m_Mapper.Delete("deleteBook", param);

			return result;
		}
		
		public List<Story> GetStories(int bookid)
		{
			if(bookid == 0)
			{
				return new List<Story>();
			}
			
			SqlParameter param = new SqlParameter();
			param.Add("bookid", bookid);
			
			List<Story> result = m_Mapper.QueryForList<Story>("getStories", param);
			
			return result;
		}
		
		public int InsertStory(ref List<Story> stories)
		{
			m_Mapper.BeginTransaction();
			int result = 0;
			foreach(Story story in stories)
			{
				result += m_Mapper.Insert("insertStory", story);
				story.id = m_Mapper.QueryForScalar<int>("getLastInsertId");
			}
			m_Mapper.CommitTransaction();
			
			return result;
		}
		
		public int DeleteStories(int bookid)
		{
			SqlParameter param = new SqlParameter();
			param.Add("id", bookid);
			
			int result = m_Mapper.Delete("deleteStories", param);
			
			return result;
		}
		
		public int InsertCategory(string name, out int id)
		{
			SqlParameter param = new SqlParameter();
			param.Add("name", Escape(name));
			
			int result = m_Mapper.Insert("insertCategory", param);
			id = m_Mapper.QueryForScalar<int>("getLastInsertId");
			
			return result;
		}
		
		public int UpdateCategory(int catid, string name)
		{
			SqlParameter param = new SqlParameter();
			param.Add("id", catid);
			param.Add("name", Escape(name));
			
			int result = m_Mapper.Update("updateCategory", param);
			
			return result;
		}
		
		public int DeleteCategory(int catid)
		{
			SqlParameter param = new SqlParameter();
			param.Add("id", catid);
			
			int result = m_Mapper.Delete("deleteCategory", param);
			
			return result;
		}
	}
}
