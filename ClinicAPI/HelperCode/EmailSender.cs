using ClinicAPI.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace ClinicAPI.HelperCode
{
    public class EmailSender
    {
		public async Task<string> sendmail(string email, int status)
		{
			var body = "Thank You!<br/>Your information has been received. <br/>An admin staff member will call you about your online/in-person request within one to three business days.<br/> Please note that your appointment will be on hold until we confirm that payment has been received.<br/> Please send in the receipt of payment at: contact@synapse.org.pk<br/> or call on 03041118000<br/> You may call us at these numbers from 9:00 a.m.through 05:00 p.m.Monday through Friday for appointment - related queries.";

			MailMessage mail = new MailMessage();
			mail.To.Add(email);
			mail.From = new MailAddress("contact@synapse.org.pk");
			mail.Subject = "mail";
			mail.Body = body;
			mail.IsBodyHtml = true;
			SmtpClient smtp = new SmtpClient();
			smtp.Host = "smtp.org.pk";
			smtp.Port = 587;
			smtp.UseDefaultCredentials = false;
			smtp.Credentials = new NetworkCredential("contact@synapse.org.pk", "Redbull.1");
			smtp.EnableSsl = true;
			await smtp.SendMailAsync(mail);
			return "send";
		}

		public async Task<string> sendPrescription(string email,string filepath)
		{
			var body = "Thank You!<br/>Your information has been received. <br/>An admin staff member will call you about your online/in-person request within one to three business days.<br/> Please note that your appointment will be on hold until we confirm that payment has been received.<br/> Please send in the receipt of payment at: contact@synapse.org.pk<br/> or call on 03041118000<br/> You may call us at these numbers from 9:00 a.m.through 05:00 p.m.Monday through Friday for appointment - related queries.";

			MailMessage mail = new MailMessage();
			mail.To.Add(email);
			mail.From = new MailAddress("contact@synapse.org.pk");
			mail.Subject = "mail";
			mail.Body = body;
			mail.IsBodyHtml = true;
			SmtpClient smtp = new SmtpClient();
			smtp.Host = "smtp.org.pk";
			smtp.Port = 587;
			smtp.UseDefaultCredentials = false;
			smtp.Credentials = new NetworkCredential("contact@synapse.org.pk", "Redbull.1");
			smtp.EnableSsl = true;
			await smtp.SendMailAsync(mail);
			return "send";
		}
	}
}