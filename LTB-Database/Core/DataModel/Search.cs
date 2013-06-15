using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlDataMapper;

namespace LTB_Database.Core.DataModel
{
	public class Search
	{
		[SqlMapperAttributes("name")]
		public string label { get; set; }
	}
}
