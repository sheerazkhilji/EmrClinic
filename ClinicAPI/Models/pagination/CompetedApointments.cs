using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicAPI.Models.pagination
{
    public class CompetedApointments
    {
		public string appoientmentID { get; set; }

		public string appoinement_date { get; set; }

		public string pateintid { get; set; }

		public string pateintname { get; set; }

		public string doctorID { get; set; }

		public string doctorName { get; set; }

		public string paymenttype { get; set; }

		public string amount { get; set; }

		public string pidandaid { get; set; }
	}
}