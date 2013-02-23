using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LTB_Database.Repository
{
	public class LtbRepositoryConfig
	{
		private static LtbRepositoryConfig instance = null;
		
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

		private LtbRepositoryConfig(string connectionString, string assemblyName, string connectionClass, string mappingFile)
		{
			ConnectionString = connectionString;
			AssemblyName = assemblyName;
			ConnectionClass = connectionClass;
			MappingFile = mappingFile;
		}
		
		public static LtbRepositoryConfig Config(string connectionString, string assemblyName, string connectionClass, string mappingFile)
		{
			if(instance == null)
			{
				instance = new LtbRepositoryConfig(connectionString, assemblyName, connectionClass, mappingFile);
			}
			return instance;
		}
		
		public static LtbRepositoryConfig Instance()
		{
			if(instance == null)
			{
				throw new Exception("Repository instance is not configured. Call Config() first.");
			}
			return instance;
		}
		
		public static LtbRepositoryConfig Get()
		{
			return Instance();
		}
	}
}
