using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace LTB_Database.Core
{
	public class GlobalConfig
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(GlobalConfig));
		
		private static GlobalConfig m_Instance = null;
		private static readonly object singletonLock = new object();
		
		public string Log4Net
		{
			get;
			private set;
		}
		
		public string SqlMapperConfig
		{
			get;
			private set;
		}
		
		public string AllowMimeType
		{
			get;
			private set;
		}
		
		public string ImagePath
		{
			get;
			private set;
		}
		
		private GlobalConfig()
		{
			ReadConfig();
		}
		
		public static GlobalConfig Instance()
		{
			if(m_Instance == null)
			{
				lock(singletonLock)
				{
					if(m_Instance == null)
					{
						m_Instance = new GlobalConfig();
					}
				}
			}
			return m_Instance;
		}
		
		public static GlobalConfig Get()
		{
			return Instance();
		}
		
		private void ReadConfig()
		{
			if (ConfigurationManager.AppSettings["log4net"] != null)
			{
				Log4Net = ConfigurationManager.AppSettings["log4net"].ToString();
			}
			
			if (ConfigurationManager.AppSettings["sqlMapperConfig"] != null)
			{
				SqlMapperConfig = ConfigurationManager.AppSettings["sqlMapperConfig"].ToString();
			}
			
			if (ConfigurationManager.AppSettings["allowMimeType"] != null)
			{
				AllowMimeType = ConfigurationManager.AppSettings["allowMimeType"].ToString();
			}

			if (ConfigurationManager.AppSettings["imagePath"] != null)
			{
				ImagePath = ConfigurationManager.AppSettings["imagePath"].ToString();
			}
		}
	}
}
