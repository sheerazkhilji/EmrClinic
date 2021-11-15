using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClinicWeb.Controllers
{
    public class WebsiteController : Controller
    {
        // GET: Website
        public ActionResult Index()
        {
            return View();
        }
		[HttpPost]
		[Route("api/CreateAppoint")]
		public string CreateAppointAsyn( WebAppointment appointment)
		{
			WebsiteResponse res = new WebsiteResponse();

			//NameValue.Add("@Patient_ID ", appointment.PatientId);
			//NameValue.Add("@Appointment_Data", appointment.Appointment_Data);
			//NameValue.Add("@StartDate", appointment.sDate);
			//NameValue.Add("@EndDate", appointment.eDate);
			//NameValue.Add("@DoctorID", appointment.DoctorID);
			//NameValue.Add("@PaymentType", appointment.PaymentType);
			//NameValue.Add("@Amount", appointment.Amount);
			//NameValue.Add("@AppointmentStatus", appointment.AppointmentStatus);

			//NameValue.Add("@AppType", appointment.AppType.ToString());

			//NameValue.Add("@cumpusid", appointment.campusid.ToString());

			//NameValue.Add("@link", appointment.link.ToString());


			//NameValue.Add("@action", "insert");
			//OperationLayer = new DataOperationLayer(ConnectionString);
			//string json = OperationLayer.callStoredProcedure("sp_Appointment", NameValue);
			//if (json == "Appointment Already exists ")
			//{
			//	res.status = "Appointment Already exists";
			//	res.Object = json;

			//}
			//else
			//{
			//res.status = "success";
			//res.Object = "Appointment created successfully";


			//}
			//return res.status;

			return "";

		
		}

	}
	public class WebAppointment
	{
		public string AppointmentType { get; set; }

		public string DrEmail { get; set; }

		public string PatientFullName { get; set; }

		public string PatientAge { get; set; }

		public string PatientGender { get; set; }

		public string OnBehalfOf { get; set; }

		public string RelationshipWithPatient { get; set; }

		public string Email { get; set; }

		public string PhoneNumber { get; set; }

		public string Address { get; set; }

		public string City { get; set; }


		public string Province { get; set; }

		public string Country { get; set; }


		public string PeriviouslyConsulted { get; set; }

		public string PeriviouslyConsultedDetails { get; set; }

		public string ReferredBy { get; set; }

		public string InitialAppointmentDateTime { get; set; }
		public string FollowupDateTime { get; set; }

	}

	public class WebsiteResponse
    {
        public string status { get; set; }

        public string message { get; set; }

        public object Object { get; set; }
    }
}