using ClinicAPI.Models;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartupAttribute(typeof(ClinicAPI.App_Start.Startup))]
namespace ClinicAPI.App_Start
{
    public class Startup
    {
		public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

		public void Configuration(IAppBuilder app)
		{
			oAuthAppProvider myprovider = new oAuthAppProvider();
			OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
			{
				TokenEndpointPath = new PathString("/token"),
				Provider = myprovider,
				AllowInsecureHttp = true
			};
			app.UseOAuthAuthorizationServer(options);
			app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
			HttpConfiguration config = new HttpConfiguration();
			WebApiConfig.Register(config);
		}
	}
}