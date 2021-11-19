using ClinicAPI.HelperCode;
using ClinicAPI.Models;
using ClinicAPI.Models.pagination;
using ClinicAPI.Models.Request;
using DataConnection;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ClinicAPI.Controllers
{
    public class PatientRegistrationController : ApiController
    {
		private NameValueCollection NameValue = new NameValueCollection();

		private DataOperationLayer OperationLayer;

		private Response res = new Response();

		private EmailSender Email = new EmailSender();

		private string ConnectionString = ConfigurationManager.AppSettings["Connectionstring"];

		[Route("api/PatientRegistration")]
		[HttpPost]
		public async Task<Response> Registration([FromBody] PatientRegistration model)
		{
			if (model.Patient_Name == "" || model.Patient_Name == null)
			{
				res.status = "Fail";
				res.message = "User name is Required";
				return res;
			}
			//if (model.DateOfBirth == "" || model.DateOfBirth == null)
			//{
			//	res.status = "Fail";
			//	res.message = " Patient DateOfBirth is Required";
			//	return res;
			//}
			if (model.CityID == "" || model.CityID == "")
			{
				res.status = "Fail";
				res.message = " Patient City is Required";
				return res;
			}
			string ApplicationURL = ConfigurationManager.AppSettings["ApplicationURL"];
			if (model.ImageBase64 != null && model.ImageBase64.Length != 0)
			{
				for (int i = 0; i < model.ImageBase64.Length; i++)
				{
					string fileextension = Utilities.GetMimeType(model.ImageBase64[i]).Extension;
					byte[] bytes = Convert.FromBase64String(model.ImageBase64[i]);
					string filename = Utilities.AppendTimeStamp("document" + fileextension);
					string filePath = HttpContext.Current.Server.MapPath("~/Files/Documents/" + filename);
					if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Files/Documents")))
					{
						Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Files/Documents"));
					}
					FileStream stream = new FileStream(filePath, FileMode.CreateNew);
					BinaryWriter writer = new BinaryWriter(stream);
					writer.Write(bytes, 0, bytes.Length);
					writer.Close();
					model.Documents = ((filename != "") ? (ApplicationURL + "/Files/Documents/" + filename) : "");
				}
			}
			NameValue.Clear();

			NameValue.Add("@PID","");
			NameValue.Add("@Patient_Name", model.Patient_Name);
			NameValue.Add("@Patient_Email", model.Patient_Email);
			NameValue.Add("@Patient_Phone_Number", model.Patient_Phone_Number);
			NameValue.Add("@Patient_Mobile_Number", model.Patient_Mobile_Number);
			NameValue.Add("@Patient_Address", model.Patient_Address);
			NameValue.Add("@CNIC", model.CNIC);
			NameValue.Add("@DateOfBirth", model.DateOfBirth );
			NameValue.Add("@Patient_Gender", model.Patient_Gender);
			NameValue.Add("@Documents", "");
			NameValue.Add("@CityID", model.CityID);
			NameValue.Add("@Place_Of_Birth", model.Place_Of_Birth);
			NameValue.Add("@comment", model.Comment);
			NameValue.Add("@Action", "insert");
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

		[Route("api/PatientUpdate")]
		[HttpPost]
		public Response Update([FromBody] PatientRegistration model)
		{
			if (model.Patient_Name == "" || model.Patient_Name == null)
			{
				res.status = "Fail";
				res.message = "User name is Required";
				return res;
			}
			string ApplicationURL = ConfigurationManager.AppSettings["ApplicationURL"];
			string doc = "";
			if (model.ImageBase64 != null && model.ImageBase64.Length != 0)
			{
				string fileextension = Utilities.GetMimeType(model.ImageBase64[0]).Extension;
				byte[] bytes = Convert.FromBase64String(model.ImageBase64[0]);
				string filename = Utilities.AppendTimeStamp("document" + fileextension);
				string filePath = HttpContext.Current.Server.MapPath("~/Files/Documents/" + filename);
				if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Files/Documents")))
				{
					Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Files/Documents"));
				}
				FileStream stream = new FileStream(filePath, FileMode.CreateNew);
				BinaryWriter writer = new BinaryWriter(stream);
				writer.Write(bytes, 0, bytes.Length);
				writer.Close();
				doc = ((filename != "") ? (ApplicationURL + "/Files/Documents/" + filename) : "");
			}
			NameValue.Clear();
			NameValue.Add("@PID", model.PatientID);
			NameValue.Add("@Patient_Name", model.Patient_Name);
			NameValue.Add("@Patient_Email", model.Patient_Email);
			NameValue.Add("@Patient_Phone_Number", model.Patient_Phone_Number);
			NameValue.Add("@Patient_Mobile_Number", model.Patient_Mobile_Number);
			NameValue.Add("@Patient_Address", model.Patient_Address);
			NameValue.Add("@CNIC", model.CNIC);
			NameValue.Add("@DateOfBirth", model.DateOfBirth);
			NameValue.Add("@Patient_Gender", model.Patient_Gender);
			NameValue.Add("@Documents", "");
			NameValue.Add("@CityID", model.CityID);
			NameValue.Add("@Place_Of_Birth", model.Place_Of_Birth);
			NameValue.Add("@comment", model.Comment);
			NameValue.Add("@Action", "update");
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

		[Route("api/PatientList")]
		public SysDataTablePager<PatientModel> GetDatatable()
		{
			NameValueCollection nvc = HttpUtility.ParseQueryString(base.Request.RequestUri.Query);
			string sEcho = nvc["sEcho"].ToString();
			string sSearch = nvc["sSearch"].ToString();
			int iDisplayStart = Convert.ToInt32(nvc["iDisplayStart"]);
			int iDisplayLength = Convert.ToInt32(nvc["iDisplayLength"]);
			int iSortCol = Convert.ToInt32(nvc["iSortCol_0"]);
			string sortOrder = nvc["sSortDir_0"].ToString();
			NameValue.Add("@search", sSearch);
			OperationLayer = new DataOperationLayer(ConnectionString);
			DataTable dataSet = OperationLayer.List("sp_get_all_Patient", NameValue);
			List<PatientModel> patientModels = new List<PatientModel>();
			List<PatientModel> Customers = new List<PatientModel>();
			int Count = dataSet.Rows.Count;
			int i = 0;
			foreach (DataRow dr in dataSet.Rows)
			{
				i++;
				patientModels.Add(new PatientModel
				{
					ID = i,
					Patient_ID = dr["Patient_ID"].ToString(),
					Patient_Name = dr["Patient_Name"].ToString(),
					Patient_Email = dr["Patient_Email"].ToString(),
					Patient_Phone_Number = dr["Patient_Phone_Number"].ToString(),
					Patient_Mobile_Number = dr["Patient_Mobile_Number"].ToString(),
					Patient_Address = dr["Patient_Address"].ToString(),
					CNIC = dr["CNIC"].ToString(),
					DateOfBirth = dr["DateOFBirthNottime"].ToString(),
					Patient_Gender = dr["Patient_Gender"].ToString(),
					Documents = dr["Documents"].ToString(),
					CityID = dr["CityID"].ToString(),
					IsActive = Convert.ToBoolean(dr["IsActive"]),
					Place_of_Birth = dr["Place_of_Birth"].ToString(),
					cityname = dr["City_Name"].ToString(),
					pcityname = dr["City_Name"].ToString(),
					comment = dr["comment"].ToString(),
					MR_No=dr["MR_No"].ToString()==null ?"": dr["MR_No"].ToString(),
				});
				Customers = SortFunction(iSortCol, sortOrder, patientModels).Skip(iDisplayStart).Take(iDisplayLength).ToList();
			}
			return new SysDataTablePager<PatientModel>(Customers, Count, Count, sEcho);
		}

		private List<PatientModel> SortFunction(int iSortCol, string sortOrder, List<PatientModel> list)
		{
			if (iSortCol == 1 || iSortCol == 4)
			{
				Func<PatientModel, string> orderingFunction = (PatientModel c) => (iSortCol != 1) ? ((iSortCol != 4) ? c.Patient_Name : c.cityname) : c.Patient_Name;
				list = ((!(sortOrder == "desc")) ? list.OrderBy(orderingFunction).ToList() : list.OrderByDescending(orderingFunction).ToList());
			}
			return list;
		}

		[HttpGet]
		[Route("api/GetPatientByID")]
		public Response Edit(string id)
		{
			NameValue.Add("@PID", id);
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
			NameValue.Add("@Action", "getbyID");
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

		[HttpPost]
		[Route("api/AddInitialAnalysiz")]
		public Response Add_Initial_Analysiz([FromBody] InitialAnalysiz model)
		{
			NameValue.Add("@PkID", "");
			NameValue.Add("@Present_Complaint", WebUtility.HtmlEncode(model.Present_Complaint));
			NameValue.Add("@History_of_Presenting_Complaint", WebUtility.HtmlEncode(model.History_of_Presenting_Complaint));
			NameValue.Add("@Past_psychiatric_history", WebUtility.HtmlEncode(model.Past_psychiatric_history));
			NameValue.Add("@Current_Medication", WebUtility.HtmlEncode(model.Current_Medication));
			NameValue.Add("@Physical_History", WebUtility.HtmlEncode(model.Physical_History));
			NameValue.Add("@Family_History", WebUtility.HtmlEncode(model.Family_History));
			NameValue.Add("@Adolesence", WebUtility.HtmlEncode(model.Adolesence));
			NameValue.Add("@Alcohal_Substance_abuse", WebUtility.HtmlEncode(model.AlcohalSubstance_abuse));
			NameValue.Add("@Premorbid_personality", WebUtility.HtmlEncode(model.Premorbid_personality));
			NameValue.Add("@Mental_State_Examination", model.Mental_State_Examination);
			NameValue.Add("@Appearance_and_Behavior", WebUtility.HtmlEncode(model.Appearance_and_Behavior));
			NameValue.Add("@Speech", WebUtility.HtmlEncode(model.Speech));
			NameValue.Add("@Mood_and_Affect", WebUtility.HtmlEncode(model.Mood_and_Affect));
			NameValue.Add("@Sucidial_ideations", WebUtility.HtmlEncode(model.Sucidial_ideations));
			NameValue.Add("@Thoughts", WebUtility.HtmlEncode(model.Thoughts));
			NameValue.Add("@Delusions", WebUtility.HtmlEncode(model.Delusions));
			NameValue.Add("@hallucinations", WebUtility.HtmlEncode(model.hallucinations));
			NameValue.Add("@Insight", WebUtility.HtmlEncode(model.Insight));
			NameValue.Add("@Cognitions", WebUtility.HtmlEncode(model.Cognitions));
			NameValue.Add("@Provisional_Diagnosis", WebUtility.HtmlEncode(model.Provisional_Diagnosis));
			NameValue.Add("@differential_diagnosis", WebUtility.HtmlEncode(model.differential_diagnosis));
			NameValue.Add("@Pid", model.Pid);
			NameValue.Add("@schooling", WebUtility.HtmlEncode(model.schooling));
			NameValue.Add("@Maritial_Status", model.Maritial_Status);
		

			NameValue.Add("@occupation", WebUtility.HtmlEncode(model.occupation));

			NameValue.Add("@Bir_ae_child", WebUtility.HtmlEncode(model.Bir_ae_child));
			NameValue.Add("@Name_Of_interviewer", WebUtility.HtmlEncode(model.Name_Of_interviewer));

			NameValue.Add("@CreatedDate",model.CreatedDate.ToString() );


			NameValue.Add("@Action", "insert");
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_Initial_Analysiz", NameValue);
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
		[Route("api/UpdateInitialAnalysiz")]
		public Response UpdateInitialAnalysiz([FromBody] InitialAnalysiz model)
		{
			NameValue.Add("@PkID", model.ID);
			NameValue.Add("@Present_Complaint", WebUtility.HtmlEncode(model.Present_Complaint));
			NameValue.Add("@History_of_Presenting_Complaint", WebUtility.HtmlEncode(model.History_of_Presenting_Complaint));
			NameValue.Add("@Past_psychiatric_history", WebUtility.HtmlEncode(model.Past_psychiatric_history));
			NameValue.Add("@Current_Medication", WebUtility.HtmlEncode(model.Current_Medication));
			NameValue.Add("@Physical_History", WebUtility.HtmlEncode(model.Physical_History));
			NameValue.Add("@Family_History", WebUtility.HtmlEncode(model.Family_History));
			NameValue.Add("@Adolesence", WebUtility.HtmlEncode(model.Adolesence));
			NameValue.Add("@Alcohal_Substance_abuse", WebUtility.HtmlEncode(model.AlcohalSubstance_abuse));
			NameValue.Add("@Premorbid_personality", WebUtility.HtmlEncode(model.Premorbid_personality));
			NameValue.Add("@Mental_State_Examination", WebUtility.HtmlEncode(model.Mental_State_Examination));
			NameValue.Add("@Appearance_and_Behavior", WebUtility.HtmlEncode(model.Appearance_and_Behavior));
			NameValue.Add("@Speech", WebUtility.HtmlEncode(model.Speech));
			NameValue.Add("@Mood_and_Affect", WebUtility.HtmlEncode(model.Mood_and_Affect));
			NameValue.Add("@Sucidial_ideations", WebUtility.HtmlEncode(model.Sucidial_ideations));
			NameValue.Add("@Thoughts", WebUtility.HtmlEncode(model.Thoughts));
			NameValue.Add("@Delusions", WebUtility.HtmlEncode(model.Delusions));
			NameValue.Add("@hallucinations", WebUtility.HtmlEncode(model.hallucinations));
			NameValue.Add("@Insight", WebUtility.HtmlEncode(model.Insight));
			NameValue.Add("@Cognitions", WebUtility.HtmlEncode(model.Cognitions));
			NameValue.Add("@Provisional_Diagnosis", WebUtility.HtmlEncode(model.Provisional_Diagnosis));
			NameValue.Add("@differential_diagnosis", WebUtility.HtmlEncode(model.differential_diagnosis));
			NameValue.Add("@Pid", model.Pid);
			NameValue.Add("@schooling", WebUtility.HtmlEncode(model.schooling));
			NameValue.Add("@Maritial_Status", model.Maritial_Status);

			NameValue.Add("@occupation", WebUtility.HtmlEncode(model.occupation));
			NameValue.Add("@Name_Of_interviewer", WebUtility.HtmlEncode(model.Name_Of_interviewer));

			NameValue.Add("@CreatedDate", model.CreatedDate.ToString());

			NameValue.Add("@Bir_ae_child", WebUtility.HtmlEncode(model.Bir_ae_child));
			NameValue.Add("@Action", "update");
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_Initial_Analysiz", NameValue);
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
		[Route("api/AddFollowupAnalysis")]
		public Response AddFollowupAnalysis([FromBody] FollowupAnalysis model)
		{
			NameValue.Add("@pkid", "");
			NameValue.Add("@CurrentMedication", WebUtility.HtmlEncode(model.CurrentMedication));
			NameValue.Add("@ProgressiveNote", WebUtility.HtmlEncode(model.ProgressiveNote));
			NameValue.Add("@Subjectivenc", WebUtility.HtmlEncode(""));
			NameValue.Add("@ObjectiveandMentalStateExamination", WebUtility.HtmlEncode(model.ObjectiveandMentalStateExamination));
			NameValue.Add("@Assessment", WebUtility.HtmlEncode(model.Assessment));
			NameValue.Add("@Plan", WebUtility.HtmlEncode(""));
			NameValue.Add("@PatientID", model.PatientID);
			NameValue.Add("@Action", "insert");
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_FollowupAnalysis", NameValue);
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
		[Route("api/UpdateFollowupAnalysis")]
		public Response UpdateFollowupAnalysis([FromBody] FollowupAnalysis model)
		{
			NameValue.Add("@pkid", model.pkid);
			NameValue.Add("@CurrentMedication", WebUtility.HtmlEncode(model.CurrentMedication));
			NameValue.Add("@ProgressiveNote", WebUtility.HtmlEncode(model.ProgressiveNote));
			NameValue.Add("@Subjectivenc", WebUtility.HtmlEncode(""));
			NameValue.Add("@ObjectiveandMentalStateExamination", WebUtility.HtmlEncode(model.ObjectiveandMentalStateExamination));
			NameValue.Add("@Assessment", WebUtility.HtmlEncode(model.Assessment));
			NameValue.Add("@Plan",WebUtility.HtmlEncode(""));
			NameValue.Add("@PatientID", model.PatientID);
			NameValue.Add("@Action", "update");
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_FollowupAnalysis", NameValue);
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
		[Route("api/GetFollowupAnalysisbyID")]
		public Response GetFollowupAnalysisbyID(string id)
		{
			NameValue.Add("@pkid", id);
			NameValue.Add("@CurrentMedication", "");
			NameValue.Add("@ProgressiveNote", "");
			NameValue.Add("@Subjectivenc", "");
			NameValue.Add("@ObjectiveandMentalStateExamination", "");
			NameValue.Add("@Assessment", "");
			NameValue.Add("@Plan", "");
			NameValue.Add("@PatientID", "");
			NameValue.Add("@Action", "getbyID");
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_FollowupAnalysis", NameValue);
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
		[Route("api/IsInitialAnalysized")]
		public Response IsInitialAnalysized(string id)
		{
			NameValue.Add("@PkID", "");
			NameValue.Add("@Present_Complaint", "");
			NameValue.Add("@History_of_Presenting_Complaint", "");
			NameValue.Add("@Past_psychiatric_history", "");
			NameValue.Add("@Current_Medication", "");
			NameValue.Add("@Physical_History", "");
			NameValue.Add("@Family_History", "");
			NameValue.Add("@Adolesence", "");
			NameValue.Add("@Alcohal_Substance_abuse", "");
			NameValue.Add("@Premorbid_personality", "");
			NameValue.Add("@Mental_State_Examination", "");
			NameValue.Add("@Appearance_and_Behavior", "");
			NameValue.Add("@Speech", "");
			NameValue.Add("@Mood_and_Affect", "");
			NameValue.Add("@Sucidial_ideations", "");
			NameValue.Add("@Thoughts", "");
			NameValue.Add("@Delusions", "");
			NameValue.Add("@hallucinations", "");
			NameValue.Add("@Insight", "");
			NameValue.Add("@Cognitions", "");
			NameValue.Add("@Provisional_Diagnosis", "");
			NameValue.Add("@differential_diagnosis", "");
			NameValue.Add("@Pid", id);
			NameValue.Add("@schooling", "");
			NameValue.Add("@Maritial_Status", "");

			NameValue.Add("@occupation", "");

			NameValue.Add("@Bir_ae_child", "");
	
			NameValue.Add("@Name_Of_interviewer","");

			NameValue.Add("@CreatedDate", "");

			NameValue.Add("@Action", "getInitialAssessmentbyID");
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_Initial_Analysiz", NameValue);
			if (json == null || json == "")
			{
				res.status = "no";
			}
			else
			{
				res.status = "success";
				res.Object = json;
			}
			return res;
		}

		[HttpGet]
		[Route("api/DeletePatient")]
		public Response Delete(string id)
		{
			NameValue.Add("@PID", id);
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
			NameValue.Add("@Action", "delete");
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
		[Route("api/DeleteAppointment")]
		public Response DeleteAppointment(string id)
		{
			NameValue.Add("@id", id);
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_Delete_Appointment", NameValue);
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

		[Route("api/sp_ALLPatientAppointmentsList")]
		[HttpGet]
		public SysDataTablePager<AppointedPateints> AppointedPatient()
		{
			NameValueCollection nvc = HttpUtility.ParseQueryString(base.Request.RequestUri.Query);
			string sEcho = nvc["sEcho"].ToString();
			string sSearch = nvc["sSearch"].ToString();
			int iDisplayStart = Convert.ToInt32(nvc["iDisplayStart"]);
			int iDisplayLength = Convert.ToInt32(nvc["iDisplayLength"]);
			int iSortCol = Convert.ToInt32(nvc["iSortCol_0"]);
			string sortOrder = nvc["sSortDir_0"].ToString();
			NameValue.Add("@search", sSearch);
			OperationLayer = new DataOperationLayer(ConnectionString);
			DataTable dataSet = OperationLayer.List("sp_ALLPatientAppointments", NameValue);
			List<AppointedPateints> patientModels = new List<AppointedPateints>();
			List<AppointedPateints> Customers = new List<AppointedPateints>();
			int Count = dataSet.Rows.Count;
			int i = 0;
			foreach (DataRow dr in dataSet.Rows)
			{
				i++;
				patientModels.Add(new AppointedPateints
				{
					AppointID = dr["AppointID"].ToString(),
					Appointment_Data = dr["Appoint"].ToString(),
					StartDate = dr["StartDate"].ToString(),
					EndDate = dr["EndDate"].ToString(),
					Patient_ID = dr["Patient_ID"].ToString(),
					Patient_Name = dr["Patient_Name"].ToString(),
					Patient_Email = dr["Patient_Email"].ToString(),
					Patient_Phone_Number = dr["Patient_Phone_Number"].ToString(),
					Patient_Mobile_Number = dr["Patient_Mobile_Number"].ToString(),
					Patient_Address = dr["Patient_Address"].ToString(),
					CNIC = dr["CNIC"].ToString(),
					DateOfBirth = dr["DateOfBirth"].ToString(),
					Patient_Gender = dr["Patient_Gender"].ToString(),
					Documents = dr["Documents"].ToString(),
					CityID = dr["CityID"].ToString(),
					IsActive = Convert.ToBoolean(dr["IsActive"]),
					Place_of_Birth = dr["Place_of_Birth"].ToString(),
					cityname = dr["City_Name"].ToString(),
					pcityname = dr["City_Name"].ToString(),
					DoctorName = dr["UserName"].ToString(),
					pidandaid = dr["Patient_Name"].ToString() + "$" + dr["Patient_ID"].ToString() + "$" + dr["AppointID"].ToString(),
					campus=dr["campus"].ToString(),
					AppType=Convert.ToInt32(dr["AppType"]),
					link=dr["link"].ToString(),

					
				
				
				
				});
				Customers = SortFunction(iSortCol, sortOrder, patientModels).Skip(iDisplayStart).Take(iDisplayLength).ToList();
			}
			return new SysDataTablePager<AppointedPateints>(Customers, Count, Count, sEcho);
		}

		private List<AppointedPateints> SortFunction(int iSortCol, string sortOrder, List<AppointedPateints> list)
		{
			if (iSortCol == 1 || iSortCol == 4)
			{
				Func<AppointedPateints, string> orderingFunction = (AppointedPateints c) => (iSortCol != 1) ? c.Appointment_Data : c.Appointment_Data;
				list = ((!(sortOrder == "desc")) ? list.OrderBy(orderingFunction).ToList() : list.OrderByDescending(orderingFunction).ToList());
			}
			return list;
		}

		[Route("api/AppointedPatientOfDoctor")]
		[HttpGet]
		public SysDataTablePager<AppointedPateints> AppointedPatientOfDoctor(string id)
		{
			NameValueCollection nvc = HttpUtility.ParseQueryString(base.Request.RequestUri.Query);
			string sEcho = nvc["sEcho"].ToString();
			string sSearch = nvc["sSearch"].ToString();
			int iDisplayStart = Convert.ToInt32(nvc["iDisplayStart"]);
			int iDisplayLength = Convert.ToInt32(nvc["iDisplayLength"]);
			int iSortCol = Convert.ToInt32(nvc["iSortCol_0"]);
			string sortOrder = nvc["sSortDir_0"].ToString();
			NameValue.Add("@doctorid", id);
			NameValue.Add("@search", sSearch);
			OperationLayer = new DataOperationLayer(ConnectionString);
			DataTable dataSet = OperationLayer.List("sp_get_appointmentbypatient", NameValue);
			List<AppointedPateints> patientModels = new List<AppointedPateints>();
			List<AppointedPateints> Customers = new List<AppointedPateints>();
			int Count = dataSet.Rows.Count;
			int i = 0;
			foreach (DataRow dr in dataSet.Rows)
			{
				i++;
				patientModels.Add(new AppointedPateints
				{
					AppointID = dr["AppointID"].ToString(),
					Appointment_Data = dr["Appoint"].ToString(),
					StartDate = dr["StartDate"].ToString(),
					EndDate = dr["EndDate"].ToString(),
					Patient_ID = dr["Patient_ID"].ToString(),
					Patient_Name = dr["Patient_Name"].ToString(),
					Patient_Email = dr["Patient_Email"].ToString(),
					Patient_Phone_Number = dr["Patient_Phone_Number"].ToString(),
					Patient_Mobile_Number = dr["Patient_Mobile_Number"].ToString(),
					Patient_Address = dr["Patient_Address"].ToString(),
					CNIC = dr["CNIC"].ToString(),
					DateOfBirth = dr["DateOfBirth"].ToString(),
					Patient_Gender = dr["Patient_Gender"].ToString(),
					Documents = dr["Documents"].ToString(),
					CityID = dr["CityID"].ToString(),
					IsActive = Convert.ToBoolean(dr["IsActive"]),
					Place_of_Birth = dr["Place_of_Birth"].ToString(),
					cityname = dr["City_Name"].ToString(),
					pcityname = dr["City_Name"].ToString(),
					DoctorName = dr["UserName"].ToString(),
					pidandaid = dr["Patient_Name"].ToString() + "$" + dr["Patient_ID"].ToString() + "$" + dr["AppointID"].ToString()
				});
				Customers = SortFunction(iSortCol, sortOrder, patientModels).Skip(iDisplayStart).Take(iDisplayLength).ToList();
			}
			return new SysDataTablePager<AppointedPateints>(Customers, Count, Count, sEcho);
		}

		private List<AppointedPateints> SortFunction(int iSortCol, string sortOrder, List<AppointedPateints> list, int? i)
		{
			if (iSortCol == 1 || iSortCol == 4)
			{
				Func<AppointedPateints, string> orderingFunction = (AppointedPateints c) => (iSortCol != 1) ? c.Appointment_Data : c.Appointment_Data;
				list = ((!(sortOrder == "desc")) ? list.OrderBy(orderingFunction).ToList() : list.OrderByDescending(orderingFunction).ToList());
			}
			return list;
		}

		[Route("api/GetFollowupAnalysisPage")]
		public SysDataTablePager<FollowupAnalysisPage> GetFollowupAnalysis(string id)
		{
			NameValueCollection nvc = HttpUtility.ParseQueryString(base.Request.RequestUri.Query);
			string sEcho = nvc["sEcho"].ToString();
			string sSearch = nvc["sSearch"].ToString();
			int iDisplayStart = Convert.ToInt32(nvc["iDisplayStart"]);
			int iDisplayLength = Convert.ToInt32(nvc["iDisplayLength"]);
			int iSortCol = Convert.ToInt32(nvc["iSortCol_0"]);
			string sortOrder = nvc["sSortDir_0"].ToString();
			NameValue.Add("@PateintID", id);
			NameValue.Add("@search", sSearch);
			OperationLayer = new DataOperationLayer(ConnectionString);
			DataTable dataSet = OperationLayer.List("sp_Followup_Server", NameValue);
			List<FollowupAnalysisPage> patientModels = new List<FollowupAnalysisPage>();
			List<FollowupAnalysisPage> Customers = new List<FollowupAnalysisPage>();
			int Count = dataSet.Rows.Count;
			int i = 0;
			foreach (DataRow dr in dataSet.Rows)
			{
				i++;
				patientModels.Add(new FollowupAnalysisPage
				{
					pkid = dr["PkID"].ToString(),
					CurrentMedication = dr["CurrentMedication"].ToString(),
					ProgressiveNote = dr["ProgressiveNote"].ToString(),
					Subjective = dr["Subjective"].ToString(),
					ObjectiveandMentalStateExamination = dr["ObjectiveandMentalStateExamination"].ToString(),
					Assessment = dr["Assessment"].ToString(),
					Plan = dr["Plan"].ToString(),
					PatientID = dr["PatientID"].ToString(),
					Date = dr["follow_date"].ToString()
				});
				Customers = SortFunction(iSortCol, sortOrder, patientModels).Skip(iDisplayStart).Take(iDisplayLength).ToList();
			}
			return new SysDataTablePager<FollowupAnalysisPage>(Customers, Count, Count, sEcho);
		}

		private List<FollowupAnalysisPage> SortFunction(int iSortCol, string sortOrder, List<FollowupAnalysisPage> list)
		{
			if (iSortCol == 1 || iSortCol == 4)
			{
				Func<FollowupAnalysisPage, string> orderingFunction = (FollowupAnalysisPage c) => (iSortCol != 1) ? c.Subjective : c.Subjective;
				list = ((!(sortOrder == "desc")) ? list.OrderBy(orderingFunction).ToList() : list.OrderByDescending(orderingFunction).ToList());
			}
			return list;
		}

		[HttpGet]
		[Route("api/GetDocumentbyID")]
		public Response GetDocumentbyID(string id)
		{
			NameValue.Add("@PID", id);
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
			NameValue.Add("@Action", "GetDocumentbyID");
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

		[HttpPost]
		[Route("api/ADDocument")]
		public Response ADDDocument([FromBody] Douments douments)
		{
			string ApplicationURL = ConfigurationManager.AppSettings["ApplicationURL"];
			string file = "";
			if (douments.file != null)
			{

				string fileextension = Utilities.GetMimeType(douments.file).Extension == "" ? ".txt" : Utilities.GetMimeType(douments.file).Extension;
				byte[] bytes = Convert.FromBase64String(douments.file);
				string filename = Utilities.AppendTimeStamp("document" + fileextension);
				string filePath = HttpContext.Current.Server.MapPath("~/Files/Documents/" + filename);
				if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Files/Documents")))
				{
					Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Files/Documents"));
				}
				FileStream stream = new FileStream(filePath, FileMode.CreateNew);
				BinaryWriter writer = new BinaryWriter(stream);
				writer.Write(bytes, 0, bytes.Length);
				writer.Close();
				file = ((filename != "") ? (ApplicationURL + "/Files/Documents/" + filename) : "");
			}
			NameValue.Add("@docid", "");
			NameValue.Add("@pateintid", douments.PID);
			NameValue.Add("@file", file);
			NameValue.Add("@filename", douments.Name);
			NameValue.Add("@Action", "insert");
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_documents", NameValue);
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
		[Route("api/UpdateDoc")]
		public Response UpdateDoc([FromBody] Douments douments)
		{
			string ApplicationURL = ConfigurationManager.AppSettings["ApplicationURL"];
			string file = "";
			if (douments.file != null && douments.file != "")
			{

				string fileextension = Utilities.GetMimeType(douments.file).Extension == "" ? ".txt" : Utilities.GetMimeType(douments.file).Extension;
				byte[] bytes = Convert.FromBase64String(douments.file);
				string filename = Utilities.AppendTimeStamp("document" + fileextension);
				string filePath = HttpContext.Current.Server.MapPath("~/Files/Documents/" + filename);
				if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Files/Documents")))
				{
					Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Files/Documents"));
				}
				FileStream stream = new FileStream(filePath, FileMode.CreateNew);
				BinaryWriter writer = new BinaryWriter(stream);
				writer.Write(bytes, 0, bytes.Length);
				writer.Close();
				file = ((filename != "") ? (ApplicationURL + "/Files/Documents/" + filename) : "");
			}
			NameValue.Add("@docid", douments.ID);
			NameValue.Add("@pateintid", "");
			if (douments.file != null && douments.file != "")
			{
				NameValue.Add("@file", file);
			}
			else
			{
				NameValue.Add("@file", douments.Doc);
			}
			NameValue.Add("@filename", douments.Name);
			NameValue.Add("@Action", "update");
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_documents", NameValue);
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
		[Route("api/getdocumentbyidPatient")]
		public Response getdocumentbyidPatient(string id)
		{
			NameValue.Add("@docid", id);
			NameValue.Add("@pateintid", "");
			NameValue.Add("@file", "");
			NameValue.Add("@filename", "");
			NameValue.Add("@Action", "getbyid");
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_documents", NameValue);
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
		[Route("api/DeleteIDDoc")]
		public Response DeleteIDDoc(string id)
		{
			NameValue.Add("@docid", id);
			NameValue.Add("@pateintid", "");
			NameValue.Add("@file", "");
			NameValue.Add("@filename", "");
			NameValue.Add("@Action", "delete");
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_documents", NameValue);
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
		[Route("api/GetALLDocuments")]
		public SysDataTablePager<Douments> GetALLDocuments(string id,string userid)
		{
			NameValueCollection nvc = HttpUtility.ParseQueryString(base.Request.RequestUri.Query);
			string sEcho = nvc["sEcho"].ToString();
			string sSearch = nvc["sSearch"].ToString();
			int iDisplayStart = Convert.ToInt32(nvc["iDisplayStart"]);
			int iDisplayLength = Convert.ToInt32(nvc["iDisplayLength"]);
			int iSortCol = Convert.ToInt32(nvc["iSortCol_0"]);
			string sortOrder = nvc["sSortDir_0"].ToString();
			NameValue.Add("@PateintID", id);
			NameValue.Add("@serach", sSearch);
			NameValue.Add("@USerID", userid);



			OperationLayer = new DataOperationLayer(ConnectionString);
			DataTable dataSet = OperationLayer.List("sp_getalldocumentsServer", NameValue);
			List<Douments> patientModels = new List<Douments>();
			List<Douments> Customers = new List<Douments>();
			int Count = dataSet.Rows.Count;
			int i = 0;
			foreach (DataRow dr in dataSet.Rows)
			{
				i++;
				patientModels.Add(new Douments
				{
					ID = dr["pkID"].ToString(),
					PID = dr["PatientID"].ToString(),
					Name = dr["FileName"].ToString(),
					file = dr["file_path"].ToString()
				});
				Customers = SortFunction(iSortCol, sortOrder, patientModels).Skip(iDisplayStart).Take(iDisplayLength).ToList();
			}
			return new SysDataTablePager<Douments>(Customers, Count, Count, sEcho);
		}

		private List<Douments> SortFunction(int iSortCol, string sortOrder, List<Douments> list)
		{
			if (iSortCol == 1 || iSortCol == 4)
			{
				Func<Douments, string> orderingFunction = (Douments c) => (iSortCol != 1) ? c.Name : c.Name;
				list = ((!(sortOrder == "desc")) ? list.OrderBy(orderingFunction).ToList() : list.OrderByDescending(orderingFunction).ToList());
			}
			return list;
		}





		[HttpGet]
		[Route("api/getAppointmentbyPateintIDNew")]
		public SysDataTablePager<CompetedApointments> getAppointmentbyPateintID( int PateintID)
		{
			NameValueCollection nvc = HttpUtility.ParseQueryString(base.Request.RequestUri.Query);
			string sEcho = nvc["sEcho"].ToString();
			string sSearch = nvc["sSearch"].ToString();
			int iDisplayStart = Convert.ToInt32(nvc["iDisplayStart"]);
			int iDisplayLength = Convert.ToInt32(nvc["iDisplayLength"]);
			int iSortCol = Convert.ToInt32(nvc["iSortCol_0"]);
			string sortOrder = nvc["sSortDir_0"].ToString();
			NameValue.Add("@PateintID ",PateintID.ToString());
			NameValue.Add("@search", sSearch);
			OperationLayer = new DataOperationLayer(ConnectionString);
			DataTable dataSet = OperationLayer.List("sp_getAppointmentbyPateintID", NameValue);
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
		[Route("api/PatientRegistration/getSpeechTherapyByPatientId")]
		public Response getSpeechTherapyByPatientId(string id)
		{
			NameValue.Add("@PatientId", id.ToString());

			OperationLayer = new DataOperationLayer(ConnectionString);
			var data = OperationLayer.Statusget("sp_getSpeechTherapyByPatientId", NameValue);
			List<City> cities = new List<City>();

            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
			{
				cities.Add(new City()
				{
					ID=data.Tables[0].Rows[i][0].ToString(),
					Name= data.Tables[0].Rows[i][1].ToString(),



				});



			}

			PatientRegistration patient = new PatientRegistration()
			{
				PatientID= data.Tables[1].Rows[0][0].ToString(),
				Patient_Name= data.Tables[1].Rows[0][1].ToString(),
				Patient_Phone_Number= data.Tables[1].Rows[0][3].ToString(),
				Patient_Mobile_Number= data.Tables[1].Rows[0][4].ToString(),
				CityID= data.Tables[1].Rows[0][9].ToString(),
				DateOfBirth=data.Tables[1].Rows[0][13].ToString()

			};
			SpeechTherapy st;


			if (data.Tables[2].Rows.Count>0)
			{
				st = new SpeechTherapy()
				{
					StId = Convert.ToInt32(data.Tables[2].Rows[0][0]),
					ParentGuardian = data.Tables[2].Rows[0][2].ToString(),
					BillingAddress = data.Tables[2].Rows[0][3].ToString(),
					State = data.Tables[2].Rows[0][4].ToString(),
					Zip = data.Tables[2].Rows[0][5].ToString(),

					ChildLiveWithBothParents = data.Tables[2].Rows[0][6].ToString(),
					IfNowithWhomChildLive = data.Tables[2].Rows[0][7].ToString(),
					PrimaryLanguage = data.Tables[2].Rows[0][8].ToString(),
					SecondaryLanguage = data.Tables[2].Rows[0][9].ToString(),
					Pediatrician = data.Tables[2].Rows[0][10].ToString(),
					PediatricianPhone = data.Tables[2].Rows[0][11].ToString(),
					ReferringPhysician = data.Tables[2].Rows[0][12].ToString(),
					HowDoYouHereAboutUs = data.Tables[2].Rows[0][13].ToString(),
					PreviousSpeechTherapyEvaluation = data.Tables[2].Rows[0][14].ToString(),
					OtherTherapiesToDate = data.Tables[2].Rows[0][15].ToString(),
					Presentproblem = data.Tables[2].Rows[0][16].ToString(),
					NotedPresentProblem = data.Tables[2].Rows[0][17].ToString(),
					ChildReaction = data.Tables[2].Rows[0][18].ToString(),
					FamilyReaction = data.Tables[2].Rows[0][19].ToString(),
					significantChangesInLastSixMonths = data.Tables[2].Rows[0][20].ToString(),
					significantIfSoWhat = data.Tables[2].Rows[0][21].ToString(),
					ChildInfection = data.Tables[2].Rows[0][22].ToString(),
					EustachianTubes = data.Tables[2].Rows[0][23].ToString(),
					Chronic = data.Tables[2].Rows[0][24].ToString(),
					Allergies = data.Tables[2].Rows[0][25].ToString(),
					AllergiesDescribe = data.Tables[2].Rows[0][26].ToString(),
					SeriousOrRecurrentIllnesses = data.Tables[2].Rows[0][27].ToString(),
					AnyOperation = data.Tables[2].Rows[0][28].ToString(),
					OperationDescribe = data.Tables[2].Rows[0][30].ToString(),
					TraumaToHead = data.Tables[2].Rows[0][31].ToString(),
					AnyMedications = data.Tables[2].Rows[0][32].ToString(),
					VisionProblems = data.Tables[2].Rows[0][33].ToString(),
					VisionProblemsDescribe = data.Tables[2].Rows[0][34].ToString(),
					HearingDifficulties = data.Tables[2].Rows[0][35].ToString(),
					HearingDifficultiesDescribe = data.Tables[2].Rows[0][36].ToString(),
					DentalProblem = data.Tables[2].Rows[0][37].ToString(),
					DentalProblemDescribe = data.Tables[2].Rows[0][38].ToString(),
					OtherMedicalHistory = data.Tables[2].Rows[0][39].ToString(),
					AgeWhenChildSatUpAlone = data.Tables[2].Rows[0][40].ToString(),
					Crawled = data.Tables[2].Rows[0][41].ToString(),
					Walked = data.Tables[2].Rows[0][42].ToString(),
					ToitelTrained = data.Tables[2].Rows[0][43].ToString(),
					DressedIndependently = data.Tables[2].Rows[0][44].ToString(),
					TiedShoes = data.Tables[2].Rows[0][45].ToString(),
					LeftOrRightHanded = data.Tables[2].Rows[0][46].ToString(),
					SelfdiRectedActivities = data.Tables[2].Rows[0][47].ToString(),
					AdultDirected = data.Tables[2].Rows[0][48].ToString(),
					BedTime = data.Tables[2].Rows[0][49].ToString(),
					ChildSleepWell = data.Tables[2].Rows[0][50].ToString(),
					RespondTypicallyToLightSoundPeople = data.Tables[2].Rows[0][51].ToString(),
					ChildPlayWithOthers = data.Tables[2].Rows[0][52].ToString(),
					Who = data.Tables[2].Rows[0][53].ToString(),
					CryAppropriately = data.Tables[2].Rows[0][54].ToString(),
					ChildsCurrentSchool = data.Tables[2].Rows[0][55].ToString(),
					Grade = data.Tables[2].Rows[0][56].ToString(),
					ChildPerformanceEducationally = data.Tables[2].Rows[0][57].ToString(),
					ReceivingSpecialServicesAtSchool = data.Tables[2].Rows[0][58].ToString(),
					IfYesPleaseDescribeTheSerivces = data.Tables[2].Rows[0][59].ToString(),
					IFSPOrIEP = data.Tables[2].Rows[0][60].ToString(),
					TeacherDescribePerformance = data.Tables[2].Rows[0][61].ToString(),
					TeacherExpressedAnyConcern = data.Tables[2].Rows[0][62].ToString(),
					TeacherExpressedAnyConcernExplain = data.Tables[2].Rows[0][63].ToString(),
					resultOfThisEvaluation = data.Tables[2].Rows[0][64].ToString(),
					Anythingelse = data.Tables[2].Rows[0][65].ToString(),
					















				};



			}


            else{

				st = new SpeechTherapy()
				{
					StId = 0,
					ParentGuardian = "",
					BillingAddress = "",
					State = "",
					Zip = "",

					ChildLiveWithBothParents = "",
					IfNowithWhomChildLive = "",
					PrimaryLanguage = "",
					SecondaryLanguage = "",
					Pediatrician = "",
					PediatricianPhone = "",
					ReferringPhysician = "",
					HowDoYouHereAboutUs = "",
					PreviousSpeechTherapyEvaluation = "",
					OtherTherapiesToDate = "",
					Presentproblem = "",
					NotedPresentProblem = "",
					ChildReaction = "",
					FamilyReaction = "",
					significantChangesInLastSixMonths = "",
					significantIfSoWhat = "",
					ChildInfection = "",
					EustachianTubes = "",
					Chronic = "",
					Allergies = "",
					AllergiesDescribe = "",
					SeriousOrRecurrentIllnesses = "",
					AnyOperation = "",
					OperationDescribe = "",
					TraumaToHead = "",
					AnyMedications = "",
					VisionProblems = "",
					VisionProblemsDescribe = "",
					HearingDifficulties = "",
					HearingDifficultiesDescribe = "",
					DentalProblem = "",
					DentalProblemDescribe = "",
					OtherMedicalHistory = "",
					AgeWhenChildSatUpAlone = "",
					Crawled = "",
					Walked = "",
					ToitelTrained = "",
					DressedIndependently = "",
					TiedShoes = "",
					LeftOrRightHanded = "",
					SelfdiRectedActivities = "",
					AdultDirected = "",
					BedTime = "",
					ChildSleepWell = "",
					RespondTypicallyToLightSoundPeople = "",
					ChildPlayWithOthers = "",
					Who = "",
					CryAppropriately = "",
					ChildsCurrentSchool = "",
					Grade = "",
					ChildPerformanceEducationally = "",
					ReceivingSpecialServicesAtSchool = "",
					IfYesPleaseDescribeTheSerivces = "",
					IFSPOrIEP = "",
					TeacherDescribePerformance = "",
					TeacherExpressedAnyConcern = "",
					TeacherExpressedAnyConcernExplain = "",
					CreatedDate = "",
					Anythingelse = "",
					resultOfThisEvaluation = ""















				};
			}




			res.status = "success";
                res.Object = new
				{

					cities = cities,
					patient = patient,
					speechtherapy = st


				};
           
            return res;
		}





		[HttpPost]
		[Route("api/PatientRegistration/AddUpdateSpeechTherapyByPatientId")]
		public Response AddUpdateSpeechTherapyByPatientId([FromBody] SpeechTherapy st)
		{


			NameValue.Add("@PatientId",st.PatientId.ToString());
	NameValue.Add("@PatientName",st.PatientName);
			NameValue.Add("@CityId",st.City);
			NameValue.Add("@Patient_Phone_Number",st.HomePhone);
			NameValue.Add("@PatientMobileNumber",st.CellPhone);
			NameValue.Add("@DateOfBirth",st.Dateofbirth);
			NameValue.Add("@ParentGuardian",st.ParentGuardian);
			NameValue.Add("@BillingAddress",st.BillingAddress);
			NameValue.Add("@PState",st.State);
			NameValue.Add("@Zip",st.Zip);
			NameValue.Add("@ChildLiveWithBothParents",st.ChildLiveWithBothParents);
			NameValue.Add("@IfNowithWhomChildLive",st.IfNowithWhomChildLive);
			NameValue.Add("@PrimaryLanguage",st.PrimaryLanguage);
			NameValue.Add("@SecondaryLanguage", st.SecondaryLanguage);
			NameValue.Add("@Pediatrician",st.Pediatrician);
			NameValue.Add("@PediatricianPhone",st.PediatricianPhone);
			NameValue.Add("@ReferringPhysician",st.ReferringPhysician);
			NameValue.Add("@HowDoYouHereAboutUs",st.HowDoYouHereAboutUs);
			NameValue.Add("@PreviousSpeechTherapyEvaluation", WebUtility.HtmlEncode(st.PreviousSpeechTherapyEvaluation));
			NameValue.Add("@OtherTherapiesToDate",st.OtherTherapiesToDate);
			NameValue.Add("@Presentproblem",st.Presentproblem);
			NameValue.Add("@NotedPresentProblem",st.NotedPresentProblem);
			NameValue.Add("@ChildReaction",st.ChildReaction);
			NameValue.Add("@FamilyReaction",st.FamilyReaction);
			NameValue.Add("@significantChangesInLastSixMonths",st.significantChangesInLastSixMonths);
			NameValue.Add("@significantIfSoWhat",st.significantIfSoWhat);
			NameValue.Add("@ChildInfection",st.ChildInfection);
			NameValue.Add("@EustachianTubes",st.EustachianTubes);
			NameValue.Add("@Chronic",st.Chronic);
			NameValue.Add("@Allergies",st.Allergies);
			NameValue.Add("@AllergiesDescribe",st.AllergiesDescribe);
			NameValue.Add("@SeriousOrRecurrentIllnesses",st.SeriousOrRecurrentIllnesses);
			NameValue.Add("@AnyOperation",st.AnyOperation);
			NameValue.Add("@OperationDescribe",st.OperationDescribe);
			NameValue.Add("@TraumaToHead",st.TraumaToHead);
			NameValue.Add("@AnyMedications",st.AnyMedications);
			NameValue.Add("@VisionProblems",st.VisionProblems);
			NameValue.Add("@VisionProblemsDescribe",st.VisionProblemsDescribe);
			NameValue.Add("@HearingDifficulties",st.HearingDifficulties);
			NameValue.Add("@HearingDifficultiesDescribe",st.HearingDifficultiesDescribe);
			NameValue.Add("@DentalProblem",st.DentalProblem);
			NameValue.Add("@DentalProblemDescribe",st.DentalProblemDescribe);
			NameValue.Add("@OtherMedicalHistory",st.OtherMedicalHistory);
			NameValue.Add("@AgeWhenChildSatUpAlone",st.AgeWhenChildSatUpAlone);
			NameValue.Add("@Crawled",st.Crawled);
			NameValue.Add("@Walked",st.Walked);
			NameValue.Add("@ToitelTrained",st.ToitelTrained);
			NameValue.Add("@DressedIndependently",st.DressedIndependently);
			NameValue.Add("@TiedShoes",st.TiedShoes);
			NameValue.Add("@LeftOrRightHanded",st.LeftOrRightHanded);
			NameValue.Add("@SelfdiRectedActivities",st.SelfdiRectedActivities);
			NameValue.Add("@AdultDirected",st.AdultDirected);
			NameValue.Add("@BedTime",st.BedTime);
			NameValue.Add("@ChildSleepWell",st.ChildSleepWell);
			NameValue.Add("@RespondTypicallyToLightSoundPeople",st.RespondTypicallyToLightSoundPeople);
			NameValue.Add("@ChildPlayWithOthers",st.ChildPlayWithOthers);
			NameValue.Add("@Who",st.Who);
			NameValue.Add("@CryAppropriately",st.CryAppropriately);
			NameValue.Add("@ChildsCurrentSchool",st.ChildsCurrentSchool);
			NameValue.Add("@Grade",st.Grade);
			NameValue.Add("@ChildPerformanceEducationally",st.ChildPerformanceEducationally);
			NameValue.Add("@ReceivingSpecialServicesAtSchool",st.ReceivingSpecialServicesAtSchool);
			NameValue.Add("@IfYesPleaseDescribeTheSerivces",st.IfYesPleaseDescribeTheSerivces);
			NameValue.Add("@IFSPOrIEP",st.IFSPOrIEP);
			NameValue.Add("@TeacherDescribePerformance",st.TeacherDescribePerformance);
			NameValue.Add("@TeacherExpressedAnyConcern",st.TeacherExpressedAnyConcern);
			NameValue.Add("@TeacherExpressedAnyConcernExplain",st.TeacherExpressedAnyConcernExplain);
			NameValue.Add("@resultOfThisEvaluation",st.resultOfThisEvaluation);
			NameValue.Add("@Anythingelse",st.Anythingelse);

			OperationLayer = new DataOperationLayer(ConnectionString);


			var data = OperationLayer.callStoredProcedure("sp_AddUpdateSpeechTherapy", NameValue);
		





			res.status = "success";
			

			return res;
		}







	}
}
