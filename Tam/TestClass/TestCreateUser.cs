using Tam.Models;
using Tam.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tam.TestClass {
	public class TestCreateUser {
		public void CreateUser(UserModel userModel) {
			using (var s = HibernateSession.GetCurrentSession()) {
				using (var tx = s.BeginTransaction()) {
					s.Save(userModel);
					tx.Commit();
					s.Flush();
				}
			}
		}
		public void createUser() {
			var usr = new UserModel() {
				FirstName = "cons",
				LastName = "mname",
				UserName = "myusername3@del.com"
			};
			CreateUser(usr);
		}
	}
}