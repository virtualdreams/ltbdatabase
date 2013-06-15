using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlDataMapper;

namespace LTB_Database.Core.DataAccess
{
	public class SearchRepository: RepositoryBase
	{
		public SearchRepository(SqlMapper dal)
		{
			_dal = dal;
		}
	}
}
