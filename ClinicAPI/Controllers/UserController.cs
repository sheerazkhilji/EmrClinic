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
   
    public class UserController : ApiController
    {
		private NameValueCollection NameValue = new NameValueCollection();

		private DataOperationLayer OperationLayer;

		private Response res = new Response();

		private EmailSender Email = new EmailSender();

		private string ConnectionString = ConfigurationManager.AppSettings["Connectionstring"];

		[Route("api/Registration")]
		[HttpPost]
		public Response Registration([FromBody] RegistrationModel model)
		{
			if (model.UserName == "" || model.UserName == null)
			{
				res.status = "Fail";
				res.message = "User name is Required";
				return res;
			}
			if (model.Email == "" || model.Email == null)
			{
				res.status = "Fail";
				res.message = " Email is Required";
				return res;
			}
			if (model.Password == "" || model.Password == null)
			{
				res.status = "Fail";
				res.message = " Password is Required";
				return res;
			}
			if (model.Password != model.ConfirmPassword)
			{
				res.status = "Fail";
				res.message = " password Does not match";
				return res;
			}
			if (model.Cid == null || model.Cid == "")
			{
				res.status = "Fail";
				res.message = " City is Required";
				return res;
			}
			NameValue.Add("@Id", model.Rid.ToString());
			NameValue.Add("@Username", model.UserName);
			NameValue.Add("@Email", model.Email);
			NameValue.Add("@Password", model.Password);
			NameValue.Add("@PhoneNo", model.PhoneNumber);
			NameValue.Add("@CityID", model.Cid);
			NameValue.Add("@Speciality", model.Speciality);
			NameValue.Add("@RoleId", DBNull.Value.ToString());
			NameValue.Add("@isActive", DBNull.Value.ToString());
			NameValue.Add("@Action", "insert");
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_User_Insert_Update", NameValue);
			if (json == "exits")
			{
				res.status = "exits";
			}
			else
			{
				if (model.rolesids.Length != 0)
				{
					for (int i = 0; i < model.rolesids.Length; i++)
					{
						NameValue.Clear();
						NameValue.Add("@userid", json);
						NameValue.Add("@roleid", model.rolesids[i]);
						NameValue.Add("@active", "1");
						NameValue.Add("@action", "insert");
						OperationLayer = new DataOperationLayer(ConnectionString);
						string jsonr = OperationLayer.callStoredProcedure("sp_assign_roles", NameValue);
					}
				}
				if (json == null || json == "")
				{
					res.status = "fail";
				}
				else
				{
					res.status = "success";
					res.Object = json;
				}
			}
			return res;
		}

		[Route("api/getUserbyID")]
		[HttpGet]
		public Response getUserbyID(string id)
		{
			NameValue.Add("@id", id);
			NameValue.Add("@search", "");
			NameValue.Add("@action", "jsonbyID");
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_get_all_User", NameValue);
			if (json == "no")
			{
				res.status = "not found";
			}
			else
			{
				res.status = "success";
				res.Object = json;
			}
			return res;
		}

		[Route("api/UserList")]
		public SysDataTablePager<RegistrationModel> GetDatatable()
		{
			NameValueCollection nvc = HttpUtility.ParseQueryString(base.Request.RequestUri.Query);
			string sEcho = nvc["sEcho"].ToString();
			string sSearch = nvc["sSearch"].ToString();
			int iDisplayStart = Convert.ToInt32(nvc["iDisplayStart"]);
			int iDisplayLength = Convert.ToInt32(nvc["iDisplayLength"]);
			int iSortCol = Convert.ToInt32(nvc["iSortCol_0"]);
			string sortOrder = nvc["sSortDir_0"].ToString();
			NameValue.Add("@id", "");
			NameValue.Add("@search", sSearch);
			NameValue.Add("@action", "getall");
			OperationLayer = new DataOperationLayer(ConnectionString);
			DataTable dataSet = OperationLayer.List("sp_get_all_User", NameValue);
			List<RegistrationModel> registrationModels = new List<RegistrationModel>();
			List<RegistrationModel> Customers = new List<RegistrationModel>();
			int Count = dataSet.Rows.Count;
			int i = 0;
			foreach (DataRow dr in dataSet.Rows)
			{
				i++;
				registrationModels.Add(new RegistrationModel
				{
					UserId = dr["UserID"].ToString(),
					UserName = dr["UserName"].ToString(),
					Email = dr["UserEmail"].ToString(),
					PhoneNumber = dr["PhoneNo"].ToString(),
					Cid = dr["CityID"].ToString(),
					Speciality = dr ["Speciality"].ToString(),
					cityname = dr["City_Name"].ToString()
					
				});
				Customers = SortFunction(iSortCol, sortOrder, registrationModels).Skip(iDisplayStart).Take(iDisplayLength).ToList();
			}
			return new SysDataTablePager<RegistrationModel>(Customers, Count, Count, sEcho);
		}

		private List<RegistrationModel> SortFunction(int iSortCol, string sortOrder, List<RegistrationModel> list)
		{
			if (iSortCol == 1 || iSortCol == 4)
			{
				Func<RegistrationModel, string> orderingFunction = (RegistrationModel c) => (iSortCol != 1) ? ((iSortCol != 4) ? c.Email : c.PhoneNumber) : c.UserName;
				list = ((!(sortOrder == "desc")) ? list.OrderBy(orderingFunction).ToList() : list.OrderByDescending(orderingFunction).ToList());
			}
			return list;
		}

		[Route("api/DeleteUser")]
		[HttpGet]
		
		public Response Deleteuser(string id)
		{
			OperationLayer = new DataOperationLayer(ConnectionString);
			NameValue.Add("@id", id);
			NameValue.Add("@search", "");
			NameValue.Add("@action", "delete");
			string json = OperationLayer.callStoredProcedure("sp_get_all_User", NameValue);
			if (json == "no")
			{
				res.status = "fail";
				res.message = "not deleted";
				return res;
			}
			res.status = "success";
			res.message = "deleted";
			res.Object = json;
			return res;
		}

		[Route("api/UpdateUser")]
		[HttpPost]
		public Response UpdateUser([FromBody] RegistrationModel model)
		{
			if (model.UserName == "" || model.UserName == null)
			{
				res.status = "Fail";
				res.message = "User name is Required";
				return res;
			}
			if (model.Email == "" || model.Email == null)
			{
				res.status = "Fail";
				res.message = " Email is Required";
				return res;
			}
			if (model.Cid == null || model.Cid == "")
			{
				res.status = "Fail";
				res.message = " City is Required";
				return res;
			}
			NameValue.Add("@Id", model.UserId);
			NameValue.Add("@Username", model.UserName);
			NameValue.Add("@Email", model.Email);
			NameValue.Add("@Password", "");
			NameValue.Add("@PhoneNo", model.PhoneNumber);
			NameValue.Add("@CityID", model.Cid);
			NameValue.Add("@Speciality", model.Speciality);
			NameValue.Add("@RoleId", DBNull.Value.ToString());
			NameValue.Add("@isActive", DBNull.Value.ToString());
			NameValue.Add("@Action", "Update");
			OperationLayer = new DataOperationLayer(ConnectionString);
			string json = OperationLayer.callStoredProcedure("sp_User_Insert_Update", NameValue);
			if (json == "exits")
			{
				res.status = "exits";
				return res;
			}
			for (int j = 0; j < model.rolesids.Length; j++)
			{
				NameValue.Clear();
				NameValue.Add("@userid", model.UserId);
				NameValue.Add("@roleid", model.rolesids[j]);
				NameValue.Add("@active", "1");
				NameValue.Add("@action", "activerole");
				OperationLayer = new DataOperationLayer(ConnectionString);
				string jsonr = OperationLayer.callStoredProcedure("sp_assign_roles", NameValue);
			}
			for (int i = 0; i < model.roleisactive.Length; i++)
			{
				NameValue.Clear();
				NameValue.Add("@userid", model.UserId);
				NameValue.Add("@roleid", model.roleisactive[i]);
				NameValue.Add("@active", "0");
				NameValue.Add("@action", "activerole");
				OperationLayer = new DataOperationLayer(ConnectionString);
				string jsonr2 = OperationLayer.callStoredProcedure("sp_assign_roles", NameValue);
			}
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
