using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicAPI.Models.Request
{
    public class PatientRegistration
    {
		public string PatientID { get; set; }

		public string Patient_Name { get; set; }

		public string Patient_Email { get; set; }

		public string Patient_Phone_Number { get; set; }

		public string Patient_Mobile_Number { get; set; }

		public string CNIC { get; set; }

		public string DateOfBirth { get; set; }

		public string Patient_Gender { get; set; }

		public string Documents { get; set; }

		public string Patient_Address { get; set; }

		public string CityID { get; set; }

		public string[] ImageBase64 { get; set; }

		public string Place_Of_Birth { get; set; }

		public string Comment { get; set; }

        public string MR_No { get; set; }
    }
}