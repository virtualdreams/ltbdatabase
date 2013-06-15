using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlDataMapper;
using System.Web.Mvc;
using LTB_Database.Core.DataAccess;
using LTB_Database.ViewModels;
using LTB_Database.Core.DataModel;
using LTB_Database.Models;

namespace LTB_Database.Core
{
	public class HomeService
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(HomeService));
		private SqlMapper _dal = null;
		private IValidationDictionary _validatonDictionary = null;

		public HomeService(IValidationDictionary validationDictionary)
		{
			_dal = new SqlMapper(GlobalConfig.Get().SqlMapperConfig);	
			_validatonDictionary = validationDictionary;	
		}
		
		public HomeIndexModel Index(int count)
		{
			BookRepository bRepo = new BookRepository(_dal);
			
			var viewData = new HomeIndexModel();
			viewData.Latest = bRepo.List().OrderByDescending(x => x.added).Take(count).ToArray();
			
			return viewData;
		}
		
		public Search[] AjaxSearch(string term)
		{	
			if (String.IsNullOrEmpty(term))
				throw new ArgumentNullException("term");
			
			BookRepository bRepo = new BookRepository(_dal);
			
			Search[] terms =  bRepo.SearchForTerm(term);
			return terms;
			//List<string> ret = new List<string>();
			//foreach (var name in terms)
			//{
			//    ret.Add(name.label);
			//}

			//return ret.ToArray();
		}
		
		public StatisticViewModel Statistics()
		{
			BookRepository rRepo = new BookRepository(_dal);
			StoryRepository sRepo = new StoryRepository(_dal);
			
			var viewData = new StatisticViewModel();
			viewData.Books = rRepo.List().Count();
			viewData.Stories = sRepo.List().Count();
			
			return viewData;
		}
	}
}
