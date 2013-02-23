﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LTB_Database.Repository;

namespace LTB_Database.ViewModels
{
    public class HomeIndexModel
    {
        public List<Book> Latest { get; set; }
        public List<Category> Categories { get; set; }
    }
}