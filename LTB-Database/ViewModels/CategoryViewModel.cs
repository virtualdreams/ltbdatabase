using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LTB_Database.Core;
using LTB_Database.Core.DataModel;
using LTB_Database.Models;

namespace LTB_Database.ViewModels
{
	public class CategoryViewModel
	{
		public CategoryModel Category
		{
			get;
			set;
		}
		
		public BookModel[] Books
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