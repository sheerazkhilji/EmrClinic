using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClinicWeb.Controllers
{
    public class AppointmentController : Controller
    {
		
		
		public ActionResult Index()
		{
			if ( Request.Cookies["Token"] == null ||  Request.Cookies["Role"] == null ||  Request.Cookies["Name"] == null)
			{
				return RedirectToAction("Login", "Account");
			}
			if ( Request.Cookies["Token"] != null && ( Request.Cookies["Role"].Value.ToString().Contains("Admin") ||  Request.Cookies["Role"].Value.ToString().Contains("Assistant")))
			{
				return View();
			}
			return RedirectToAction("Login", "Account");
		}

		public ActionResult DoctorAppointments()
		{
			if ( Request.Cookies["Token"] == null ||  Request.Cookies["Role"] == null ||  Request.Cookies["Name"] == null)
			{
				return RedirectToAction("Login", "Account");
			}
			if ( Request.Cookies["Token"] != null && ( Request.Cookies["Role"].Value.ToString().Contains("Admin") ||  Request.Cookies["Role"].Value.ToString().Contains("Doctor")))
			{
				return View();
			}
			return RedirectToAction("Login", "Account");
		}


      public ActionResult AppointmentDetails()
        {



			if ( Request.Cookies["Token"] == null ||  Request.Cookies["Role"] == null ||  Request.Cookies["Name"] == null)
			{
				return RedirectToAction("Login", "Account");
			}
			if ( Request.Cookies["Token"] != null && ( Request.Cookies["Role"].Value.ToString().Contains("Admin") ||  Request.Cookies["Role"].Value.ToString().Contains("Doctor")))
			{
				return View();
			}
			return RedirectToAction("Login", "Account");
		}
		public ActionResult AddUpdateFormDetails() {

			return View("_AddUpdateFormDetails");
		}

	}
}