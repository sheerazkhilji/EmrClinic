using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicAPI.Models.pagination
{
    public class PatientModel
    {
		public int ID { get; set; }

		public string Patient_ID { get; set; }

		public string Patient_Name { get; set; }

		public string Patient_Email { get; set; }

		public string Patient_Phone_Number { get; set; }

		public string Patient_Mobile_Number { get; set; }

		public string Patient_Address { get; set; }

		public string CNIC { get; set; }

		public string DateOfBirth { get; set; }

		public string Patient_Gender { get; set; }

		public string Documents { get; set; }

		public string CityID { get; set; }

		public bool IsActive { get; set; }

		public string Place_of_Birth { get; set; }

		public string cityname { get; set; }

		public string pcityname { get; set; }

		public string M_Status { get; set; }

		public string comment { get; set; }
        public string MR_No { get; set; }
    }
}