using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicAPI.Models.Request
{
    public class Prescription :PatientRegistration
    {
        public int Presid { get; set; }

        public int Pid { get; set; }

        public string Medication { get; set; }

        public string Investigations { get; set; }

        public string Referral { get; set; }


        public string Advice { get; set; }

        public string Return_to_Clinic { get; set; }
        public int IsActive { get; set; }


        public int Suggid { get; set; }



        public int AppointmentID { get; set; }
        public string DoctorName { get; set; }



    }
}