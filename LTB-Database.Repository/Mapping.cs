using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LTB_Database.Repository
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
	
	public class Category
	{
		public int id { get; set; }
		public string name { get; set; }
	}
	
	public class Story
	{
		public int id { get; set; }
		public string name { get; set; }
		public int bookid { get; set; }
	}
	
	public class Search
	{
		public string name { get; set; }
	}
}
