using Tam.Models;
using Tam.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tam.NHibernate;
using Microsoft.AspNet.Identity;

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
				UserName = "myusername11@del.com"
			};
			CreateUser(usr);
		}
		
		public async void PasswordTesting() {

			NHibernateUserStore store = new NHibernateUserStore();			
		var usr = await store.FindByNameAsync("myusername4@del.com");
			await store.SetPasswordAsync(usr, "test2");
		}
	}
}