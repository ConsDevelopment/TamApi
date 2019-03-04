using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Tam.Accessor.Registration;
using Tam.Models;

namespace Tam.Controllers {
	[EnableCors(origins: "*", headers: "*", methods: "*")] // tune to your needs
	public class RegistrationController : ApiController {
		// GET api/<controller>
		//[EnableCors(origins: "*", headers: "*", methods: "*")]
		public IEnumerable<string> Get() {
			return new string[] { "value1", "value2" };
		}

		// GET api/<controller>/5
		public string Get(int id) {
			return "value";
		}

		// POST api/<controller>
		//[EnableCors(origins: "*", headers: "*", methods: "*")]
		public bool Post(UserModel registor) {
			bool result=false;
			PasswordHasher ph = new PasswordHasher();
			var passHash = ph.HashPassword(registor.PasswordHash);
			UserModel user = new UserModel() {
				UserName = registor.UserName,
				PasswordHash = passHash,
				LastName = registor.LastName,
				FirstName = registor.FirstName,
				SecurityStamp = Guid.NewGuid().ToString()
			};
			Registration reg = new Registration();
			var isNotExists = reg.RegisterUser(user).Result;

			if (isNotExists) {
				result = true;
			} else {
				result = false;
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