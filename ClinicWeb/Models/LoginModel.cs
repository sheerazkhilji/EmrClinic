﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicWeb.Models
{
    public class LoginModel
    {
        public string email { get; set; }

        public string password { get; set; }

        public bool remember { get; set; }
    }
}