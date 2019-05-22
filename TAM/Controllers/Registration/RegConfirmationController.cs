using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Tam.Accessor;
using Tam.Models;
using Tam.Models.Enums;
using Tam.Models.UserModels;
using Tam.NHibernate;

namespace Tam.Controllers.Registration {
	public class RegConfirmationController : ApiController {
		// GET api/<controller>
		public IEnumerable<string> Get() {
			return new string[] { "value1", "value2" };
		}

		// GET api/<controller>/5
		public string Get(int id) {
			return "value";
		}

		// POST api/<controller>
		public async Task<string> Post(RegistrationSatusModel value) {

			string id = null;
			string result="Ok";
			var nus = new NHibernateUserStore();
			UserModel user = null;
			if (HttpContext.Current.Session["UserId"] != null) {
				id = HttpContext.Current.Session["UserId"].ToString();
			}
			if (id == null) {
				user = await nus.FindByStampAsync(CurrentUserSession.userSecurityStampCookie);
			} else {
				user = await nus.FindByIdAsync(id);
			}

			var nds = new NHibernateDriverStore();
			
			var driver = nds.FindByIdAsync(value.Id).Result;
			var usr = new UserModel {
				UserName = driver.Email,
				FirstName = driver.LastName,
				PasswordHash = driver.Password,
				Gender = driver.Gender,
				SecurityStamp = Guid.NewGuid().ToString(),
				Driver=driver
			};
			driver.Status = RegistrationStatus.Accepted;
			driver.UpdatedBy = user;
			await nus.CreateAsync(usr);
			try {
				await nds.UpdateAsync(driver);
			} catch (Exception e) {
				result = e.Message;
			}
			try {
				await nds.CreateDriverAsync(driver);
				var nhus = new NHibernateUserStore();
				var emails = driver.Email;
				await Emailer.SendMessage(driver.Email + " Activated", emails, "Registration");
			} catch (Exception e) {
				result = e.Message;
			}
			return result;
		}

		// PUT api/<controller>/5
		public void Put(int id, [FromBody]string value) {
		}

		// DELETE api/<controller>/5
		public void Delete(int id) {
		}
	}
}