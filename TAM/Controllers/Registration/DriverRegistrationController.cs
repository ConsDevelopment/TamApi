using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Tam.Accessor;
using Tam.Models;
using Tam.NHibernate;

namespace Tam.Controllers.Registration {
	[EnableCors(origins: "*", headers: "*", methods: "*")] // tune to your needs
	public class DriverRegistrationController : ApiController {
		// GET api/<controller>
		public IEnumerable<string> Get() {
			return new string[] { "value1", "value2" };
		}

		// GET api/<controller>/5
		public string Get(int id) {
			return "value";
		}

		// POST api/<controller>
		public async Task<string> Post(DriverModel value) {
			string result = "Ok";
			PasswordHasher ph = new PasswordHasher();
			var passHash = ph.HashPassword(value.Password);
			NHibernateDriverStore hds = new NHibernateDriverStore();
			var driver = new DriverModel {
				Email = value.Email,
				FirstName = value.FirstName,
				LastName = value.LastName,
				MiddleName = value.MiddleName,
				BirthDate = value.BirthDate,
				Gender = value.Gender,
				HomeAddress = value.HomeAddress,
				Town = value.Town,
				District = value.District,
				Province = value.Province,
				City = value.City,
				MobileNumber = value.MobileNumber,
				LicenseNumber = value.LicenseNumber,
				Password = passHash
			};
			try {
				await hds.CreateDriverAsync(driver);
				var nhus = new NHibernateUserStore();
				var emails = await nhus.GetAllAdminEmailAsync();
				await Emailer.SendMessage(driver.Email + " For Activation", emails, "Registration");
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