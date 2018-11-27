using FluentNHibernate.Mapping;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NHibernate.Id;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using TAM.Models.Enums;
using TAM.Models.Interfaces;
using TAM.Models.UserModels;

namespace TAM.Models {
	public class UserModel : IdentityUser, IDeletable, IStringIdentifiable {
		public virtual string FirstName { get; set; }
		public virtual string LastName { get; set; }
		public virtual string MiddleName { get; set; }
		public virtual DateTime? BirthDate { get; set; }
		public virtual string HomeAddress { get; set; }
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
		public virtual string Email { get { return UserName; } }
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
		public virtual string ImageGuid { get; set; }
		public virtual GenderType? Gender { get; set; }
		public virtual long CurrentRole { get; set; }
		public virtual string Province { get; set; }
		public virtual string City { get; set; }
		public virtual string Country { get; set; }
		public virtual string ContactNumber { get; set; }
		//[NotMapped]
		private string _ImageUrl { get; set; }

		public virtual String Name() {
			return ((FirstName ?? "").Trim() + " " + (LastName ?? "").Trim()).Trim();
		}

#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
		//public virtual IList<UserRoleModel> Roles { get; set; }

		//public virtual async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<UserModel> manager, string authenticationType) {
		//	// Note the authenticationType must match the one defined in 
		//	// CookieAuthenticationOptions.AuthenticationType
		//	var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
		//	// Add custom user claims here
		//	return userIdentity;
		//}
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword


		public UserModel() {
			//Roles = new List<UserRoleModel>();
			CreateTime = DateTime.UtcNow;
		}

		public virtual long? GetCurrentRole() {
			
				return CurrentRole;
			
		}


		public virtual DateTime? DeleteTime { get; set; }

		
		public class UserModelMap : ClassMap<UserModel> {
			public UserModelMap() {
				Id(x => x.Id).CustomType(typeof(string)).GeneratedBy.Custom(typeof(UUIDStringGenerator)).Length(36);
				Map(x => x.UserName).Index("UserName_IDX").Length(400);
				Map(x => x.FirstName).Not.LazyLoad();
				Map(x => x.LastName).Not.LazyLoad();
				Map(x => x.MiddleName).Not.LazyLoad();
				Map(x => x.BirthDate);
				Map(x => x.HomeAddress);
				Map(x => x.PasswordHash);
				Map(x => x.ContactNumber);
				Map(x => x.CurrentRole);
				Map(x => x.SecurityStamp);
				Map(x => x.DeleteTime);
				Map(x => x.ImageGuid);
				Map(x => x.Province);
				Map(x => x.City);
				Map(x => x.Country);
				Map(x => x.Gender);
				Map(x => x.CreateTime);
				Map(x => x.DisableTips);
				HasMany(x => x.Logins).Cascade.SaveUpdate();
				HasMany(x => x.Roles).Cascade.SaveUpdate();
				HasMany(x => x.Claims).Cascade.SaveUpdate();
			}
		}



		public virtual DateTime CreateTime { get; set; }
		public virtual bool DisableTips { get; set; }
	}



	public class IdentityUserLoginMap : ClassMap<IdentityUserLogin> {
		public IdentityUserLoginMap() {
			Id(x => x.UserId).Length(36);
			Map(x => x.LoginProvider);
			Map(x => x.ProviderKey);
		}
	}
	public class IdentityUserClaimMap : ClassMap<IdentityUserClaim> {
		public IdentityUserClaimMap() {
			Id(x => x.Id);
		}
	}
}