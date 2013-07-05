using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTB_Database.Core.DataModel
{
	public class Book
	{
		public long id { get; set; }
		public string name { get; set; }
		public long number { get; set; }
		public long catid { get; set; }
		public string image { get; set; }
		public DateTime added { get; set; }
		public string category { get; set; }
	}
}
