using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LTB_Database.Models;
using LTB_Database.Repository;

namespace LTB_Database.ViewModels
{
	public class BooksIndexViewModel
	{
		public List<Book> Books
		{
			get;
			set;
		}
		public Pager Pager
		{
			get;
			set;
		}
 	}
}

