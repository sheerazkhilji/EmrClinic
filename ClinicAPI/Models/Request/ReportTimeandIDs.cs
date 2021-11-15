using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicAPI.Models.Request
{
    public class ReportTimeandIDs
    {
        public string startdate { get; set; }

        public string enddate { get; set; }

        public string ids { get; set; }

    }
}