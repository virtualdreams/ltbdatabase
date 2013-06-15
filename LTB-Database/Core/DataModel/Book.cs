using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTB_Database.Core.DataModel
{
	public class Book
	{
		public int id { get; set; }
		public string name { get; set; }
		public int number { get; set; }
		public int catid { get; set; }
		public string image { get; set; }
		public DateTime added { get; set; }
		public string category { get; set; }
	}
}
