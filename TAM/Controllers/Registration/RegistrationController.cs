using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TAM.Accessors;
using TAM.Models;
using TAM.Models.UserModels;

namespace TAM.Controllers {
	public class RegistrationController : ApiController {
		// GET api/<controller>
		public IEnumerable<string> Get() {
			return new string[] { "value1", "value2" };
		}

		// GET api/<controller>/5
		public string Get(int id) {
			return "value";
		}

		// POST api/<controller>
		public void Post(UserModel model) {
			if (ModelState.IsValid) {
				

				var user = new UserModel() { UserName = model.Email.ToLower(),
						FirstName = model.FirstName,
						LastName = model.LastName,
						BirthDate=model.BirthDate,
						City=model.City,
					ContactNumber=model.ContactNumber};

				UserAccessor.CreateUser( user);
			}
			}

		// PUT api/<controller>/5
		public void Put(int id, [FromBody]string value) {
		}

		// DELETE api/<controller>/5
		public void Delete(int id) {
		}
	}
}