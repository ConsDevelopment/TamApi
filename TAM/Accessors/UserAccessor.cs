using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TAM.Models;
using TAM.Utilities.NHibernate;

namespace TAM.Accessors {
	public class UserAccessor  {
		public static void CreateUser(UserModel userModel) {
			using (var s = HibernateSession.GetCurrentSession()) {
				using (var tx = s.BeginTransaction()) {
					s.Save(userModel);
					tx.Commit();
					s.Flush();
				}
			}
		}
	}
}