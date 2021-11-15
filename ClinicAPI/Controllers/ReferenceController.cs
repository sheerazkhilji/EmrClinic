using ClinicAPI.HelperCode;
using ClinicAPI.Models;
using ClinicAPI.Models.Request;
using DataConnection;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ClinicAPI.Controllers
{
    public class ReferenceController : ApiController
	{
		private NameValueCollection NameValue = new NameValueCollection();

		private DataOperationLayer OperationLayer;

		private Response res = new Response();

		private EmailSender Email = new EmailSender();

		private string ConnectionString = ConfigurationManager.AppSettings["Connectionstring"];


		[Route("api/Reference/Addreferral")]
		[HttpPost]
		public Response Addreferral(Referral model)
        {


			NameValue.Clear();

			NameValue.Add("@MobileNumber", model.MobileNumber);
			NameValue.Add("@Email", model.Email);
			NameValue.Add("@RefereedBy", model.RefereedBy);
			NameValue.Add("@DateOfBirth", model.DateOfBirth.ToString());
			NameValue.Add("@CNIC", model.CNIC);
			NameValue.Add("@pateint_name", model.pateint_name);

			NameValue.Add("@CreateDate", model.CreateDate);
			NameValue.Add("@appointmentDate", model.appointmentDate);
			NameValue.Add("@doctorId", model.DoctorId.ToString());
			NameValue.Add("@comments", model.Comments);
			NameValue.Add("@Save", model.Save.ToString());

			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_Insert_Reference", NameValue);
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

		[Route("api/Reference/GetDatatable")]
		[HttpGet]
		public SysDataTablePager<Referral> GetDatatable()
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
			DataTable dataSet = OperationLayer.List("sp_GetAllReferral", NameValue);
			List<Referral> patientModels = new List<Referral>();
			List<Referral> Customers = new List<Referral>();
			int Count = dataSet.Rows.Count;
			int i = 0;
			foreach (DataRow dr in dataSet.Rows)
			{
				i++;
				patientModels.Add(new Referral
				{
				
					Rid = Convert.ToInt32(dr["Rid"]),
					MobileNumber = dr["MobileNumber"].ToString(),
					Email = dr["Email"].ToString(),
					CNIC = dr["CNIC"].ToString(),
					RefereedBy=dr["RefereedBy"].ToString(),
					DateOfBirth=dr["Age"].ToString(),
					pateint_name=dr["Patient_Name"].ToString(),
					CreateDate=dr["CreateDated"].ToString() == null?"": dr["CreateDated"].ToString(),
					appointmentDate = dr["appointmentDated"].ToString()==null?"":dr["appointmentDated"].ToString(),
					DoctorName=dr["UserName"].ToString()

					//MR_No = dr["MR_No"].ToString() == null ? "" : dr["MR_No"].ToString(),
				});
				Customers = SortFunction(iSortCol, sortOrder, patientModels).Skip(iDisplayStart).Take(iDisplayLength).ToList();
			}
			return new SysDataTablePager<Referral>(Customers, Count, Count, sEcho);
		}

		private List<Referral> SortFunction(int iSortCol, string sortOrder, List<Referral> list)
		{
			if (iSortCol == 1 || iSortCol == 4)
			{
				Func<Referral, string> orderingFunction = (Referral c) => (iSortCol != 1) ? ((iSortCol != 4) ? c.pateint_name : c.Email) : c.pateint_name;
				list = ((!(sortOrder == "desc")) ? list.OrderBy(orderingFunction).ToList() : list.OrderByDescending(orderingFunction).ToList());
			}
			return list;
		}




		[Route("api/Reference/GetByid")]
		[HttpGet]
		public Response GetByid(int id)
		{


			NameValue.Clear();

			NameValue.Add("@rid", id.ToString());
		

			OperationLayer = new DataOperationLayer(ConnectionString);
		var data= OperationLayer.Statusget("sp_GetAllReferralbyid", NameValue);
			List<Doctor> doctors = new List<Doctor>();

			Referral reff = new Referral();


            for (int i = 0; i <data.Tables[0].Rows.Count ; i++)
            {
				doctors.Add(new Doctor
				{

					DoctorID= Convert.ToInt32(data.Tables[0].Rows[i][0]),
					DoctorName= data.Tables[0].Rows[i][1].ToString()

				});
            }



			for (int i = 0; i < data.Tables[1].Rows.Count; i++)
			{
				reff.Rid = Convert.ToInt32(data.Tables[1].Rows[i][0]);

				reff.MobileNumber = data.Tables[1].Rows[i][1].ToString();

				reff.Email = data.Tables[1].Rows[i][2].ToString();

				reff.RefereedBy = data.Tables[1].Rows[i][3].ToString();

				reff.DateOfBirth = data.Tables[1].Rows[i][13].ToString();

				reff.CNIC =  data.Tables[1].Rows[i][5].ToString();

				reff.pateint_name = data.Tables[1].Rows[i][6].ToString();

				reff.DoctorId = Convert.ToInt32(data.Tables[1].Rows[i][9]);

				reff.Comments = data.Tables[1].Rows[i][10].ToString();

				reff.appointmentDate = data.Tables[1].Rows[i][11].ToString();

				reff.CreateDate = data.Tables[1].Rows[i][12].ToString();

				



			}




			
				res.status = "success";
				res.Object = new {
				Doctors=doctors,
				Reff=reff
				
				};
			



			return res;



		}





		[Route("api/Reference/updateReference")]
		[HttpPost]
		public Response update_Reference(Referral model)
		{


			NameValue.Clear();

			NameValue.Add("@idd", model.Rid.ToString());
			NameValue.Add("@MobileNumber", model.MobileNumber);
			NameValue.Add("@Email", model.Email);
			NameValue.Add("@RefereedBy", model.RefereedBy);
			NameValue.Add("@DateOfBirth", model.DateOfBirth.ToString());
			NameValue.Add("@CNIC", model.CNIC);
			NameValue.Add("@pateint_name", model.pateint_name);

			NameValue.Add("@CreateDate", model.CreateDate);
			NameValue.Add("@appointmentDate", model.appointmentDate);


			NameValue.Add("@doctorId", model.DoctorId.ToString());
			NameValue.Add("@comments", model.Comments);

			NameValue.Add("@Save", model.Save.ToString());

			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_update_Reference", NameValue);
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



		[Route("api/Reference/Delete")]
		[HttpGet]
		public Response Delete(int id)
		{


			NameValue.Clear();

			NameValue.Add("@id", id.ToString());
			
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_deleteRef", NameValue);
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



		[Route("api/Reference/Registor")]
		[HttpGet]
		public Response Registor(int id)
		{


			NameValue.Clear();

			NameValue.Add("@id", id.ToString());


			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_Registor_pateintByReferral", NameValue);
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

	}
}
