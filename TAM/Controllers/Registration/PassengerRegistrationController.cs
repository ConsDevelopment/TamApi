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
using Tam.Models.Enums;
using Tam.NHibernate;

namespace Tam.Controllers.Registration {
	[EnableCors(origins: "*", headers: "*", methods: "*")] // tune to your needs
	public class PassengerRegistrationController : ApiController {
		// GET api/<controller>
		public IEnumerable<string> Get() {
			return new string[] { "value1", "value2" };
		}

		// GET api/<controller>/5
		public string Get(int id) {
			return "value";
		}

		// POST api/<controller>
		public async Task<string> Post(PassengerModel value) {
			string result = "Ok";
			PasswordHasher ph = new PasswordHasher();
			var passHash = ph.HashPassword(value.Password);
			var hps = new NHibernatePassengerStore();
			var passenger = new PassengerModel {
				Email = value.Email,
				FirstName = value.FirstName,
				LastName = value.LastName,
				MiddleName = value.MiddleName,
				BirthDate = value.BirthDate,
				Gender = value.Gender,
				Town = value.Town,
				Province = value.Province,
				City = value.City,
				MobileNumber = value.MobileNumber,
				Password = passHash
			};
			var nhus = new NHibernateUserStore();
			var existingUser = await nhus.FindByNameAsync(passenger.Email);
			try {
				if (existingUser != null) {
					existingUser.Passenger = passenger;
					await nhus.UpdateAsync(existingUser);
				} else {
					var usr = new UserModel {
						UserName = passenger.Email,
						FirstName = passenger.FirstName,
						LastName = passenger.LastName,
						PasswordHash = passenger.Password,
						SecurityStamp = Guid.NewGuid().ToString(),
						Passenger = passenger
					};
					await nhus.CreateAsync(usr);
				}				
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