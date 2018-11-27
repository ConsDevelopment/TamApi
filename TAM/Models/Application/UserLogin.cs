using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TAM.Models.Interfaces;

namespace TAM.Models {
	public class UserLogin : IdentityUserLogin, ILongIdentifiable {
		public long Id { get; protected set; }
	}
}