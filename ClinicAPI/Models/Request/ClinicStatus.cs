using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicAPI.Models.Request
{
    public class ClinicStatus
    {
        public string TotalPateint { get; set; }

        public string TotalCash { get; set; }

        public string MonthlySales { get; set; }

        public string AppointmentsDue { get; set; }

        public string todaySales { get; set; }
    }
}