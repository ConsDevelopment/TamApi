using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Tam.Models;
using Tam.NHibernate;

namespace Tam.Accessor.Registrations {

	public class Registration {
		public async Task<bool> RegisterUser(UserModel user) {

			NHibernateUserStore store = new NHibernateUserStore();
			var usr = await store.FindByNameAsync(user.UserName);
			if (usr == null) {
				await store.CreateAsync(user);
				return true;
			}
			return false;
		}

	}
}