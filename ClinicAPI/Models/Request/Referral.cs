using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicAPI.Models.Request
{
    public class Referral
    {
        public int Rid { get; set; }

        public string MobileNumber { get; set; }

        public string Email { get; set; }

        public string RefereedBy { get; set; }

        public string DateOfBirth { get; set; }

        public string CNIC { get; set; }

        public string pateint_name { get; set; }

        public string CreateDate { get; set; }

        public int DoctorId { get; set; }

        public string DoctorName { get; set; }


        public string Comments { get; set; }
        public string appointmentDate { get; set; }

        public int Save { get; set; }

    }
}