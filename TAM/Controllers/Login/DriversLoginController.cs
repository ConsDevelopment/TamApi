using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Script.Services;
using System.Web.Services;
using Tam.Models;
using Tam.Models.Enums;
using Tam.NHibernate;

namespace Tam.Controllers.Login {
	[EnableCors(origins: "*", headers: "*", methods: "*")] // tune to your needs
	public class DriversLoginController : ApiController {
		// GET api/<controller>
		//public IEnumerable<string> Get() {
		//	return new string[] { "value1", "value2" };
		//}

		// GET api/<controller>/5
		public async Task<DriverProfile> Post(LoginModel Login) {
			NHibernateUserStore store = new NHibernateUserStore();
			var usr = await store.FindByNamePassAsync(Login.Username, Login.Password);
			if (usr.Driver != null) {
				string Origin = "", Destination = "", FranchiseNo = "", PlateNo = "";
					var vehicle = (from x in usr.Driver.Vehicle.OfType<DriverVehicleModel>() where x.Status == StatusType.Active select x)
		.FirstOrDefault();

					if (vehicle != null) {
						PlateNo = vehicle.Vehicle.PlateNumber;
						FranchiseNo = vehicle.Vehicle.FranchiseNo;
						if (vehicle.Route != null) {
							Origin = vehicle.Route.Origin;
							Destination = vehicle.Route.Destination;
						}
					}
				
				var driverProfile = new DriverProfile {
					LastName = usr.Driver.LastName,
					FirstName = usr.Driver.FirstName,
					MiddleName = usr.Driver.MiddleName,
					PlateNo = PlateNo,
					FranchiseNo = FranchiseNo,
					Origin = Origin,
					Destination = Destination,
					Token = usr.SecurityStamp
				};
				return driverProfile;
			}
			return null;
		}

		// POST api/<controller>
		//public void Post([FromBody]string value) {

		//}

		// PUT api/<controller>/5
		public void Put(int id, [FromBody]string value) {
		}

		// DELETE api/<controller>/5
		public void Delete(int id) {
		}
	}
}