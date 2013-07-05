using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LTB_Database.Models
{
	public class BookModel
	{
		public long Id
		{
			get;
			set;
		}

		[Required(ErrorMessage="Bitte gib einen Titel ein")]
		public string Name
		{
			get;
			set;
		}
		[Required(ErrorMessage = "Bitte gib eine Nummer ein")]
		[Range(0, 99999, ErrorMessage="Die Nummer muss im Bereich von 0 - 9999 liegen")]
		public long Number
		{
			get;
			set;
		}
		[Required(ErrorMessage = "Bitte wähle eine Kategorie aus")]
		public long CategoryId
		{
			get;
			set;
		}
		
		public DateTime Added
		{
			get;
			set;
		}

		private string[] _stories = new string[] { };
		public string[] Stories
		{
			get
			{
				return _stories;
			}
			set
			{
				if(value != null)
					_stories = value;
			}
		}
		
		public string Image
		{
			get;
			set;
		}

		public HttpPostedFileBase Imagefile
		{
			get;
			set;
		}
	}
}