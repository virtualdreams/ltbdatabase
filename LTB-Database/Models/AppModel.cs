using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Diagnostics;

namespace LTB_Database.Models
{
	public static class AppModel
	{
		public static string Version
		{
			get
			{
				Assembly asm = Assembly.GetExecutingAssembly();
				FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(asm.Location);
				return String.Format("v{0}", fvi.ProductVersion);
			}
		}
	}
}
