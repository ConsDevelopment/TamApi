using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tam.Models.Enums;

namespace Tam.Models {
	public class LoginModel {
		
		public string Username { get; set; }
		public string Password { get; set; }
		public string LoginProvider { get; set; }
		public string ProviderKey { get; set; }
		public bool RememberMe { get; set; }
	}
	public class RegistrationSatusModel {
		public long Id { get; set; }
		public RegistrationStatus status { get; set; }
	}
	public class DriverProfile {
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string PlateNo { get; set; }
		public string FranchiseNo { get; set; }
		public string Origin { get; set; }
		public string Destination { get; set; }
		public string Token { get; set; }
	}
}