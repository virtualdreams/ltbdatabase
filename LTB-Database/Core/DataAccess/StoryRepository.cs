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
			return _dal.QueryForList<Story>("getAllStories").ToArray();
		}
		
		public Story Get(int id)
		{
			return _dal.QueryForObject<Story>("getStory", new SqlParameter("storyid", id));
		}

		public int InsertStory(Story story)
		{
			return _dal.Insert<Story>("insertStory", story);
		}

		public int UpdateStory(Story story)
		{
			return _dal.Update<Story>("updateStory", story);
		}

		public int DeleteStory(int id)
		{
			return _dal.Delete("deleteStory", new SqlParameter("bookid", id));
		}
		
		public int DeleteStory(Story story)
		{
			return _dal.Delete<Story>("deleteStory", story);
		}
		
		#endregion
		
		//public Story[] GetAllStories()
		//{
		//    List<Story> list = _dal.QueryForList<Story>("getAllStories");
			
		//    return list.ToArray();
		//}
		
		//public Story[] GetAllStories(int id)
		//{
		//    List<Story> list = _dal.QueryForList<Story>("getAllStories");
			
		//    return list.Where(x => x.bookid == id).ToArray();
		//}
		
		//public Story GetStory(int id)
		//{
		//    Story obj = _dal.QueryForObject<Story>("getStory", new SqlParameter("storyid", id));
			
		//    return obj;
		//}
		
		//public int GetCount()
		//{
		//    List<Story> list = _dal.QueryForList<Story>("getAllStories");
			
		//    return list.Count;
		//}
		
		//public int GetCount(int id)
		//{
		//    List<Story> list = _dal.QueryForList<Story>("getAllStories");

		//    return list.Where(x => x.bookid == id).Count();
		//}
		
		
	}
}
