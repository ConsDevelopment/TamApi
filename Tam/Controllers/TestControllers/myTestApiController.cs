using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tam.Models;

namespace Tam.Controllers {
	public class myTestApiController : ApiController {
		//[Route("api/TestController/myTestApiController1/GetMessage/{quoteName}")]
		//[HttpGet, HttpPost]
		public string GetMessage(string quoteName) {
			return "Hello World";
		}
		// GET api/<controller>
		public IEnumerable<string> Get() {
			return new string[] { "value1", "value2" };
		}

		// GET api/<controller>/5
		public string Get(int id) {
			return "value";
		}
		public string Get(UserModel user) {
			return "value";
		}

		// POST api/<controller>
		public void Post([FromBody]string value) {
		}
		//[Route("http://localhost:63899/api/TestController/myTestApiController1")]
		
		public void Post(UserModel value) {
			UserModel usr = new UserModel();
			usr = value;
		}

		// PUT api/<controller>/5
		public void Put(int id, [FromBody]string value) {
		}

		// DELETE api/<controller>/5
		public void Delete(int id) {
		}
	}
}