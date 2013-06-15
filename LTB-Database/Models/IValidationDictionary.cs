using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LTB_Database.Models
{
	public interface IValidationDictionary
	{
		void AddError(string key, string errorMessage);
		void Clear();
		bool IsValid
		{
			get; 
		}
	}
}
