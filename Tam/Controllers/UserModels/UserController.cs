using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tam.Models;


namespace Tam.Controllers {
	public class UserController : ApiController {
		// GET api/<controller>
		//public IEnumerable<string> Get() {
		//	return new string[] { "value1", "value2" };
		//}

		// GET api/<controller>/5
		//[Route("api/UserModels/UserController/{usr}")]
		//public string Get(UserModel source) {
		//	UserModel usr = new UserModel();
		//	usr.UserName = source.UserName;
		//	return "value";
		//}

		//POST api/<controller>
		[AcceptVerbs("GET", "POST")]
		public UserModel Post(UserModel source) {
			UserModel usr = new UserModel();
			usr.UserName = source.UserName;
			return usr;
		}

		// PUT api/<controller>/5
		public void Put(int id, [FromBody]string value) {
		}

		// DELETE api/<controller>/5
		public void Delete(int id) {
		}
	}
}