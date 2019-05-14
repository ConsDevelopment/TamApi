using Tam.Exceptions;
using Tam.Models;

using Tam.Properties;
using Tam.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;


namespace Tam.Accessor {

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
	public class Emailer {
		public static async Task SendMessage(string msg, string to, string subj) {
			

			MailMessage mail = new MailMessage();
			mail.To.Add(to);
			//mail.To.Add("Another Email ID where you wanna send same email");
			mail.From = new MailAddress("myaccess589@gmail.com");
			mail.Subject = "Registration";


			mail.Body = msg;

			mail.IsBodyHtml = true;
			SmtpClient smtp = new SmtpClient();
			smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
			smtp.Credentials = new System.Net.NetworkCredential
				 ("myaccess589@gmail.com", "'123qweR");
			//Or your Smtp Email ID and Password
			smtp.EnableSsl = true;
			smtp.Send(mail);
		}
		public static async Task SendMessage(string msg, IList<UserModel> to, string subj) {


			MailMessage mail = new MailMessage();
			for (var i = 0; i < to.Count; i++) {
				mail.To.Add(to[i].Email);
			}
			
			//mail.To.Add("Another Email ID where you wanna send same email");
			mail.From = new MailAddress("myaccess589@gmail.com");
			mail.Subject = "Registration";


			mail.Body = msg;

			mail.IsBodyHtml = true;
			SmtpClient smtp = new SmtpClient();
			smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
			smtp.Credentials = new System.Net.NetworkCredential
				 ("myaccess589@gmail.com", "'123qweR");
			//Or your Smtp Email ID and Password
			smtp.EnableSsl = true;
			smtp.Send(mail);
		}
	}

}