using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicAPI.Models.Request
{
    public class Invoice:PatientRegistration
    {

        public int AppointmentAmount { get; set; }
        public int InviceID { get; set; }
        public int AppointmentID { get; set; }
        public string Discription { get; set; }

        public int Due { get; set; }

        public int Amount { get; set; }


        public int AmountWay { get; set; }


        public string MR_No { get; set; }


        public int TotalPaid { get; set; }


        public string InvoiceNumer { get; set; }

    }


}