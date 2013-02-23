using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LTB_Database.Models;
using LTB_Database.Repository;

namespace LTB_Database.ViewModels
{
	public class StatisticViewModel
	{
		public long Books { get; set; }
		public long Stories { get; set; }
	}
}
