using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ClinicWeb.HelperCode
{
    public class ToeknHelper
    {

		public static Dictionary<string, string> getTokenDetails(string email, string password)
		{
			Dictionary<string, string> tokenDetails = null;
			try
			{
				 HttpClient client = new HttpClient();
				Dictionary<string, string> login = new Dictionary<string, string>
			{
				{ "username", email },
				{ "password", password },
				{ "grant_type", "password" }
			};


				//Task<HttpResponseMessage> res = client.PostAsync(System.Configuration.ConfigurationManager.AppSettings["url"].ToString()+"token", new FormUrlEncodedContent(login));

				//Task<HttpResponseMessage> res = client.PostAsync(System.Configuration.ConfigurationManager.AppSettings["url"].ToString()+"token", new FormUrlEncodedContent(login));

				//Task<HttpResponseMessage> res = client.PostAsync(System.Configuration.ConfigurationManager.AppSettings["url"].ToString()+"token", new FormUrlEncodedContent(login));

				Task<HttpResponseMessage> res = client.PostAsync(System.Configuration.ConfigurationManager.AppSettings["url"].ToString()+"token", new FormUrlEncodedContent(login));

                string result = res.Result.Content.ReadAsStringAsync().Result;
				if (res.IsCompleted)
				{
					if (result.Contains("access_token"))
					{
						tokenDetails = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
						return tokenDetails;
					}
					return tokenDetails;
				}
				return tokenDetails;
			}
			catch (Exception)
			{
				return tokenDetails;
			}
		}
	}
}