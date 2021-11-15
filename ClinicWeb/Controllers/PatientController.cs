using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClinicWeb.Controllers
{
    public class PatientController : Controller
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

		public ActionResult Patients()
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

		public ActionResult Registration()
		{
			if ( Request.Cookies["Token"] == null ||  Request.Cookies["Role"] == null ||  Request.Cookies["Name"] == null)
			{
				return RedirectToAction("Login", "Account");
			}
			if ( Request.Cookies["Token"] != null && ( Request.Cookies["Role"].Value.ToString().Contains("Admin") ||  Request.Cookies["Role"].Value.ToString().Contains("Assistant")))
			{
				return PartialView();
			}
			return RedirectToAction("Login", "Account");
		}

		public ActionResult ListofPatient()
		{
			if ( Request.Cookies["Token"] == null ||  Request.Cookies["Role"] == null ||  Request.Cookies["Name"] == null)
			{
				return RedirectToAction("Login", "Account");
			}
			return PartialView();
		}

		public ActionResult PatientIntialAssesment()
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

		public ActionResult PatientEditAssesment()
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

		public ActionResult PatientIntialAssesmentDetails()
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

		public ActionResult PatientFollowupAnalysis()
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

		public ActionResult PatientAppointedList()
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

		public ActionResult AllAppointments()
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

		public ActionResult PatientDetails()
		{
			if ( Request.Cookies["Token"] == null ||  Request.Cookies["Role"] == null ||  Request.Cookies["Name"] == null)
			{
				return RedirectToAction("Login", "Account");
			}
			return View();
		}

		public ActionResult Douments()
		{
			if ( Request.Cookies["Token"] == null ||  Request.Cookies["Role"] == null ||  Request.Cookies["Name"] == null)
			{
				return RedirectToAction("Login", "Account");
			}
			return View();
		}

		public ActionResult ListofDouments()
		{
			if ( Request.Cookies["Token"] == null ||  Request.Cookies["Role"] == null ||  Request.Cookies["Name"] == null)
			{
				return RedirectToAction("Login", "Account");
			}
			return PartialView();
		}

		public ActionResult AddDouments()
		{
			if ( Request.Cookies["Token"] == null ||  Request.Cookies["Role"] == null ||  Request.Cookies["Name"] == null)
			{
				return RedirectToAction("Login", "Account");
			}
			return PartialView();
		}

		public ActionResult PateintDouments()
		{
			if ( Request.Cookies["Token"] == null ||  Request.Cookies["Role"] == null ||  Request.Cookies["Name"] == null)
			{
				return RedirectToAction("Login", "Account");
			}
			return View();
		}

		public ActionResult PateintListofDouments()
		{
			if ( Request.Cookies["Token"] == null ||  Request.Cookies["Role"] == null ||  Request.Cookies["Name"] == null)
			{
				return RedirectToAction("Login", "Account");
			}
			return PartialView();
		}

		public ActionResult PateintAddDouments()
		{
			if ( Request.Cookies["Token"] == null ||  Request.Cookies["Role"].ToString() == null ||  Request.Cookies["Name"] == null)
			{
				return RedirectToAction("Login", "Account");
			}
			return PartialView();
		}

		public ActionResult completedAppointments()
		{
			if ( Request.Cookies["Token"] == null ||  Request.Cookies["Role"] == null ||  Request.Cookies["Name"] == null)
			{
				return RedirectToAction("Login", "Account");
			}
			return View();
		}



		public ActionResult RegistorPateintDetail()
		{
			if ( Request.Cookies["Token"] == null ||  Request.Cookies["Role"] == null ||  Request.Cookies["Name"] == null)
			{
				return RedirectToAction("Login", "Account");
			}
			return View();
		}


	}
}