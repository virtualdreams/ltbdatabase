using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTB_Database.Core
{
	public class Pager
	{
		private long _totalPages;
		private long _totalCount;
		private long _pageSize;
		private long _pageIndex;

		public Pager(long pageIndex, long pageSize, long totalItems)
		{
			this._pageIndex = pageIndex;
			this._pageSize = pageSize;
			this._totalCount = totalItems;
			this._totalPages = _totalCount / _pageSize;
			if (this._totalCount % this._pageSize > 0)
			{
				this._totalPages++;
			}
		}

		/// <summary>
		/// Get total pages
		/// </summary>
		public long TotalPages
		{
			get
			{
				return (this._totalPages);
			}
		}

		/// <summary>
		/// Get total items
		/// </summary>
		public long TotalCount
		{
			get
			{
				return (this._totalCount); 
			}
		}

		/// <summary>
		/// Get items per page
		/// </summary>
		public long PageSize
		{
			get
			{
				return (this._pageSize); 
			}
		}

		/// <summary>
		/// Get or set current page
		/// </summary>
		public long PageIndex
		{
			get
			{
				return (this._pageIndex); 
			}
			set
			{ 
				this._pageIndex = value; 
			}
		}

		/// <summary>
		/// Has next page
		/// </summary>
		public bool HasNextPage
		{
			get
			{ 
				return (this.PageIndex * this.PageSize) < this.TotalCount; 
			}
		}

		/// <summary>
		/// Has prev page
		/// </summary>
		public bool HasPrevPage
		{
			get
			{
				return (this.PageIndex > 1); 
			} 
		}
		
		public long SetPageIndexToLimit(long page)
		{
			long r = (long)(this.TotalCount / this.PageSize);
			if (page > r && this.TotalCount != 0)
			{
				this.PageIndex = this.TotalPages;
				return this.TotalPages;
			}

			if (page < 1)
			{
				this.PageIndex = 1;
				return 1;
			}
			
			return this.PageIndex;
		}
	}
}

