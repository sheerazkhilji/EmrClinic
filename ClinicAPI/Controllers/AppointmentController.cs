using ClinicAPI.HelperCode;
using ClinicAPI.Models;
using ClinicAPI.Models.pagination;
using ClinicAPI.Models.Request;
using DataConnection;
using iTextSharp.text;
using iTextSharp.text.pdf;

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ClinicAPI.Controllers
{
    public class AppointmentController : ApiController
    {
		private NameValueCollection NameValue = new NameValueCollection();

		private DataOperationLayer OperationLayer;

		private Response res = new Response();

		private EmailSender Email = new EmailSender();

        private string ConnectionString = ConfigurationManager.AppSettings["Connectionstring"];

        [HttpGet]
		[Route("api/getallPatient")]
		public Response getallPatient()
		{
			NameValue.Add("@PID", "");
			NameValue.Add("@Patient_Name", "");
			NameValue.Add("@Patient_Email", "");
			NameValue.Add("@Patient_Phone_Number", "");
			NameValue.Add("@Patient_Mobile_Number", "");
			NameValue.Add("@Patient_Address", "");
			NameValue.Add("@CNIC", "");
			NameValue.Add("@DateOfBirth", "");
			NameValue.Add("@Patient_Gender", "");
			NameValue.Add("@Documents", "");
			NameValue.Add("@CityID", "");
			NameValue.Add("@Place_Of_Birth", "");
			NameValue.Add("@comment", "");
			NameValue.Add("@Action", "getall");
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_Patient_Registration", NameValue);
			if (json == null || json == "")
			{
				res.status = "fail";
			}
			else
			{
				res.status = "success";
				res.Object = json;
			}
			return res;
		}

		[HttpGet]
		[Route("api/getallDoctor")]
		public Response getallDoctor()
		{
			NameValue.Add("@id", "");
			NameValue.Add("@action", "getall");
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_Doctor", NameValue);
			if (json == null || json == "")
			{
				res.status = "fail";
			}
			else
			{
				res.status = "success";
				res.Object = json;
			}
			return res;
		}

		[HttpPost]
		[Route("api/CreateAppoint")]
		public Response CreateAppointAsyn([FromBody] Appointment appointment)
		{
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

			NameValue.Add("@DoctorSlotid", appointment.DoctorSlotid.ToString());

			NameValue.Add("@action", "insert");
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_Appointment", NameValue);
            if (json== "Appointment Already exists ")
            {
				res.status = "Appointment Already exists";
				res.Object = json;

			}
            else
            {
				res.status = "success";
				res.Object = json;


			}
			return res;
		}

		[HttpPost]
		[Route("api/confirmAppoint")]
		public  Response confirmAppoint([FromBody] Appointment appointment)
		{
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

			NameValue.Add("@action", "confirm");
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_Appointment", NameValue);
			switch (json)
			{
				case null:
				case "":
					res.status = "fail";
					break;
				case "Appointment Already exists":
					res.status = "Appointment Already exists";
					res.Object = json;
					break;
				default:
					res.status = "success";
					res.Object = json;
					NameValue.Clear();
					NameValue.Add("@id", appointment.PatientId);
					break;
			}
			return res;
		}

		[HttpGet]
		[Route("api/getAppointments")]
		public Response getAppointments()
		{
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

			NameValue.Add("@action", "getall");
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_Appointment", NameValue);
			if (json == null || json == "")
			{
				res.status = "fail";
			}
			else
			{
				res.status = "success";
				res.Object = json;
			}
			return res;
		}



		[HttpGet]
		[Route("api/Appointment/getAppointmentsbyuserid")]
		public Response getAppointmentsbyuserid(int userid)
		{
			

			NameValue.Add("@userID",userid.ToString());
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_GetAppointmentsbyRoles", NameValue);
			res.status = "success";
				res.Object = json;
			
			return res;
		}

		[HttpGet]
		[Route("api/getAppointmentbyPateintID")]
		public Response getAppointmentbyPateintID(string id)
		{
			NameValue.Add("@Patient_ID ", id);
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

			NameValue.Add("@action", "getAppointmentbyPateintID");
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_Appointment", NameValue);
			if (json == null || json == "")
			{
				res.status = "fail";
			}
			else
			{
				res.status = "success";
				res.Object = json;
			}
			return res;
		}

		[HttpGet]
		[Route("api/getAppointmentbyDoctorID")]
		public Response getAppointmentbyDoctorID(string id)
		{
			NameValue.Add("@Patient_ID ", "");
			NameValue.Add("@Appointment_Data", "");
			NameValue.Add("@StartDate", "");
			NameValue.Add("@EndDate", "");
			NameValue.Add("@DoctorID", id);
			NameValue.Add("@PaymentType", "");
			NameValue.Add("@Amount", "");
			NameValue.Add("@AppointmentStatus", "");

			NameValue.Add("@AppType", "");
			NameValue.Add("@cumpusid", "");

			NameValue.Add("@link", "");

			NameValue.Add("@action", "getAppointmentbyDoctorID");
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_Appointment", NameValue);
			if (json == null || json == "")
			{
				res.status = "fail";
			}
			else
			{
				res.status = "success";
				res.Object = json;
			}
			return res;
		}

		[HttpPost]
		[Route("api/EditAppoint")]
		public  Response EditAppoint([FromBody] Appointment appointment)
		{
			NameValue.Add("@AppointmentID", appointment.AppointmentID);
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
			NameValue.Add("@action", "Edit");
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_EditAppointment", NameValue);
			if (json == "Appointment Already exists")
			{
				res.status = "Appointment Already exists";
				res.Object = json;

			}
			else
			{
				res.status = "success";
				res.Object = json;


			}
			return res;
		}

		[HttpPost]
		[Route("api/ConfirmEditAppoint")]
		public  Response ConfirmEditAppoint([FromBody] Appointment appointment)
		{
			NameValue.Add("@AppointmentID", appointment.AppointmentID);
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

			NameValue.Add("@action", "confirmEdit");
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_EditAppointment", NameValue);
			if (json == null || json == "")
			{
				res.status = "fail";
			}
			else
			{
				res.status = "success";
				res.Object = json;
			}
			return res;
		}

		[HttpGet]
		[Route("api/CompleteAppointment")]
		public Response CompleteAppointment(string id)
		{
			NameValue.Add("@AppointmentID", id);
			NameValue.Add("@Patient_ID ", "");
			NameValue.Add("@Appointment_Data", "");
			NameValue.Add("@StartDate", "");
			NameValue.Add("@EndDate", "");
			NameValue.Add("@DoctorID", "");
			NameValue.Add("@PaymentType", "");
			NameValue.Add("@Amount", "");
			NameValue.Add("@AppointmentStatus", "");
			NameValue.Add("@AppType","");
			NameValue.Add("@cumpusid", "");

			NameValue.Add("@link", "");

			NameValue.Add("@action", "complete");
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_EditAppointment", NameValue);
			if (json == null || json == "")
			{
				res.status = "fail";
			}
			else
			{
				res.status = "success";
				res.Object = json;
			}
			return res;
		}


		[HttpGet]
		[Route("api/Appointment/DidNotAttempt")]
		public Response DidNotAttempt(string id)
		{
			NameValue.Add("@AppointmentID", id);
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_DidNotAttempt", NameValue);
			if (json == null || json == "")
			{
				res.status = "fail";
			}
			else
			{
				res.status = "success";
				res.Object = json;
			}
			return res;
		}

		[HttpGet]
		[Route("api/CompeltedAppoinemnt")]
		public SysDataTablePager<CompetedApointments> CompeltedAppoinemnt(string startdate,string enddate)
		{
			NameValueCollection nvc = HttpUtility.ParseQueryString(base.Request.RequestUri.Query);
			string sEcho = nvc["sEcho"].ToString();
			string sSearch = nvc["sSearch"].ToString();
			int iDisplayStart = Convert.ToInt32(nvc["iDisplayStart"]);
			int iDisplayLength = Convert.ToInt32(nvc["iDisplayLength"]);
			int iSortCol = Convert.ToInt32(nvc["iSortCol_0"]);
			string sortOrder = nvc["sSortDir_0"].ToString();
			NameValue.Add("@id ", "");
			NameValue.Add("@startdate", startdate==null? "":startdate);
			NameValue.Add("@enddate", enddate == null ? "" : enddate);
			NameValue.Add("@search", sSearch);
			OperationLayer = new DataOperationLayer(ConnectionString);
			DataTable dataSet = OperationLayer.List("sp_CompletedAppointment", NameValue);
			List<CompetedApointments> patientModels = new List<CompetedApointments>();
			List<CompetedApointments> Customers = new List<CompetedApointments>();
			int Count = dataSet.Rows.Count;
			int i = 0;
			foreach (DataRow dr in dataSet.Rows)
			{
				i++;
				patientModels.Add(new CompetedApointments
				{
					appoientmentID = dr["AppointID"].ToString(),
					appoinement_date = dr["Appoint"].ToString(),
					pateintid = dr["Patient_ID"].ToString(),
					pateintname = dr["Patient_Name"].ToString(),
					doctorID = dr["UserID"].ToString(),
					doctorName = dr["UserName"].ToString(),
					amount = dr["Amount"].ToString(),
					paymenttype = dr["PaymentType"].ToString(),
					pidandaid = dr["Patient_Name"].ToString() + "$" + dr["Patient_ID"].ToString() + "$" + dr["AppointID"].ToString()
				});
				Customers = SortFunction(iSortCol, sortOrder, patientModels).Skip(iDisplayStart).Take(iDisplayLength).ToList();
			}

		


			return new SysDataTablePager<CompetedApointments>(Customers, Count, Count, sEcho);
		}

		private List<CompetedApointments> SortFunction(int iSortCol, string sortOrder, List<CompetedApointments> list)
		{
			if (iSortCol == 1 || iSortCol == 4)
			{
				Func<CompetedApointments, string> orderingFunction = (CompetedApointments c) => (iSortCol != 1) ? c.pateintname : c.pateintname;
				list = ((!(sortOrder == "desc")) ? list.OrderBy(orderingFunction).ToList() : list.OrderByDescending(orderingFunction).ToList());
			}
			return list;
		}

		[HttpGet]
		[Route("api/CompeltedAppoinemntbymonth")]
		public SysDataTablePager<CompetedApointments> CompeltedAppoinemntbymonth()
		{
			NameValueCollection nvc = HttpUtility.ParseQueryString(base.Request.RequestUri.Query);
			string sEcho = nvc["sEcho"].ToString();
			string sSearch = nvc["sSearch"].ToString();
			int iDisplayStart = Convert.ToInt32(nvc["iDisplayStart"]);
			int iDisplayLength = Convert.ToInt32(nvc["iDisplayLength"]);
			int iSortCol = Convert.ToInt32(nvc["iSortCol_0"]);
			string sortOrder = nvc["sSortDir_0"].ToString();
			NameValue.Add("@id ", "");
			NameValue.Add("@search", sSearch);
			OperationLayer = new DataOperationLayer(ConnectionString);
			DataTable dataSet = OperationLayer.List("sp_CompletedAppointmentbymonth", NameValue);
			List<CompetedApointments> patientModels = new List<CompetedApointments>();
			List<CompetedApointments> Customers = new List<CompetedApointments>();
			int Count = dataSet.Rows.Count;
			int i = 0;
			foreach (DataRow dr in dataSet.Rows)
			{
				i++;
				patientModels.Add(new CompetedApointments
				{
					appoientmentID = dr["AppointID"].ToString(),
					appoinement_date = dr["Appoint"].ToString(),
					pateintid = dr["Patient_ID"].ToString(),
					pateintname = dr["Patient_Name"].ToString(),
					doctorID = dr["UserID"].ToString(),
					doctorName = dr["UserName"].ToString(),
					amount = dr["Amount"].ToString(),
					paymenttype = dr["PaymentType"].ToString(),
					pidandaid = dr["Patient_Name"].ToString() + "$" + dr["Patient_ID"].ToString() + "$" + dr["AppointID"].ToString()
				});
				Customers = SortFunction(iSortCol, sortOrder, patientModels).Skip(iDisplayStart).Take(iDisplayLength).ToList();
			}
			return new SysDataTablePager<CompetedApointments>(Customers, Count, Count, sEcho);
		}

		private List<CompetedApointments> SortFunction(int iSortCol, string sortOrder, List<CompetedApointments> list, int? id)
		{
			if (iSortCol == 1 || iSortCol == 4)
			{
				Func<CompetedApointments, string> orderingFunction = (CompetedApointments c) => (iSortCol != 1) ? c.pateintname : c.pateintname;
				list = ((!(sortOrder == "desc")) ? list.OrderBy(orderingFunction).ToList() : list.OrderByDescending(orderingFunction).ToList());
			}
			return list;
		}

		[HttpGet]
		[Route("api/CompeltedAppoinemntbyday")]
		public SysDataTablePager<CompetedApointments> CompeltedAppoinemntbyday()
		{
			NameValueCollection nvc = HttpUtility.ParseQueryString(base.Request.RequestUri.Query);
			string sEcho = nvc["sEcho"].ToString();
			string sSearch = nvc["sSearch"].ToString();
			int iDisplayStart = Convert.ToInt32(nvc["iDisplayStart"]);
			int iDisplayLength = Convert.ToInt32(nvc["iDisplayLength"]);
			int iSortCol = Convert.ToInt32(nvc["iSortCol_0"]);
			string sortOrder = nvc["sSortDir_0"].ToString();
			NameValue.Add("@id ", "");
			NameValue.Add("@search", sSearch);
			OperationLayer = new DataOperationLayer(ConnectionString);
			DataTable dataSet = OperationLayer.List("sp_CompletedAppointmentbytoday", NameValue);
			List<CompetedApointments> patientModels = new List<CompetedApointments>();
			List<CompetedApointments> Customers = new List<CompetedApointments>();
			int Count = dataSet.Rows.Count;
			int i = 0;
			foreach (DataRow dr in dataSet.Rows)
			{
				i++;
				patientModels.Add(new CompetedApointments
				{
					appoientmentID = dr["AppointID"].ToString(),
					appoinement_date = dr["Appoint"].ToString(),
					pateintid = dr["Patient_ID"].ToString(),
					pateintname = dr["Patient_Name"].ToString(),
					doctorID = dr["UserID"].ToString(),
					doctorName = dr["UserName"].ToString(),
					amount = dr["Amount"].ToString(),
					paymenttype = dr["PaymentType"].ToString(),
					pidandaid = dr["Patient_Name"].ToString() + "$" + dr["Patient_ID"].ToString() + "$" + dr["AppointID"].ToString()
				});
				Customers = SortFunction(iSortCol, sortOrder, patientModels).Skip(iDisplayStart).Take(iDisplayLength).ToList();
			}
			return new SysDataTablePager<CompetedApointments>(Customers, Count, Count, sEcho);
		}

		private List<CompetedApointments> SortFunction(int iSortCol, string sortOrder, List<CompetedApointments> list, int? id, string name)
		{
			if (iSortCol == 1 || iSortCol == 4)
			{
				Func<CompetedApointments, string> orderingFunction = (CompetedApointments c) => (iSortCol != 1) ? c.pateintname : c.pateintname;
				list = ((!(sortOrder == "desc")) ? list.OrderBy(orderingFunction).ToList() : list.OrderByDescending(orderingFunction).ToList());
			}
			return list;
		}

		[HttpGet]
		[Route("api/ClinicStatus")]
		public Response ClinicStatus(string id)
		{
			ClinicStatus clinic = new ClinicStatus();
			NameValue.Add("@id ", id);
			OperationLayer = new DataOperationLayer(ConnectionString);
			DataSet dataSet = OperationLayer.Statusget("sp_ClinicStatus", NameValue);
			if (dataSet.Tables.Count > 0)
			{
				clinic.TotalPateint = dataSet.Tables[0].Rows[0].ItemArray[0].ToString();
				clinic.TotalCash = dataSet.Tables[1].Rows[0].ItemArray[0].ToString();
				clinic.MonthlySales = dataSet.Tables[2].Rows[0].ItemArray[0].ToString();
				clinic.AppointmentsDue = dataSet.Tables[3].Rows[0].ItemArray[0].ToString();
				clinic.todaySales = dataSet.Tables[4].Rows[0].ItemArray[0].ToString();
			}
			else
			{
				clinic.TotalPateint = "";
				clinic.TotalPateint = "";
				clinic.MonthlySales = "";
				clinic.AppointmentsDue = "";
				clinic.todaySales = "";
			}
			res.Object = clinic;
			res.status = "success";
			return res;
		}

		[HttpGet]
		[Route("api/getexpensebyid")]
		public Response getexpensebyid(string id)
		{
			NameValue.Add("@id", id);
			NameValue.Add("@Amount", "");
			NameValue.Add("@Date", "");

			NameValue.Add("@title", "");
			NameValue.Add("@action", "getbyid");
			NameValue.Add("@search", "");
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_Expenses", NameValue);
			if (json == null || json == "")
			{
				res.status = "fail";
			}
			else
			{
				res.status = "success";
				res.Object = json;
			}
			return res;
		}

		[HttpPost]
		[Route("api/AddExpenses")]
		public Response AddExpenses([FromBody] Expenses expenses)
		{
			NameValue.Add("@id", "");
			NameValue.Add("@Amount", expenses.Amount);

			NameValue.Add("@Date", expenses.date);


			NameValue.Add("@title", expenses.title);

			NameValue.Add("@action", "insert");
			NameValue.Add("@search", "");
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_Expenses", NameValue);
			if (json == null || json == "")
			{
				res.status = "fail";
			}
			else
			{
				res.status = "success";
				res.Object = json;
			}
			return res;
		}

		[HttpPost]
		[Route("api/UpdateExpenses")]
		public Response UpdateExpenses([FromBody] Expenses expenses)
		{
			NameValue.Add("@id", expenses.Id);
			NameValue.Add("@Amount", expenses.Amount);

			NameValue.Add("@Date", expenses.date);

			NameValue.Add("@title", expenses.title);
			NameValue.Add("@action", "update");
			NameValue.Add("@search", "");
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_Expenses", NameValue);
			if (json == null || json == "")
			{
				res.status = "fail";
			}
			else
			{
				res.status = "success";
				res.Object = json;
			}
			return res;
		}

		[HttpGet]
		[Route("api/deleteexpensebyid")]
		public Response deleteexpensebyid(string id)
		{
			NameValue.Add("@id", id);
			NameValue.Add("@Amount", "");

			NameValue.Add("@Date", "");

			NameValue.Add("@title", "");
			NameValue.Add("@action", "delete");
			NameValue.Add("@search", "");
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_Expenses", NameValue);
			if (json == null || json == "")
			{
				res.status = "fail";
			}
			else
			{
				res.status = "success";
				res.Object = json;
			}
			return res;
		}

		[HttpGet]
		[Route("api/ExpensesList")]
		public SysDataTablePager<Expenses> ExpensesList()
		{
			NameValueCollection nvc = HttpUtility.ParseQueryString(base.Request.RequestUri.Query);
			string sEcho = nvc["sEcho"].ToString();
			string sSearch = nvc["sSearch"].ToString();
			int iDisplayStart = Convert.ToInt32(nvc["iDisplayStart"]);
			int iDisplayLength = Convert.ToInt32(nvc["iDisplayLength"]);
			int iSortCol = Convert.ToInt32(nvc["iSortCol_0"]);
			string sortOrder = nvc["sSortDir_0"].ToString();
			NameValue.Add("@id", "");
			NameValue.Add("@Amount", "");
			NameValue.Add("@Date", "");

			NameValue.Add("@title", "");
			NameValue.Add("@action", "ListOfExpenses");
			NameValue.Add("@search", sSearch);
			OperationLayer = new DataOperationLayer(ConnectionString);
			DataSet dataSet = OperationLayer.Statusget("sp_Expenses", NameValue);
			List<Expenses> patientModels = new List<Expenses>();
			List<Expenses> Customers = new List<Expenses>();
			int Count = dataSet.Tables[0].Rows.Count;
			for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
			{
				patientModels.Add(new Expenses
				{
					Id = dataSet.Tables[0].Rows[i][0].ToString(),
					title = dataSet.Tables[0].Rows[i][1].ToString(),
					Amount = dataSet.Tables[0].Rows[i][2].ToString(),
					date = dataSet.Tables[0].Rows[i][5].ToString(),
					total = dataSet.Tables[1].Rows[0][0].ToString(),
					todayTotal = dataSet.Tables[2].Rows[0][0].ToString()
				});
				Customers = SortFunction(iSortCol, sortOrder, patientModels).Skip(iDisplayStart).Take(iDisplayLength).ToList();
			}
			return new SysDataTablePager<Expenses>(Customers, Count, Count, sEcho);
		}

		private List<Expenses> SortFunction(int iSortCol, string sortOrder, List<Expenses> list)
		{
			if (iSortCol == 3 || iSortCol == 3)
			{
				Func<Expenses, string> orderingFunction = (Expenses c) => (iSortCol != 1) ? c.date : c.date;
				list = ((!(sortOrder == "desc")) ? list.OrderBy(orderingFunction).ToList() : list.OrderByDescending(orderingFunction).ToList());
			}
			return list;
		}

		[HttpGet]
		[Route("api/ExpensesListbydate")]
		public SysDataTablePager<Expenses> ExpensesListbydate(string startdate, string endate)
		{
			NameValueCollection nvc = HttpUtility.ParseQueryString(base.Request.RequestUri.Query);
			string sEcho = nvc["sEcho"].ToString();
			string sSearch = nvc["sSearch"].ToString();
			int iDisplayStart = Convert.ToInt32(nvc["iDisplayStart"]);
			int iDisplayLength = Convert.ToInt32(nvc["iDisplayLength"]);
			int iSortCol = Convert.ToInt32(nvc["iSortCol_0"]);
			string sortOrder = nvc["sSortDir_0"].ToString();
			NameValue.Add("@start", startdate);
			NameValue.Add("@end", endate);
			NameValue.Add("@search", sSearch);
			OperationLayer = new DataOperationLayer(ConnectionString);
			DataSet dataSet = OperationLayer.Statusget("sp_get_expensebydatefilter", NameValue);
			List<Expenses> patientModels = new List<Expenses>();
			List<Expenses> Customers = new List<Expenses>();
			int Count = dataSet.Tables[0].Rows.Count;
			for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
			{
				patientModels.Add(new Expenses
				{
					Id = dataSet.Tables[0].Rows[i][0].ToString(),
					title = dataSet.Tables[0].Rows[i][1].ToString(),
					Amount = dataSet.Tables[0].Rows[i][2].ToString(),
					date = dataSet.Tables[0].Rows[i][5].ToString(),
					todayTotal = dataSet.Tables[1].Rows[0][0].ToString()
				});
				Customers = SortFunction(iSortCol, sortOrder, patientModels).Skip(iDisplayStart).Take(iDisplayLength).ToList();
			}
			return new SysDataTablePager<Expenses>(Customers, Count, Count, sEcho);
		}

		private List<Expenses> SortFunction(int iSortCol, string sortOrder, List<Expenses> list, int? s)
		{
			if (iSortCol == 3 || iSortCol == 3)
			{
				Func<Expenses, string> orderingFunction = (Expenses c) => (iSortCol != 1) ? c.date : c.date;
				list = ((!(sortOrder == "desc")) ? list.OrderBy(orderingFunction).ToList() : list.OrderByDescending(orderingFunction).ToList());
			}
			return list;
		}


		[HttpPost]
		[Route("api/Appointment/ADDprescription")]
		public Response ADDprescription([FromBody] Prescription p)
		{
			NameValue.Add("@Presid", p.Presid.ToString());

			NameValue.Add("@Pid", p.Pid.ToString());

			NameValue.Add("@AppointmentID", p.AppointmentID.ToString());


			NameValue.Add("@Medication", WebUtility.HtmlEncode(p.Medication));


			NameValue.Add("@Investigations", WebUtility.HtmlEncode(p.Investigations));


			NameValue.Add("@Referral", WebUtility.HtmlEncode(p.Referral));

			NameValue.Add("@IsActive", "");

			NameValue.Add("@Advice", WebUtility.HtmlEncode(p.Advice));


			NameValue.Add("@Return_to_Clinic", WebUtility.HtmlEncode(p.Return_to_Clinic));



			NameValue.Add("@action", "insert");
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_prescription", NameValue);

			if (json == null || json == "")
			{
				res.status = "fail";
			}
			else
			{
				res.status = "success";
				res.Object = json;
			}
			return res;
		}



		[HttpPost]
		[Route("api/Appointment/EditPrescription")]
		public Response EditPrescription([FromBody] Prescription p)
		{
			NameValue.Add("@Presid", p.Presid.ToString());

			NameValue.Add("@Pid", p.Pid.ToString());
			NameValue.Add("@AppointmentID", p.AppointmentID.ToString());


			NameValue.Add("@Medication", WebUtility.HtmlEncode(p.Medication));


			NameValue.Add("@Investigations", WebUtility.HtmlEncode(p.Investigations));


			NameValue.Add("@Referral", WebUtility.HtmlEncode(p.Referral));

			NameValue.Add("@IsActive", "");

			NameValue.Add("@Advice", WebUtility.HtmlEncode(p.Advice));


			NameValue.Add("@Return_to_Clinic", WebUtility.HtmlEncode(p.Return_to_Clinic));


			NameValue.Add("@action", "update");
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_prescription", NameValue);

			if (json == null || json == "")
			{
				res.status = "fail";
			}
			else
			{
				res.status = "success";
				res.Object = json;
			}
			return res;
		}




		[HttpGet]
		[Route("api/Appointment/DeletePrescription")]
		public Response DeletePrescription(int id)
		{
			NameValue.Add("@Presid", id.ToString());

			NameValue.Add("@Pid", "");
			NameValue.Add("@AppointmentID", "");


			NameValue.Add("@Medication", "");


			NameValue.Add("@Investigations", "");


			NameValue.Add("@Referral", "");

			NameValue.Add("@IsActive", "");

			NameValue.Add("@Advice", "");


			NameValue.Add("@Return_to_Clinic", "");


			NameValue.Add("@action", "delete");
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_prescription", NameValue);

			if (json == null || json == "")
			{
				res.status = "fail";
			}
			else
			{
				res.status = "success";
				res.Object = json;
			}
			return res;
		}


		[HttpGet]
		[Route("api/Appointment/GetPrescriptionbyid")]
		public Response GetPrescriptionbyid(int id)
		{
			NameValue.Add("@Presid", id.ToString());
	

			NameValue.Add("@Pid", "");

			NameValue.Add("@AppointmentID", "");

			NameValue.Add("@Medication", "");


			NameValue.Add("@Investigations", "");


			NameValue.Add("@Referral", "");

			NameValue.Add("@IsActive", "");

			NameValue.Add("@Advice", "");


			NameValue.Add("@Return_to_Clinic", "");


			NameValue.Add("@action", "getbyid");
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_prescription", NameValue);

			if (json == null || json == "")
			{
				res.status = "fail";
			}
			else
			{
				res.status = "success";
				res.Object = json;
			}

			return res;

		}





		[HttpPost]
		[Route("api/Appointment/SuggestPrescription")]
		public Response SuggestPrescription(Prescription prescription)
		{
			NameValue.Add("@Presid", prescription.Presid.ToString());


			NameValue.Add("@Pid", "");

			NameValue.Add("@AppointmentID", "");

			NameValue.Add("@Medication", "");


			NameValue.Add("@Investigations", "");


			NameValue.Add("@Referral", "");

			NameValue.Add("@IsActive", "");

			NameValue.Add("@Advice", "");


			NameValue.Add("@Return_to_Clinic", "");


			NameValue.Add("@action", "suggust");
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_prescription", NameValue);

			if (json == null || json == "")
			{
				res.status = "fail";
			}
			else
			{
				res.status = "success";
				res.Object = json;
			}
			return res;
		}


		[HttpGet]
		[Route("api/Appoinments/CompeltedAppoinemntByDoctor")]
		public SysDataTablePager<CompetedApointments> CompeltedAppoinemntByDoctor(string startdate, string enddate, int id)
		{





			NameValueCollection nvc = HttpUtility.ParseQueryString(base.Request.RequestUri.Query);
			string sEcho = nvc["sEcho"].ToString();
			string sSearch = nvc["sSearch"].ToString();
			int iDisplayStart = Convert.ToInt32(nvc["iDisplayStart"]);
			int iDisplayLength = Convert.ToInt32(nvc["iDisplayLength"]);
			int iSortCol = Convert.ToInt32(nvc["iSortCol_0"]);
			string sortOrder = nvc["sSortDir_0"].ToString();
			NameValue.Add("@id ", id.ToString());
			NameValue.Add("@startdate", startdate == null ? "" : startdate);
			NameValue.Add("@enddate", enddate == null ? "" : enddate);
			NameValue.Add("@search", sSearch);
			OperationLayer = new DataOperationLayer(ConnectionString);
			DataTable dataSet = OperationLayer.List("sp_CompletedAppointmentByDoctor", NameValue);
			List<CompetedApointments> patientModels = new List<CompetedApointments>();
			List<CompetedApointments> Customers = new List<CompetedApointments>();
			int Count = dataSet.Rows.Count;
			int i = 0;
			foreach (DataRow dr in dataSet.Rows)
			{
				i++;
				patientModels.Add(new CompetedApointments
				{
					appoientmentID = dr["AppointID"].ToString(),
					appoinement_date = dr["Appoint"].ToString(),
					pateintid = dr["Patient_ID"].ToString(),
					pateintname = dr["Patient_Name"].ToString(),
					doctorID = dr["UserID"].ToString(),
					doctorName = dr["UserName"].ToString(),
					amount = dr["Amount"].ToString(),
					paymenttype = dr["PaymentType"].ToString(),
					pidandaid = dr["Patient_Name"].ToString() + "$" + dr["Patient_ID"].ToString() + "$" + dr["AppointID"].ToString()
				});
				Customers = SortFunction(iSortCol, sortOrder, patientModels).Skip(iDisplayStart).Take(iDisplayLength).ToList();
			}




			return new SysDataTablePager<CompetedApointments>(Customers, Count, Count, sEcho);
		}

		private List<CompetedApointments> SortFunction(int iSortCol, string sortOrder, List<CompetedApointments> list,int? id , int? aa)
		{
			if (iSortCol == 1 || iSortCol == 4)
			{
				Func<CompetedApointments, string> orderingFunction = (CompetedApointments c) => (iSortCol != 1) ? c.pateintname : c.pateintname;
				list = ((!(sortOrder == "desc")) ? list.OrderBy(orderingFunction).ToList() : list.OrderByDescending(orderingFunction).ToList());
			}
			return list;
		}



		[HttpGet]
		[Route("api/Appointment/print")]
		public Response print(int id)
		{
			NameValue.Add("@Presid", id.ToString());

			NameValue.Add("@Pid", "");

			NameValue.Add("@AppointmentID", "");

			NameValue.Add("@Medication", "");


			NameValue.Add("@Investigations", "");


			NameValue.Add("@Referral", "");

			NameValue.Add("@IsActive", "");

			NameValue.Add("@Advice", "");


			NameValue.Add("@Return_to_Clinic", "");


			NameValue.Add("@action", "print");
			OperationLayer = new DataOperationLayer(ConnectionString);
			var json = OperationLayer.callStoredProcedure("sp_prescription", NameValue);

            if (json == null || json == "")
            {
                res.status = "fail";
            }
            else
            {
                res.status = "success";
                res.Object = json;
            }
			return res;

   //         Prescription prescription = new Prescription()
   //         {
   //             Medication = data.Tables[0].Rows[0][0].ToString(),
   //             Return_to_Clinic = data.Tables[0].Rows[0][1].ToString(),
   //             Investigations = data.Tables[0].Rows[0][2].ToString(),
   //             Referral = data.Tables[0].Rows[0][3].ToString(),
   //             Advice = data.Tables[0].Rows[0][4].ToString(),
   //             PatientID = data.Tables[0].Rows[0][5].ToString(),
   //             Patient_Name = data.Tables[0].Rows[0][6].ToString(),
   //             Patient_Gender = data.Tables[0].Rows[0][7].ToString(),
   //             MR_No = data.Tables[0].Rows[0][8].ToString(),
   //             DateOfBirth = data.Tables[0].Rows[0][9].ToString(),





   //         };
   //         var fileName = "compassioncouch" + DateTime.Now.ToString("MM-dd-yyyy_HHmmss") + ".pdf";
   //         string FolderPath = Path.Combine(HttpContext.Current.Server.MapPath("~/prescriptions"));
   //         if (!Directory.Exists(FolderPath))
   //         {
   //             Directory.CreateDirectory(FolderPath);
   //         }
   //         string fullPath = Path.Combine(HttpContext.Current.Server.MapPath("~/prescriptions"), fileName);

   //         Export export = new Export();




   //         string file = export.CreateStudentPdf(prescription);

   //         string path = Path.Combine(HttpContext.Current.Server.MapPath("~/prescriptions"), file);


   //         Response response = new Response();
			//response.status = "success";


			//response.Object = file;

			//return response;


			
		}



		[HttpGet]
		[Route("api/Appointment/Predicate")]
		public SysDataTablePager<Prescription> PredicateList(int PateintID)
		{
			NameValueCollection nvc = HttpUtility.ParseQueryString(base.Request.RequestUri.Query);
			string sEcho = nvc["sEcho"].ToString();
			string sSearch = nvc["sSearch"].ToString();
			int iDisplayStart = Convert.ToInt32(nvc["iDisplayStart"]);
			int iDisplayLength = Convert.ToInt32(nvc["iDisplayLength"]);
			int iSortCol = Convert.ToInt32(nvc["iSortCol_0"]);
			string sortOrder = nvc["sSortDir_0"].ToString();
			NameValue.Add("@PateintID", PateintID.ToString());
	
			NameValue.Add("@search", sSearch);
			OperationLayer = new DataOperationLayer(ConnectionString);
			DataSet dataSet = OperationLayer.Statusget("sp_Prescription_Server", NameValue);
			List<Prescription> patientModels = new List<Prescription>();
			List<Prescription> Customers = new List<Prescription>();
			int Count = dataSet.Tables[0].Rows.Count;
			for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
			{
				patientModels.Add(new Prescription
				{
                    Presid = Convert.ToInt32(dataSet.Tables[0].Rows[i][0]),
                    Pid = Convert.ToInt32(dataSet.Tables[0].Rows[i][1].ToString()),
                    //prescription = dataSet.Tables[0].Rows[i][2].ToString(),
                    //FromDate = dataSet.Tables[0].Rows[i][3].ToString(),
                    //ToDate = dataSet.Tables[0].Rows[i][4].ToString(),
					Medication= dataSet.Tables[0].Rows[i][2].ToString(),
					IsActive=Convert.ToInt32(dataSet.Tables[0].Rows[i][3]),
					Investigations = dataSet.Tables[0].Rows[i][4].ToString(),

					Return_to_Clinic = dataSet.Tables[0].Rows[i][5].ToString(),
					Referral= dataSet.Tables[0].Rows[i][6].ToString(),
					Advice= dataSet.Tables[0].Rows[i][7].ToString(),
					Patient_Email = dataSet.Tables[0].Rows[i][9].ToString()



				});
				Customers = SortFunction(iSortCol, sortOrder, patientModels,"").Skip(iDisplayStart).Take(iDisplayLength).ToList();
			}
			return new SysDataTablePager<Prescription>(Customers, Count, Count, sEcho);
		}

		private List<Prescription> SortFunction(int iSortCol, string sortOrder, List<Prescription> list,string a)
		{
			if (iSortCol == 3 || iSortCol == 3)
			{
				Func<Prescription, string> orderingFunction = (Prescription c) => (iSortCol != 1) ? c.Medication : c.Medication;
				list = ((!(sortOrder == "desc")) ? list.OrderBy(orderingFunction).ToList() : list.OrderByDescending(orderingFunction).ToList());
			}
			return list;
		}



		[HttpGet]
		[Route("api/Appointment/getAllCampus")]
		public Response getAllCampus()
		{
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_getAllCampus", NameValue);

			if (json == null || json == "")
			{
				res.status = "fail";
			}
			else
			{
				res.status = "success";
				res.Object = json;
			}
			return res;
		}



		[HttpGet]
		[Route("api/Appointment/getPatientand_campus_and_doctors")]
		public Response getPatientand_campus_and_doctors()
		{

            try { 
			OperationLayer = new DataOperationLayer(ConnectionString);
			
		var data=  OperationLayer.Statusget("sp_getPatientand_campus_and_doctors", NameValue);

			PatientDTO patientModels = new  PatientDTO();


            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
            {
				patientModels.patients.Add(new Patient
				{
				 PatientID= Convert.ToInt32(data.Tables[0].Rows[i][0]),
				 PatientName= data.Tables[0].Rows[i][1].ToString()

				}

					) ;


            }






			for (int i = 0; i < data.Tables[1].Rows.Count; i++)
			{
				patientModels.campuses.Add(new Campus
				{
					CampusID = Convert.ToInt32(data.Tables[1].Rows[i][0]),
					CampusName = data.Tables[1].Rows[i][1].ToString()

				}

					);


			}

			for (int i = 0; i < data.Tables[2].Rows.Count; i++)
			{
				patientModels.doctors.Add(new Doctor
				{
					DoctorID = Convert.ToInt32(data.Tables[2].Rows[i][0]),
					DoctorName = data.Tables[2].Rows[i][1].ToString()

				}

					);


			}


				res.status = "200";

			res.Object = patientModels;

			}
			catch(Exception e)
            {

				res.status = "500";
				res.message = e.Message.ToString();



            }

			return res;
		}



		[HttpGet]
		[Route("api/Appointment/NotCompleted")]
		public Response NotCompleted(int id)
		{
			OperationLayer = new DataOperationLayer(ConnectionString);
			NameValue.Add("@id", id.ToString());
			string json = OperationLayer.callStoredProcedure("sp_updateAppointmentStatus_not_Complete", NameValue);

			if (json == null || json == "")
			{
				res.status = "fail";
			}
			else
			{
				res.status = "success";
				res.Object = json;
			}
			return res;
		}



		[HttpPost]
		[Route("api/Appointment/GenerateReport")]
		public Response GenerateReport([FromBody]ReportTimeandIDs obj)

		{


			NameValue.Add("@ids", obj.ids);
			NameValue.Add("@startdate", obj.startdate);

			NameValue.Add("@enddate", obj.enddate);

			OperationLayer = new DataOperationLayer(ConnectionString);
			DataSet dataSet = OperationLayer.Statusget("sp_GenerateReport", NameValue);
			List<ClinciReportsPOCO> clinciReports = new List<ClinciReportsPOCO>();
			int Count = dataSet.Tables[0].Rows.Count;
			for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
			{
				clinciReports.Add(new ClinciReportsPOCO
				{
					SNo=+1,

				PatientName	 = dataSet.Tables[0].Rows[i][0].ToString(),
					DoctorName =dataSet.Tables[0].Rows[i][1].ToString(),
					Age = dataSet.Tables[0].Rows[i][2] ==null? "":  dataSet.Tables[0].Rows[i][2].ToString(),
					DateofAppointment = dataSet.Tables[0].Rows[i][3].ToString(),
					Time = dataSet.Tables[0].Rows[i][4].ToString(),
					AppointmentType = dataSet.Tables[0].Rows[i][5].ToString()=="1"?"InPerson":"Online",
					AppointmentSatus= dataSet.Tables[0].Rows[i][6].ToString(),

				});
						}

			var fileName = "compassioncouch" + DateTime.Now.ToString("MM-dd-yyyy_HHmmss") + ".xlsx";
				string FolderPath = Path.Combine(HttpContext.Current.Server.MapPath("~/report"));
				if (!Directory.Exists(FolderPath))
				{
					Directory.CreateDirectory(FolderPath);
				}
				string fullPath = Path.Combine(HttpContext.Current.Server.MapPath("~/report"), fileName);
				MemoryStream ms = new MemoryStream();




				var Data = clinciReports;


				ms = Export.DataTableToExcelXlsxForMultipleHorizontalPrint(Data);
				FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
				ms.WriteTo(file);
				file.Close();
				Response response = new Response();
			response.status = "success";


				response.Object = "report" + "/"+fileName;//zipFileName;
				return response;

		}




		[HttpPost]
		[Route("api/Appointment/sendEmail")]
		public async Task <string> sendEmail([FromBody] EmailModel obj)

		{

		 return  await	Email.sendmail(obj.Email, obj.status);

			

		}


		[HttpGet]
		[Route("api/Appointment/sendprescriptionbyemial")]
		public async Task<string> sendprescriptionbyemial(string priId)

		{



            try {

			

				Prescription prescription = new Prescription();

			NameValue.Add("@Presid", priId.ToString());
			
			OperationLayer = new DataOperationLayer(ConnectionString);
			DataSet dataSet = OperationLayer.Statusget("sp_GenratePrescriptionforEmial", NameValue);
				string filePath = HttpContext.Current.Server.MapPath("~/Files/Documents");
				
					if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Files/Documents")))
				{
					Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Files/Documents"));
				}


				prescription.Medication = dataSet.Tables[0].Rows[0][0].ToString();
				prescription.Return_to_Clinic = dataSet.Tables[0].Rows[0][1]==null ? "": dataSet.Tables[0].Rows[0][0].ToString();
				prescription.Investigations = dataSet.Tables[0].Rows[0][2].ToString();
				prescription.Advice= dataSet.Tables[0].Rows[0][3].ToString();
				prescription.Referral = dataSet.Tables[0].Rows[0][4].ToString();
			prescription.Patient_Name= dataSet.Tables[0].Rows[0][5].ToString();
				prescription.Patient_Gender = dataSet.Tables[0].Rows[0][6].ToString();
				prescription.MR_No=dataSet.Tables[0].Rows[0][7].ToString();
				prescription.DateOfBirth = dataSet.Tables[0].Rows[0][8].ToString();

				prescription.DoctorName = dataSet.Tables[0].Rows[0][9].ToString();


				prescription.Patient_Email = dataSet.Tables[0].Rows[0][10].ToString();


				Document doc = new Document(PageSize.A4, 7f , 5f , 5f , 0f );

				string newFilePath = filePath + "\\" + prescription.MR_No+"-"+ DateTime.Now.ToString("MM-dd-yyyy")+".pdf"; //GetFileName(filePath, "UserProfile"); //  
				PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(newFilePath, FileMode.Create));
				doc.Open();
				

				iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/Content/Synapse-Logo.png"));

				//Resize image depend upon your need

				jpg.ScaleToFit(140f, 120f);

				//Give space before image

				jpg.SpacingBefore = 10f;

				//Give some space after the image

				jpg.SpacingAfter = 1f;

				jpg.Alignment = Element.ALIGN_LEFT;

				iTextSharp.text.Font mainFont = FontFactory.GetFont("Segoe UI", 22, new iTextSharp.text.BaseColor(System.Drawing.ColorTranslator.FromHtml("#999")));
				iTextSharp.text.Font infoFont1 = FontFactory.GetFont("Kalinga", 10, new iTextSharp.text.BaseColor(System.Drawing.ColorTranslator.FromHtml("#666")));
				iTextSharp.text.Font expHeadFond = FontFactory.GetFont("Calibri (Body)", 12, new iTextSharp.text.BaseColor(System.Drawing.ColorTranslator.FromHtml("#666")));
				PdfContentByte contentByte = writer.DirectContent;
				ColumnText ct = new ColumnText(contentByte);
				PdfPTable modelInfoTable = new PdfPTable(1);
				modelInfoTable.TotalWidth = 100f;
				PdfPCell modelInfoCell1 = new PdfPCell()
				{
					BorderWidthBottom = 0f ,
					BorderWidthTop = 0f ,
					BorderWidthLeft = 0f ,
					BorderWidthRight = 0f 
				};
				//Set right hand the first heading  
				Phrase mainPharse = new Phrase();
				Chunk mChunk = new Chunk("Name" + " " + prescription.Patient_Name, mainFont);
				mainPharse.Add(mChunk);

				Chunk mChunk2 = new Chunk("Medical Record (MR) Number" + " : " + prescription.MR_No, mainFont);
				mainPharse.Add(mChunk2);


				mainPharse.Add(new Chunk(Environment.NewLine));


				Chunk mChunk3 = new Chunk("Age" + " " + prescription.DateOfBirth, mainFont);
				mainPharse.Add(mChunk3);

				Chunk mChunk4 = new Chunk("Date" + " : " + DateTime.Now.ToString("MM-dd-yyyy"), mainFont);
				mainPharse.Add(mChunk4);


				mainPharse.Add(new Chunk(Environment.NewLine));


				PdfPCell cell1 = new PdfPCell()
				{
					BorderWidthBottom = 0f ,
					BorderWidthTop = 0f ,
					BorderWidthLeft = 0f ,
					BorderWidthRight = 0f 
				};
				cell1.PaddingTop = 5 ;
				Phrase bioPhrase = new Phrase();
                if (prescription.Medication!="")
                {

					Chunk bioChunk1 = new Chunk("Medication" + "  " + prescription.Medication, mainFont);
					bioPhrase.Add(bioChunk1);
					bioPhrase.Add(new Chunk(Environment.NewLine));

				}
                if (prescription.Investigations != "")
                {
					Chunk bioChunk2 = new Chunk("Investigations" + "  " + prescription.Investigations, mainFont);
					bioPhrase.Add(bioChunk2);
					bioPhrase.Add(new Chunk(Environment.NewLine));
				}

				if (prescription.Referral != "")
				{
					Chunk bioChunk3 = new Chunk("Referrel" + "  " + prescription.Referral, mainFont);
					bioPhrase.Add(bioChunk3);
					bioPhrase.Add(new Chunk(Environment.NewLine));
				}

                if (prescription.Return_to_Clinic != "")
                {
					Chunk bioChunk4 = new Chunk("Return_to_Clinic" + "  " + prescription.Return_to_Clinic, mainFont);
					bioPhrase.Add(bioChunk4);
					bioPhrase.Add(new Chunk(Environment.NewLine));

				}

					Chunk bioChunk = new Chunk("DoctorName" + "  " + prescription.DoctorName, mainFont);
					bioPhrase.Add(bioChunk);
				Chunk bioChunk5 = new Chunk("DoctorName" + "______________________________" );
				bioPhrase.Add(bioChunk5);


				bioPhrase.Add(new Chunk(Environment.NewLine));


				doc.Add(modelInfoTable);
		

				return await Email.sendPrescription(prescription.Patient_Email,"");

			}

			catch (Exception e)
            {

				return e.Message;


			}

		}















	}

}
