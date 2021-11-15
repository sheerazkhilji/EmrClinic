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
    public class InvoiceController : ApiController
	{
		private NameValueCollection NameValue = new NameValueCollection();

		private DataOperationLayer OperationLayer;

		private Response response = new Response();

		private string ConnectionString = ConfigurationManager.AppSettings["Connectionstring"];

		[HttpGet]
		[Route("api/Invoice/GetInvoiceInfo")]
		public Response GetInvoiceInfo(int appointid ,int pateintid)
		{
			NameValue.Add("@Patientid", pateintid.ToString());

			NameValue.Add("@AppointmentID", appointid.ToString());


			
			OperationLayer = new DataOperationLayer(ConnectionString);
			var data = OperationLayer.Statusget("sp_getinfo_forinvoice", NameValue);



            Invoice invoiceinfo = new Invoice()
            {
                Due  =  Convert.ToInt32(data.Tables[0].Rows[0][0]),
                Patient_Email = data.Tables[1].Rows[0][3].ToString(),
                Patient_Name = data.Tables[1].Rows[0][1].ToString(),
                Patient_Mobile_Number = data.Tables[1].Rows[0][2].ToString(),
                Discription = data.Tables[1].Rows[0][4].ToString(),
				Amount = Convert.ToInt32(data.Tables[1].Rows[0][6]),




			};

			response.status = "success";
			response.Object = invoiceinfo;





			return response;



        }


		[HttpPost]
		[Route("api/Invoice/AddInvoice")]
		public Response AddInvoice(Invoice invoice)
		{

			if (invoice.AmountWay == 3)
			{



				NameValue.Add("@Patient_ID", invoice.PatientID.ToString());

				NameValue.Add("@AppointmentID", invoice.AppointmentID.ToString());
				NameValue.Add("@amount", invoice.Amount.ToString());




				OperationLayer = new DataOperationLayer(ConnectionString);
				var data = OperationLayer.Statusget("sp_Addinvoice", NameValue);
				Random rand = new Random(); 

				const int maxValue = 999;
				string number = rand.Next(maxValue + 1).ToString("D3");


				Invoice invoiceinfo = new Invoice()
				{
					MR_No = data.Tables[0].Rows[0][0].ToString(),
					Patient_Name = data.Tables[0].Rows[0][2].ToString(),
					Patient_Mobile_Number = data.Tables[0].Rows[0][3].ToString(),
					Patient_Email = data.Tables[0].Rows[0][4].ToString()==null?"": data.Tables[0].Rows[0][4].ToString(),
					AppointmentAmount=Convert.ToInt32(data.Tables[0].Rows[0][7]),
					Discription = data.Tables[0].Rows[0][5].ToString(),
				InviceID = Convert.ToInt32(data.Tables[2].Rows[0][0]),
				TotalPaid= Convert.ToInt32(data.Tables[2].Rows[0][1]),

					Due = Convert.ToInt32(data.Tables[1].Rows[0][0]),
					
				};

				response.status = "Invoice";
				response.Object = invoiceinfo;





				return response;
			}

            else
            {

				NameValue.Add("@AppointmentID", invoice.AppointmentID.ToString());
				NameValue.Add("@Patient_ID ", "");
				NameValue.Add("@Appointment_Data", "");
				NameValue.Add("@StartDate", "");
				NameValue.Add("@EndDate", "");
				NameValue.Add("@DoctorID", "");
				NameValue.Add("@PaymentType", "");
				NameValue.Add("@Amount", "");
				NameValue.Add("@AppointmentStatus", "");
				NameValue.Add("@AppType", "");
				NameValue.Add("@cumpusid", "");

				NameValue.Add("@link", "");

				NameValue.Add("@action", "complete");
				OperationLayer = new DataOperationLayer(ConnectionString);
				string json = OperationLayer.callStoredProcedure("sp_EditAppointment", NameValue);
				if (json == null || json == "")
				{
					response.status = "fail";
				}
				else
				{
					response.status = "Due";
					response.Object = json;
				}
				return response;




			}


		}



	}
}
