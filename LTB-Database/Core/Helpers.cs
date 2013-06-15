using System;
using LTB_Database;
using LTB_Database.Repository;

namespace System.Web.Mvc
{
	public static class Helpers
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Helpers));

		//public static string ToBase64(string input)
		//{
		//    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(input);
		//    return Convert.ToBase64String(buffer);
		//}

		public static string ToBase64(byte[] input)
		{
			return Convert.ToBase64String(input);
		}
		
		public static byte[] FromBase64(string input)
		{
			return Convert.FromBase64String(input);
		}
		
		public static string EscapeSpecialCharacters(string value)
		{
			return value.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\"", "\\\"");
		}
	}
}

