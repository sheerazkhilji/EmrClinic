using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicAPI.Models.Request
{
    public class ClinciReportsPOCO
    {
        public int SNo { get; set; }
        public string PatientName { get; set; }

        public string DoctorName { get; set; }

        public string Age { get; set; }

        public string DateofAppointment { get; set; }

        public string Time { get; set; }
        public string AppointmentType { get; set; }
        public string AppointmentSatus { get; set; }



    }
 

}