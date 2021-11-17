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
    public class DoctorScheduleController : ApiController
    {


        private DataOperationLayer OperationLayer;

     

        private string ConnectionString = ConfigurationManager.AppSettings["Connectionstring"];


		[Route("api/DoctorSchedule/AddDoctorSchedule")]
		[HttpPost]
		public Response Createcities([FromBody] List<DoctorSchedule> obj)
		{
			NameValueCollection NameValue = new NameValueCollection();
			Response response = new Response();
			OperationLayer = new DataOperationLayer(ConnectionString);

			NameValue.Clear();
			string json = "";


			for (int i = 0; i < obj.Count; i++)
            {
				NameValue.Clear();

				NameValue.Add("@date", obj[i].dates);
                NameValue.Add("@starttime", obj[i].starttime);


				NameValue.Add("@endtime", obj[i].endtime);
				NameValue.Add("@doctorId", obj[i].DoctorId.ToString());
				 json = OperationLayer.callStoredProcedure("sp_AddDoctorSchedule", NameValue);


			}

            if (json == "no")
            {
                response.status = "fail";
                response.message = "not inserted";
                return response;
            }
            response.status = "success";
			response.message = "Created";
			response.Object = "";
			return response;
		}




		[Route("api/DoctorSchedule/GetAll")]
		public SysDataTablePager<DoctorSchedule> GetserverCity(int dotorId)
		{
			NameValueCollection nvc = HttpUtility.ParseQueryString(base.Request.RequestUri.Query);
			NameValueCollection NameValue = new NameValueCollection();
			string sEcho = nvc["sEcho"].ToString();
			string sSearch = nvc["sSearch"].ToString();
			int iDisplayStart = Convert.ToInt32(nvc["iDisplayStart"]);
			int iDisplayLength = Convert.ToInt32(nvc["iDisplayLength"]);
			int iSortCol = Convert.ToInt32(nvc["iSortCol_0"]);
			string sortOrder = nvc["sSortDir_0"].ToString();
			NameValue.Add("@search", sSearch);
			NameValue.Add("@doctorId", dotorId.ToString());
			OperationLayer = new DataOperationLayer(ConnectionString);
			DataTable dataSet = OperationLayer.List("sp_GetAllDoctorSchedule", NameValue);
			List<DoctorSchedule> citymodel = new List<DoctorSchedule>();
			List<DoctorSchedule> Customers = new List<DoctorSchedule>();
			int Count = dataSet.Rows.Count;
			int i = 0;
			foreach (DataRow dr in dataSet.Rows)
			{
				i++;
				citymodel.Add(new DoctorSchedule
				{
					dates = dr["Days"].ToString(),
					starttime = dr["StartTime"].ToString(),
					endtime = dr["EndTime"].ToString(),
					DoctorId = Convert.ToInt32(dr["DoctorId"]),

				});
				Customers = SortFunction(iSortCol, sortOrder, citymodel).Skip(iDisplayStart).Take(iDisplayLength).ToList();
			}
			return new SysDataTablePager<DoctorSchedule>(Customers, Count, Count, sEcho);
		}

		private List<DoctorSchedule> SortFunction(int iSortCol, string sortOrder, List<DoctorSchedule> list)
		{
			if (iSortCol == 1 || iSortCol == 4)
			{
				Func<DoctorSchedule, string> orderingFunction = (DoctorSchedule c) => (iSortCol != 1) ? ((iSortCol != 4) ? c.dates : c.dates) : c.dates;
				list = ((!(sortOrder == "desc")) ? list.OrderBy(orderingFunction).ToList() : list.OrderByDescending(orderingFunction).ToList());
			}
			return list;
		}




		[Route("api/DoctorSchedule/GetSlotByDoctorId")]
		[HttpGet]
		public Response GetSlotByDoctorId(int doctorId)
		{
			NameValueCollection NameValue = new NameValueCollection();
			Response response = new Response();
			OperationLayer = new DataOperationLayer(ConnectionString);

			NameValue.Clear();
			string json = "";


				NameValue.Add("@doctorId", doctorId.ToString());
				json = OperationLayer.callStoredProcedure("GetSlotByDoctorId", NameValue);



			if (json == "no")
			{
				response.status = "fail";
				response.message = "not inserted";
				return response;
			}
			response.status = "success";
			
			response.Object = json;
			return response;
		}











	}
}
