using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlDataMapper;

namespace LTB_Database.Core.DataAccess
{
	public class RepositoryBase
	{
		protected SqlMapper _dal = null;
		
		public int GetLastInsertId()
		{
			return _dal.QueryForScalar<int>("getLastInsertId");
		}
	}
}
