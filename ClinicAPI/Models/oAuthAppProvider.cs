using DataConnection;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace ClinicAPI.Models
{
    public class oAuthAppProvider : OAuthAuthorizationServerProvider
	{
		private NameValueCollection NameValue = new NameValueCollection();

		private DataOperationLayer OperationLayer;

		private string ConnectionString = ConfigurationManager.AppSettings["Connectionstring"];

		public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
		{
			context.Validated();
		}

		public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
		{
			ClaimsIdentity identity = new ClaimsIdentity(context.Options.AuthenticationType);
			string p = context.Password;
			return Task.Factory.StartNew(delegate
			{
				NameValue.Clear();
				NameValue.Add("@Email", context.UserName);
				NameValue.Add("@Password", context.Password);
				OperationLayer = new DataOperationLayer(ConnectionString);
				DataSet dataSet = OperationLayer.AutheticateUser("sp_Login", NameValue);
				string text = "";
				if (dataSet.Tables.Count > 0)
				{
					for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
					{
						text = text + dataSet.Tables[0].Rows[i][3].ToString() + ",";
						identity.AddClaim(new Claim("Role", dataSet.Tables[0].Rows[i][3].ToString()));
					}
					text = text.Substring(0, text.Length - 1);
					string value = dataSet.Tables[0].Rows[0][0].ToString();
					identity.AddClaim(new Claim("Email", dataSet.Tables[0].Rows[0][1].ToString()));
					identity.AddClaim(new Claim("Name", dataSet.Tables[0].Rows[0][2].ToString()));
					AuthenticationProperties properties = new AuthenticationProperties(new Dictionary<string, string>
				{
					{ "UserId", value },
					{ "Role", text },
					{
						"Email",
						dataSet.Tables[0].Rows[0][1].ToString()
					},
					{
						"Name",
						dataSet.Tables[0].Rows[0][2].ToString()
					}
				});
					context.Validated(new AuthenticationTicket(identity, properties));
				}
				else
				{
					context.SetError("invalid_grant", "invalid username and password");
				}
			});
		}

		public override Task TokenEndpoint(OAuthTokenEndpointContext context)
		{
			foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
			{
				context.AdditionalResponseParameters.Add(property.Key, property.Value);
			}
			return Task.FromResult<object>(null);
		}
	}
}