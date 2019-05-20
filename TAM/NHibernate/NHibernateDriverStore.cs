using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Tam.Exceptions;
using Tam.Models;
using Tam.Models.UserModels;
using Tam.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Security.Claims;
using Tam.Models.Enums;

namespace Tam.NHibernate {
	// Summary:
	//     Implements IUserStore using EntityFramework where TUser is the entity type
	//     of the user being stored
	//
	// Type parameters:
	//   TUser:


	//public class UserStore<TUser> : IUserLoginStore<TUser>, IUserClaimStore<TUser>, IUserRoleStore<TUser>,
	//IUserPasswordStore<TUser>, IUserSecurityStampStore<TUser>, IUserStore<TUser>, IDisposable where TUser
	//: global::Microsoft.AspNet.Identity.EntityFramework.IdentityUser

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

	public class NHibernateDriverStore : IDisposable {

		public async Task CreateDriverAsync(DriverModel driver) {
			using (var db = HibernateSession.GetCurrentSession()) {
				using (var tx = db.BeginTransaction()) {
					db.Save(driver);
					tx.Commit();
					db.Flush();
				}
			}
		}

		public async Task<IList<DriverModel>> GetAllForValidationAsync() {

			using (var db = HibernateSession.GetCurrentSession()) {
				using (var tx = db.BeginTransaction()) {
					return db.QueryOver<DriverModel>().Where(x => x.Status == RegistrationStatus.ForValidation && x.DeleteTime == null).List();
				}
			}

		}


		public void Dispose() {
			//TODO should this do anything?
		}

		
	}
}
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously