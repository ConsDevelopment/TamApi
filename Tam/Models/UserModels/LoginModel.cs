using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tam.Models {
	public class LoginModel {
		public string Username { get; set; }
		public string Password { get; set; }
		public string LoginProvider { get; set; }
		public string ProviderKey { get; set; }

		
	}
}