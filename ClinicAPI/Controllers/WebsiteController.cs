using ClinicAPI.Models;
using ClinicAPI.Models.Request;
using DataConnection;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClinicAPI.Controllers
{
    public class WebsiteController : ApiController
    {
		private NameValueCollection NameValue = new NameValueCollection();

		private DataOperationLayer OperationLayer;

		private Response response = new Response();

		private string ConnectionString = ConfigurationManager.AppSettings["Connectionstring"];

		[HttpPost]
		[Route("api/Website/CreateAppoint")]
		public string CreateAppointAsyn(WebAppointment appointment)
		{
			WebsiteResponse res = new WebsiteResponse();

            NameValue.Add("@Patient_ID ", appointment.PatientId);
            NameValue.Add("@Appointment_Data", appointment.Appointment_Data);
            NameValue.Add("@StartDate", appointment.sDate);
            NameValue.Add("@EndDate", appointment.eDate);
            NameValue.Add("@DoctorID", appointment.DoctorID);
            NameValue.Add("@PaymentType", appointment.PaymentType);
            NameValue.Add("@Amount", appointment.Amount);
            NameValue.Add("@AppointmentStatus", appointment.AppointmentStatus);

            NameValue.Add("@AppType", appointment.AppType.ToString());

            NameValue.Add("@cumpusid", appointment.campusid.ToString());

            NameValue.Add("@link", appointment.link.ToString());


            NameValue.Add("@action", "insert");
            OperationLayer = new DataOperationLayer(ConnectionString);
            string json = OperationLayer.callStoredProcedure("sp_Appointment", NameValue);
            if (json == "Appointment Already exists ")
            {
                res.status = "Appointment Already exists";
                res.Object = json;

            }
            else
            {
                res.status = "success";
                res.Object = "Appointment created successfully";


            }
            return res.status;


		}
	}


	

	public class WebsiteResponse
	{
		public string status { get; set; }

		public string message { get; set; }

		public object Object { get; set; }
	}
}
