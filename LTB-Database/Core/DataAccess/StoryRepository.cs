using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlDataMapper;
using LTB_Database.Core.DataModel;

namespace LTB_Database.Core.DataAccess
{
	public class StoryRepository: RepositoryBase
	{
		public StoryRepository(SqlMapper dal)
		{
			_dal = dal;
		}
		
		#region RepositoryPattern
		
		public Story[] List()
		{
			return _dal.QueryForList<Story>("getStories").ToArray();
		}
		
		public Story Get(int id)
		{
			return _dal.QueryForObject<Story>("getStory", new SqlParameter("storyid", id));
		}

		public int Insert(Story story)
		{
			return _dal.Insert<Story>("insertStory", story);
		}

		public int Update(Story story)
		{
			return _dal.Update<Story>("updateStory", story);
		}

		public int Delete(long id)
		{
			return _dal.Delete("deleteStory", new SqlParameter("storyid", id));
		}
		
		public int Delete(Story story)
		{
			return _dal.Delete<Story>("deleteStory", story);
		}
		
		public int DeleteByBookId(long id)
		{
			return _dal.Delete("deleteStories", new SqlParameter("bookid", id));
		}
		#endregion
	}
}
