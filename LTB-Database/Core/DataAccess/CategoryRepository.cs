using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SqlDataMapper;
using LTB_Database.Core.DataModel;

namespace LTB_Database.Core.DataAccess
{
	public class CategoryRepository: RepositoryBase
	{
		public CategoryRepository(SqlMapper dal)
		{
			_dal = dal;
		}
		
		#region RepositoryPattern
		
		public Category[] List()
		{
			return _dal.QueryForList<Category>("getCategories").ToArray();
		}
		
		public Category Get(int id)
		{
			return _dal.QueryForObject<Category>("getCategory", new SqlParameter("catid", id));
		}

		public int InsertCategory(Category category)
		{
			return _dal.Insert<Category>("insertCategory", category);
		}

		public int UpdateCategory(Category category)
		{
			return _dal.Update<Category>("updateCategory", category);
		}

		public int DeleteCategory(Category category)
		{
			return _dal.Delete<Category>("deleteCategorie", category);
		}

		public int DeleteCategory(int id)
		{
			return _dal.Delete("deleteCategorie", new SqlParameter("catid", id));
		}
		
		#endregion
	}
}
