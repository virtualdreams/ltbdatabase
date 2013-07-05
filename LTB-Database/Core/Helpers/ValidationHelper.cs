using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Resources;
using System.Collections;
using System.Globalization;

namespace LTB_Database.Core
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public class ArrayStringLengthAttribute: ValidationAttribute
	{
		public int MaximumLength
		{
			get;
			private set;
		}
		public ArrayStringLengthAttribute(int length)
		{
			if(length < 0)
				throw new ArgumentOutOfRangeException("length", length, "Der Wert darf nicht negativ sein");
			
			this.MaximumLength = length;
		}

		public override bool IsValid(object value)
		{
			if(value != null)
			{
				IEnumerable enumerable = value as IEnumerable;
				if(enumerable != null)
				{
					foreach(object s in enumerable)
					{
						if( ((string)s).Length <= this.MaximumLength )
						{
							return false;
						}
					}
				}
			}
			return false;
		}

		public override string FormatErrorMessage(string name)
		{
			return String.Format(CultureInfo.CurrentCulture, base.ErrorMessageString, new object[]
			{
				name,
				this.MaximumLength
			});
		}
	}
}
