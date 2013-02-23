using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTB_Database.Models
{
	public class Pager
	{
		private long m_TotalPages;
		private long m_TotalCount;
		private long m_PageSize;
		private long m_PageIndex;

		public Pager (long index, long pagesize, long total)
		{
			m_PageIndex = index;
			m_PageSize = pagesize;
			m_TotalCount = total;
			m_TotalPages = m_TotalCount / m_PageSize;
			if (m_TotalCount % m_PageSize > 0)
			{
				m_TotalPages++;
			}
		}

		/// <summary>
		/// Get total pages
		/// </summary>
		public long TotalPages
		{
			get { return (m_TotalPages); }
		}

		/// <summary>
		/// Get total items
		/// </summary>
		public long TotalCount
		{
			get { return (m_TotalCount); }
		}

		/// <summary>
		/// Get items per page
		/// </summary>
		public long PageSize
		{
			get { return (m_PageSize); }
		}

		/// <summary>
		/// Get current page
		/// </summary>
		public long PageIndex
		{
			get { return (m_PageIndex); }
		}

		/// <summary>
		/// Has next page
		/// </summary>
		public bool HasNextPage
		{
			///TODO <= ?
			get { return (PageIndex * PageSize) < TotalCount; }
		}

		/// <summary>
		/// Has prev page
		/// </summary>
		public bool HasPrevPage
		{
			get { return (PageIndex > 1); } 
		}
	}
}

