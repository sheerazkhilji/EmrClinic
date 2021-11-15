using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClinicWeb.Controllers
{
    public class DashboardController : Controller
    {
		public ActionResult Index()
		{
			if ( Request.Cookies["Token"] == null ||  Request.Cookies["Role"] == null ||  Request.Cookies["Name"] == null)
			{
				return RedirectToAction("Login", "Account");
			}
			if ( Request.Cookies["Token"] != null && ( Request.Cookies["Role"].Value.ToString().Contains("Admin") ||  Request.Cookies["Role"].Value.ToString().Contains("Assistant") ||  Request.Cookies["Role"].Value.ToString().Contains("Doctor")))
			{
				return View();
			}
			return RedirectToAction("Login", "Account");
		}

		public ActionResult Dashboard()
		{

			
			

			if (HttpContext.Request.Cookies["Token"] == null || Request.Cookies["Role"] == null || Request.Cookies["Name"]== null)
			{
				return RedirectToAction("Login", "Account");
			}
			return View();
		}

		public ActionResult City()
		{
			if (Request.Cookies["Token"] == null || Request.Cookies["Role"] == null || Request.Cookies["Name"] == null)
			{
				return RedirectToAction("Login", "Account");
			}
			if (Request.Cookies["Token"] != null && (Request.Cookies["Role"].Value.ToString().Contains("Admin") || Request.Cookies["Role"].Value.ToString().Contains("Assistant")))
			{
				return View();
			}
			return RedirectToAction("Login", "Account");
		}

		public ActionResult CityList()
		{
			if (Request.Cookies["Token"] == null || Request.Cookies["Role"] == null || Request.Cookies["Name"] == null)
			{
				return RedirectToAction("Login", "Account");
			}
			if (Request.Cookies["Token"] != null && (Request.Cookies["Role"].Value.ToString().Contains("Admin") || Request.Cookies["Role"].Value.ToString().Contains("Assistant")))
			{
				return PartialView();
			}
			return RedirectToAction("Login", "Account");
		}

		public ActionResult CreateCity()
		{
			if (Request.Cookies["Token"] == null || Request.Cookies["Role"] == null || Request.Cookies["Name"] == null)
			{
				return RedirectToAction("Login", "Account");
			}
			if (Request.Cookies["Token"] != null && (Request.Cookies["Role"].Value.ToString().Contains("Admin") || Request.Cookies["Role"].Value.ToString().Contains("Assistant")))
			{
				return PartialView();
			}
			return RedirectToAction("Login", "Account");
		}

		public ActionResult UserRole()
		{
			if (Request.Cookies["Token"] == null ||  Request.Cookies["Role"] == null || Request.Cookies["Name"] == null)
			{
				return RedirectToAction("Login", "Account");
			}
			if ( Request.Cookies["Token"] != null && ( Request.Cookies["Role"].Value.ToString().Contains("Admin") ||  Request.Cookies["Role"].Value.ToString().Contains("Assistant") ||  Request.Cookies["Role"].Value.ToString().Contains("Doctor")))
			{
				return View();
			}
			return RedirectToAction("AccessDenied", "Account");
		}

		public ActionResult Doctor()
		{
			if ( Request.Cookies["Token"] == null ||  Request.Cookies["Role"] == null ||  Request.Cookies["Name"] == null)
			{
				return RedirectToAction("Login", "Account");
			}
			if ( Request.Cookies["Token"] != null &&  Request.Cookies["Role"].Value.ToString().Contains("Doctor"))
			{
				return View();
			}
			return RedirectToAction("AccessDenied", "Account");
		}

		public ActionResult Expenses()
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

		public ActionResult checkingdate()
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

		public ActionResult AmountPayble()
		{
			if (Request.Cookies["Token"] == null || Request.Cookies["Role"] == null || Request.Cookies["Name"] == null)
			{
				return RedirectToAction("Login", "Account");
			}
			if (Request.Cookies["Token"] != null && (Request.Cookies["Role"].Value.ToString().Contains("Admin") || Request.Cookies["Role"].Value.ToString().Contains("Assistant")))
			{
				return View();
			}
			return RedirectToAction("Login", "Account");
		}


		public ActionResult CompletedAppointmentsByDoctor()
		{
			if (Request.Cookies["Token"] == null || Request.Cookies["Role"] == null || Request.Cookies["Name"] == null)
			{
				return RedirectToAction("Login", "Account");
			}
			if (Request.Cookies["Token"] != null && Request.Cookies["Role"].Value.ToString().Contains("Doctor"))
			{
				return View();
			}
			return RedirectToAction("AccessDenied", "Account");
		}


		public ActionResult DoctorAppointmentShedule()
		{
			if (Request.Cookies["Token"] == null || Request.Cookies["Role"] == null || Request.Cookies["Name"] == null)
			{
				return RedirectToAction("Login", "Account");
			}
			if (Request.Cookies["Token"] != null && Request.Cookies["Role"].Value.ToString().Contains("Doctor"))
			{
				return View();
			}
			return RedirectToAction("AccessDenied", "Account");
		}



		public ActionResult Referral()
		{
			if (Request.Cookies["Token"] == null || Request.Cookies["Role"] == null || Request.Cookies["Name"] == null)
			{
				return RedirectToAction("Login", "Account");
			}
			if (Request.Cookies["Token"] != null && (Request.Cookies["Role"].Value.ToString().Contains("Admin") || Request.Cookies["Role"].Value.ToString().Contains("Assistant")))
			{
				return View();
			}
			return RedirectToAction("Login", "Account");
		}
	}
}