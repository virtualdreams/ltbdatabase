using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LTB_Database.Models
{
	public class CategoryModel
	{
		public long Id
		{
			get;
			set;
		}
		
		[Required(ErrorMessage="Bitte gib einen Namen ein")]
		[StringLength(100, ErrorMessage="max. 100 Zeichen erlaubt")]
		public string Name
		{
			get;
			set;
		}
	}
}