﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LTB_Database.Repository;
using LTB_Database.Core.DataModel;

namespace LTB_Database.ViewModels
{
    public class HomeIndexModel
    {
        public Book[] Latest
        { 
			get; 
			set; 
		}
    }
}