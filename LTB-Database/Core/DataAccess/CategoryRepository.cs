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
		
		public Category Get(long id)
		{
			return _dal.QueryForObject<Category>("getCategory", new SqlParameter("catid", id));
		}

		public int Insert(Category category)
		{
			return _dal.Insert<Category>("insertCategory", category);
		}

		public int Update(Category category)
		{
			return _dal.Update<Category>("updateCategory", category);
		}

		public int Delete(Category category)
		{
			return _dal.Delete<Category>("deleteCategory", category);
		}

		public int Delete(long id)
		{
			return _dal.Delete("deleteCategory", new SqlParameter("id", id));
		}
		
		#endregion
	}
}
