using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LTB_Database.Models;

namespace LTB_Database.ViewModels
{
    public class ViewBookViewModel
    {
		public BookModel Book
		{ 
			get; 
			set; 
		}
		
		public CategoryModel Category
		{
			get;
			set;
		}
    }
}

