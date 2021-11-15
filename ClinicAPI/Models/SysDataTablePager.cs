﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicAPI.Models
{
    public class SysDataTablePager<T>
    {
		public IEnumerable<T> aaData { get; set; }

		public int iTotalRecords { get; set; }

		public int iTotalDisplayRecords { get; set; }

		public string sEcho { get; set; }

		public SysDataTablePager(IEnumerable<T> items, int totalRecords, int totalDisplayRecords, string sEcho)
		{
			aaData = items;
			iTotalRecords = totalRecords;
			iTotalDisplayRecords = totalDisplayRecords;
			this.sEcho = sEcho;
		}
	}
}