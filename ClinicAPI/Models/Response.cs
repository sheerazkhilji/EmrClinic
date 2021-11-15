using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicAPI.Models
{
    public class Response
    {
        public string status { get; set; }

        public string message { get; set; }

        public object Object { get; set; }
    }
}