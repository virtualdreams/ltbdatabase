using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace LTB_Database.Core
{
	static public class StringHelper
	{
		static public string SafeString(this string str)
		{
			if(String.IsNullOrEmpty(str))
				return "";
			return str;
		}
		
		static public string Escape(this string str)
		{
			if(!String.IsNullOrEmpty(str))
				return str.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\"", "\\\"");
			return str;
		}
		
		static public string Filter(this string str, string filter)
		{
			if (String.IsNullOrEmpty(filter))
				throw new ArgumentNullException("filter");

			if (!String.IsNullOrEmpty(str))
			{
				return Regex.Replace(str, String.Format("[{0}]", filter), "");
			}
			return str;
		}

		//static public string ToBase64(byte[] input)
		//{
		//    return Convert.ToBase64String(input);
		//}

		static public byte[] FromBase64(this string str)
		{
			if(String.IsNullOrEmpty(str))
				throw new ArgumentNullException("str");
			
			return Convert.FromBase64String(str);
		}
	}
}
