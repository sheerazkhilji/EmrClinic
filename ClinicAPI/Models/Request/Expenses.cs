using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicAPI.Models.Request
{
    public class Expenses
    {
		public string Id { get; set; }

		public string Amount { get; set; }

		public string title { get; set; }

		public string Isactive { get; set; }

		public string date { get; set; }

		public string total { get; set; }

		public string todayTotal { get; set; }
	}
}