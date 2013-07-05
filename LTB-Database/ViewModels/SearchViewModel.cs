using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LTB_Database.Models;
using LTB_Database.Core.DataModel;
using LTB_Database.Core;

namespace LTB_Database.ViewModels
{
    public class SearchViewModel
    {
        public string Query
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