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
    public class CityController : ApiController
    {
		private NameValueCollection NameValue = new NameValueCollection();

		private DataOperationLayer OperationLayer;

		private Response response = new Response();

		private string ConnectionString = ConfigurationManager.AppSettings["Connectionstring"];

		[Route("api/getallcities")]
		[HttpGet]
		public Response GetALLCities()
		{
			OperationLayer = new DataOperationLayer(ConnectionString);
			NameValue.Add("@Id", "");
			string json = OperationLayer.callStoredProcedure("sp_Get_Cities", NameValue);
			if (json == "no")
			{
				response.status = "fail";
				response.message = "no cities in table";
				return response;
			}
			response.status = "success";
			response.message = "found cities";
			response.Object = json;
			return response;
		}

		[Route("api/GetCitiesbyid")]
		[HttpGet]
		public Response GetCitiesbyid(string id)
		{
			OperationLayer = new DataOperationLayer(ConnectionString);
			NameValue.Add("@Id", id);
			string json = OperationLayer.callStoredProcedure("sp_Get_Cities", NameValue);
			if (json == "no")
			{
				response.status = "fail";
				response.message = "no cities in table";
				return response;
			}
			response.status = "success";
			response.message = "found cities";
			response.Object = json;
			return response;
		}

		[Route("api/Createcities")]
		[HttpPost]
		public Response Createcities([FromBody] City city)
		{
			OperationLayer = new DataOperationLayer(ConnectionString);
			NameValue.Add("@id", city.ID);
			NameValue.Add("@city_Name", city.Name);
			string json = OperationLayer.callStoredProcedure("sp_insert_update_cities", NameValue);
			if (json == "no")
			{
				response.status = "fail";
				response.message = "not inserted";
				return response;
			}
			response.status = "success";
			response.message = "found cities";
			response.Object = json;
			return response;
		}

		[Route("api/DeleteCity")]
		[HttpGet]
		public Response DeleteCity(string id)
		{
			OperationLayer = new DataOperationLayer(ConnectionString);
			NameValue.Add("@id", id);
			string json = OperationLayer.callStoredProcedure("sp_delete_city", NameValue);
			if (json == "no")
			{
				response.status = "fail";
				response.message = "not inserted";
				return response;
			}
			response.status = "success";
			response.message = "found cities";
			response.Object = json;
			return response;
		}

		[Route("api/GetserverCity")]
		public SysDataTablePager<City> GetserverCity()
		{
			NameValueCollection nvc = HttpUtility.ParseQueryString(base.Request.RequestUri.Query);
			string sEcho = nvc["sEcho"].ToString();
			string sSearch = nvc["sSearch"].ToString();
			int iDisplayStart = Convert.ToInt32(nvc["iDisplayStart"]);
			int iDisplayLength = Convert.ToInt32(nvc["iDisplayLength"]);
			int iSortCol = Convert.ToInt32(nvc["iSortCol_0"]);
			string sortOrder = nvc["sSortDir_0"].ToString();
			NameValue.Add("@serach", sSearch);
			OperationLayer = new DataOperationLayer(ConnectionString);
			DataTable dataSet = OperationLayer.List("cityserver", NameValue);
			List<City> citymodel = new List<City>();
			List<City> Customers = new List<City>();
			int Count = dataSet.Rows.Count;
			int i = 0;
			foreach (DataRow dr in dataSet.Rows)
			{
				i++;
				citymodel.Add(new City
				{
					ID = dr["CityID"].ToString(),
					Name = dr["City_Name"].ToString()
				});
				Customers = SortFunction(iSortCol, sortOrder, citymodel).Skip(iDisplayStart).Take(iDisplayLength).ToList();
			}
			return new SysDataTablePager<City>(Customers, Count, Count, sEcho);
		}

		private List<City> SortFunction(int iSortCol, string sortOrder, List<City> list)
		{
			if (iSortCol == 1 || iSortCol == 4)
			{
				Func<City, string> orderingFunction = (City c) => (iSortCol != 1) ? c.Name : c.Name;
				list = ((!(sortOrder == "desc")) ? list.OrderBy(orderingFunction).ToList() : list.OrderByDescending(orderingFunction).ToList());
			}
			return list;
		}

	}
}
