using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicAPI.Models.Request
{
    public class Douments
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string PID { get; set; }

        public string file { get; set; }

        public string Doc
        {
            get; set;
        }


        public string UserId { get; set; }
        public string Isprivate { get; set; }
    }
}