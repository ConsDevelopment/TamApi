using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Tam.Models;
using Tam.Models.Enums;
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
		public async Task<DriverProfile> Post(LoginModel Login) {
			NHibernateUserStore store = new NHibernateUserStore();
			try {
				var usr = await store.FindByNamePassAsync(Login.Username, Login.Password);
				//var usr = await store.FindByIdAsync("564267a0-ac19-4811-871c-a9d9011bdfe6");
				if (usr != null) {
					var session = HttpContext.Current.Session;
					session["UserId"] = usr.Id;
					usr = await HibernateSession.SignInUser(usr, Login.RememberMe);
					var vehicles = usr.Driver.Vehicle;
					var vehicle = (from x in vehicles.OfType<DriverVehicleModel>() where x.Status == StatusType.Active select x)
	  .FirstOrDefault();
					var driverProfile = new DriverProfile {
						LastName = usr.Driver.LastName,
						FirstName = usr.Driver.FirstName,
						MiddleName = usr.Driver.MiddleName,
						PlateNo = vehicle.Vehicle.PlateNumber,
						FranchiseNo=vehicle.Vehicle.FranchiseNo,
						Origin=vehicle.Route.Origin,
						Destination= vehicle.Route.Origin,
						Token=usr.SecurityStamp
						};
					return driverProfile;
				} else {
					return null;
				}
			} catch (Exception e) {
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