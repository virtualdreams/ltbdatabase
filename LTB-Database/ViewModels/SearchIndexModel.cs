using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LTB_Database.Repository;
using LTB_Database.Models;

namespace LTB_Database.ViewModels
{
    public class SearchIndexModel
    {
        public string Query { get; set; }
		public List<Book> Books { get; set; }
		public Pager Pager { get; set; }
    }
}