using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LTB_Database.Models;

namespace LTB_Database.ViewModels
{
	public class EditBookViewModel
	{
		public BookModel Book
		{
			get;
			set;
		}

		public CategoryModel[] Categories
		{
			get;
			set;
		}
	}
}