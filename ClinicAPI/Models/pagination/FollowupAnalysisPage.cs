using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicAPI.Models.pagination
{
    public class FollowupAnalysisPage
    {
		public string pkid { get; set; }

		public string CurrentMedication { get; set; }

		public string ProgressiveNote { get; set; }

		public string Subjective { get; set; }

		public string ObjectiveandMentalStateExamination { get; set; }

		public string Assessment { get; set; }

		public string Plan { get; set; }

		public string PatientID { get; set; }

		public string Date { get; set; }
	}
}