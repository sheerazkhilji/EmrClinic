using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicAPI.Models.Request
{
    public class DoctorSchedule
    {



        public string dates { get; set; }

        public string starttime { get; set; }

        public string endtime { get; set; }

        public int DoctorId { get; set; }


    }





}