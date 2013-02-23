using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LTB_Database.Repository;

namespace LTB_Database.ViewModels
{
	public class CategoriesEditViewModel
	{
		public Category Category
		{
			get;
			set;
		}

		public string Error
		{
			get;
			set;
		}
	}
}



