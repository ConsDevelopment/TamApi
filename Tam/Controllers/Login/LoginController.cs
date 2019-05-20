using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Tam.Models;
using Tam.NHibernate;
using Tam.Utilities;

namespace Tam.Controllers.Login {
	public class LoginController : ApiController {
		// GET api/<controller>
		public IEnumerable<string> Get() {
			return new string[] { "value1", "value2" };
		}

		// GET api/<controller>/5
		public string Get(int id) {
			return "value";
		}

		// POST api/<controller>
		public async Task<string> Post(LoginModel Login) {
			NHibernateUserStore store = new NHibernateUserStore();
			
			var usr = await store.FindByNamePassAsync(Login.Username, Login.Password);
			//var usr = await store.FindByIdAsync("564267a0-ac19-4811-871c-a9d9011bdfe6");
			if (usr != null) {
				var session = HttpContext.Current.Session;
				session["UserId"] = usr.Id;
				usr = await HibernateSession.SignInUser(usr, Login.RememberMe);
				return usr.SecurityStamp;
			} else {
				return null;
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