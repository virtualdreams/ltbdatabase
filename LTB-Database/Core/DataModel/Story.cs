using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTB_Database.Core.DataModel
{
	public class Story
	{
		public long id { get; set; }
		public string name { get; set; }
		public long bookid { get; set; }
	}
}
