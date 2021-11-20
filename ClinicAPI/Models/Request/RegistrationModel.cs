using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicAPI.Models.Request
{
    public class RegistrationModel
    {
		public string UserId { get; set; }

		public string Rid { get; set; }

		public string UserName { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }

		public string cityname { get; set; }

		public string ConfirmPassword { get; set; }

		public string PhoneNumber { get; set; }

		public string Cid { get; set; }
        public string Speciality { get; set; }


		public  string RoleName { get; set; }

        public string[] rolesids { get; set; }

		public string[] roleisactive { get; set; }
	}
}