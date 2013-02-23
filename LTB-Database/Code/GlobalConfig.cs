using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace System.Web.Mvc
{
	public class GlobalConfig
	{
		private static GlobalConfig m_Instance = null;
		private static readonly object singletonLock = new object();
		
		public string ConnectionString
		{
			get;
			private set;
		}
		
		public string AssemblyName
		{
			get;
			private set;
		}

		public string ConnectionClass
		{
			get;
			private set;
		}

		public string MappingFile
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
			if(ConfigurationManager.AppSettings["assemblyName"] != null)
			{
				AssemblyName = ConfigurationManager.AppSettings["assemblyName"].ToString();
			}

			if (ConfigurationManager.AppSettings["connectionClass"] != null)
			{
				ConnectionClass = ConfigurationManager.AppSettings["connectionClass"].ToString();
			}

			if (ConfigurationManager.AppSettings["mappingFile"] != null)
			{
				MappingFile = ConfigurationManager.AppSettings["mappingFile"].ToString();
			}

			if (ConfigurationManager.AppSettings["connectionString"] != null)
			{
				ConnectionString = ConfigurationManager.AppSettings["connectionString"].ToString();
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
