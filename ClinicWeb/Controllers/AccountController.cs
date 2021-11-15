using ClinicWeb.HelperCode;
using ClinicWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ClinicWeb.Controllers
{
    public class AccountController : Controller
    {
		public ActionResult Registor()
		{
			if (Request.Cookies["Token"] == null || Request.Cookies["Role"] == null || Request.Cookies["Name"] == null)
			{
				return RedirectToAction("Login", "Account");
			}
			if ((Request.Cookies["Token"] != null && Request.Cookies["Role"].Value.ToString().Contains("Admin")) || Request.Cookies["Role"].Value.ToString().Contains("Assistant"))
			{
				return View();
			}
			return RedirectToAction("Login", "Account");
		}

		public ActionResult GetALLUser()
		{
			if (Request.Cookies["Token"] == null || Request.Cookies["Role"] == null || Request.Cookies["Name"] == null)
			{
				return RedirectToAction("Login", "Account");
			}
			if ((Request.Cookies["Token"] != null && Request.Cookies["Role"].Value.ToString().Contains("Admin")) || Request.Cookies["Role"].Value.ToString().Contains("Assistant"))
			{
				return View();
			}
			return RedirectToAction("Login", "Account");
		}

		[HttpGet]
		public ActionResult Login()
		{
			HttpCookie cookie = base.Request.Cookies["Login"];

			if (cookie != null)
			{
				base.ViewBag.username = cookie["username"].ToString();
				string Encryptpassword = cookie["password"].ToString();
				byte[] b = Convert.FromBase64String(Encryptpassword);
				string decryptpassword = Encoding.ASCII.GetString(b);
				base.ViewBag.password = decryptpassword.ToString();
			}
			return View();
		}

		[HttpPost]
		public ActionResult Login(LoginModel model)
		{
			Dictionary<string, string> tokendetails = ToeknHelper.getTokenDetails(model.email, model.password);
			HttpCookie cookie = new HttpCookie("Login");
			HttpCookie Token = new HttpCookie("Token");
			HttpCookie userID = new HttpCookie("userID");
			HttpCookie Role = new HttpCookie("Role");
			HttpCookie Email = new HttpCookie("Email");
			HttpCookie Name = new HttpCookie("Name");

			if (tokendetails != null && tokendetails.ContainsKey("access_token"))
			{
			
				if (model.remember)
				{
					cookie["username"] = model.email;
					byte[] b = Encoding.ASCII.GetBytes(model.password);
					string encryptpassword = (cookie["password"] = Convert.ToBase64String(b));
					cookie.Expires = DateTime.Now.AddDays(2.0);
					base.HttpContext.Response.Cookies.Add(cookie);
				}
				else
				{
					cookie.Expires = DateTime.Now.AddDays(-1.0);
					base.HttpContext.Response.Cookies.Add(cookie);
				}
				Token.Value = tokendetails["access_token"];
				userID["userID"] = tokendetails["UserId"];
				Role["Role"] = tokendetails["Role"];
				Email["Email"] = tokendetails["Email"];
				Name.Value = tokendetails["Name"];

				Token.Expires = DateTime.Now.AddDays(2.0);
				base.HttpContext.Response.Cookies.Add(Token);
				userID.Expires = DateTime.Now.AddDays(2.0);
				base.HttpContext.Response.Cookies.Add(userID);
				Role.Expires = DateTime.Now.AddDays(2.0);
				base.HttpContext.Response.Cookies.Add(Role);
				Email.Expires = DateTime.Now.AddDays(2.0);
				base.HttpContext.Response.Cookies.Add(Email);
				Name.Expires = DateTime.Now.AddDays(2.0);
				base.HttpContext.Response.Cookies.Add(Name);

				//base.Session["Token"] = tokendetails["access_token"];
				//base.Session["userID"] = tokendetails["UserId"];
				//base.Session["Role"] = tokendetails["Role"];
				//base.Session["Email"] = tokendetails["Email"];
				//base.Session["Name"] = tokendetails["Name"];



				//base.Session.Timeout=60;
				return RedirectToAction("Dashboard", "Dashboard");
			}
			base.ViewBag.message = "Invalid Email and  Password";
			//base.Session["Token"] = null;
			//base.Session["Role"] = null;
			//base.Session["Email"] = null;
			//base.Session["Name"] = null;

			Token.Expires = DateTime.Now.AddDays(-1.0);
			base.HttpContext.Response.Cookies.Add(Token);


			userID.Expires = DateTime.Now.AddDays(-1.0);
			base.HttpContext.Response.Cookies.Add(userID);


			Role.Expires = DateTime.Now.AddDays(-1.0);
			base.HttpContext.Response.Cookies.Add(Role);


			Email.Expires = DateTime.Now.AddDays(-1.0);
			base.HttpContext.Response.Cookies.Add(Email);


			Name.Expires = DateTime.Now.AddDays(-1.0);
			base.HttpContext.Response.Cookies.Add(Name);

			return View();
		}

		[HttpPost]
		public ActionResult Logout()
		{
			//HttpCookie cookie = new HttpCookie("Login");
			HttpCookie Token = new HttpCookie("Token");
			HttpCookie userID = new HttpCookie("userID");
			HttpCookie Role = new HttpCookie("Role");
			HttpCookie Email = new HttpCookie("Email");
			HttpCookie Name = new HttpCookie("Name");

			Token.Expires = DateTime.Now.AddDays(-1.0);
			base.HttpContext.Response.Cookies.Add(Token);


			userID.Expires = DateTime.Now.AddDays(-1.0);
			base.HttpContext.Response.Cookies.Add(userID);


			Role.Expires = DateTime.Now.AddDays(-1.0);
			base.HttpContext.Response.Cookies.Add(Role);


			Email.Expires = DateTime.Now.AddDays(-1.0);
			base.HttpContext.Response.Cookies.Add(Email);


			Name.Expires = DateTime.Now.AddDays(-1.0);
			base.HttpContext.Response.Cookies.Add(Name);


			//HttpContext.Response.Cookies.Remove("Token");

			//HttpContext.Response.Cookies.Remove("userID");

			//HttpContext.Response.Cookies.Remove("Role");

			//HttpContext.Response.Cookies.Remove("Email");

			//HttpContext.Response.Cookies.Remove("Name");



			//userID.Value = null;
			//Role.Value = null;
			//Email.Value = null;
			//Name.Value = null;
			return RedirectToAction("Login");
		}

		public ActionResult AccessDenied()
		{
			

			if (Request.Cookies["Token"] == null || Request.Cookies["Role"].ToString() == null || Request.Cookies["Name"] == null)
			{
				return RedirectToAction("Login", "Account");
			}
			return View();
		}

		public ActionResult MonthlySales()
		{
			if (Request.Cookies["Token"] == null || Request.Cookies["Role"] == null || Request.Cookies["Name"] == null)
			{
				return RedirectToAction("Login", "Account");
			}
			if ((Request.Cookies["Token"] != null && Request.Cookies["Role"].Value.ToString().Contains("Admin")) || Request.Cookies["Role"].Value.ToString().Contains("Assistant"))
			{
				return View();
			}
			return RedirectToAction("Login", "Account");
		}

		public ActionResult TodaySales()
		{
			if (Request.Cookies["Token"] == null || Request.Cookies["Role"] == null || Request.Cookies["Name"] == null)
			{
				return RedirectToAction("Login", "Account");
			}
			if ((Request.Cookies["Token"] != null && Request.Cookies["Role"].Value.ToString().Contains("Admin")) || Request.Cookies["Role"].Value.ToString().Contains("Assistant"))
			{
				return View();
			}
			return RedirectToAction("Login", "Account");
		}
	}
}