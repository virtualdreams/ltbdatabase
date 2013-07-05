using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LTB_Database.Core;
using LTB_Database.Core.DataModel;
using LTB_Database.Models;

namespace LTB_Database.ViewModels
{
	/// <summary>
	/// ViewModel to view a selection of categories
	/// </summary>
    public class CategoriesViewModel
    {
		public CategoryModel[] Categories
		{ 
			get; 
			set; 
		}
    }
}

