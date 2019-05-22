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
}