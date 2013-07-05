using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Collections.Generic;
using System.Linq;

using LTB_Database.Models;

namespace LTB_Database.Core
{
	static public class HtmlHelpers
	{
		public static string Pager(this HtmlHelper helper, string actionName, string controllerName, object routeValues, Pager pager)
		{
			//only one
			TagBuilder div = new TagBuilder("div");
			div.MergeAttribute("class", "pager");
			
			//only one
			TagBuilder ul = new TagBuilder("ul");
			ul.MergeAttribute("class", "pager-list");
			
			//multiple items
			TagBuilder li = new TagBuilder("li");
			li.MergeAttribute("class", "pager-item");
			
			//multiple items
			TagBuilder a = new TagBuilder("a");
			
			StringBuilder sb = new StringBuilder();
			
			RouteValueDictionary dict = new RouteValueDictionary(routeValues);
			
			UrlHelper uh = new UrlHelper(helper.ViewContext.RequestContext);

			//do it
			if(pager.HasPrevPage)
			{
				a.MergeAttribute("href", uh.Action(actionName, controllerName,  dict.Merge(new { page = 1 })), true);
				a.SetInnerText("Erste");
				li.InnerHtml = a.ToString();
				sb.AppendLine(li.ToString());

				a.MergeAttribute("href", uh.Action(actionName, controllerName, dict.Merge(new { page = pager.PageIndex - 1 })), true);
				a.SetInnerText("« Zurück");
				li.InnerHtml = a.ToString();
				sb.AppendLine(li.ToString());
			}

			for (long page = ((pager.PageIndex - 3) <= 0 ? 1 : pager.PageIndex - 3); page <= ((pager.PageIndex + 3) >= pager.TotalPages ? pager.TotalPages : pager.PageIndex + 3); ++page)
			{
				a.MergeAttribute("href", uh.Action(actionName, controllerName, dict.Merge(new { page = page })), true);
				a.SetInnerText(page.ToString());
				li.InnerHtml = a.ToString();
				sb.AppendLine(li.ToString());
			}

			if (pager.HasNextPage)
			{
				a.MergeAttribute("href", uh.Action(actionName, controllerName, dict.Merge(new { page = pager.PageIndex + 1 })), true);
				a.SetInnerText("Weiter »");
				li.InnerHtml = a.ToString();
				sb.AppendLine(li.ToString());

				a.MergeAttribute("href", uh.Action(actionName, controllerName, dict.Merge(new { page = pager.TotalPages })), true);
				a.SetInnerText("Letzte");
				li.InnerHtml = a.ToString();
				sb.AppendLine(li.ToString());
			}
			
			ul.InnerHtml = sb.ToString();
			
			div.InnerHtml = ul.ToString();
			
			string ret = div.ToString();
			
			return ret;
		}
    }

	public static class Util
	{
		public static RouteValueDictionary Extend(this RouteValueDictionary dest, IEnumerable<KeyValuePair<string, object>> src)
		{
			src.ToList().ForEach(x => { dest[x.Key] = x.Value; });
			return dest;
		}
		
		public static RouteValueDictionary Merge(this RouteValueDictionary source, IEnumerable<KeyValuePair<string, object>> newData)
		{
			return (new RouteValueDictionary(source)).Extend(newData);
		}
		
		public static RouteValueDictionary Merge(this RouteValueDictionary source, object newData)
		{
			RouteValueDictionary rvd = new RouteValueDictionary(newData);
			return source.Merge(rvd);
		}
	}
}