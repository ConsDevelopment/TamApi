using System.Diagnostics;
using FluentNHibernate.Mapping;
using Tam.Models.Enums;
using System;
using Microsoft.AspNet.Identity.EntityFramework;
using Tam.Models.UserModels;
using System.Collections.Generic;

namespace Tam.Models {
	[DebuggerDisplay("{FirstName} {LastName}")]
	public class UserModel : IdentityUser {
		public virtual string FirstName { get; set; }
		public virtual string LastName { get; set; }
		public virtual string Email { get { return UserName; } }
		public virtual long CurrentRole { get; set; }
		public virtual GenderType? Gender { get; set; }
		public virtual String Name() {
			return ((FirstName ?? "").Trim() + " " + (LastName ?? "").Trim()).Trim();
		}
		public UserModel() {
			
			CreateTime = DateTime.UtcNow;
		}

		public virtual DateTime CreateTime { get; set; }
		public virtual DateTime? DeleteTime { get; set; }

		public virtual bool IsAdmin { get; set; }

		public virtual bool IsActive { get; set; }
		public virtual IList<UserRoleModel> Roles { get; set; }
		public virtual ICollection<UserLogin> Logins { get; set; }
		public virtual UserTypes? UserType { get; set; }
		public virtual UserModel CreatedBy { get; set; }
		public virtual DriverModel Driver { get; set; }

		public class UserModelMap : ClassMap<UserModel> {
			public UserModelMap() {
				Id(x => x.Id).CustomType(typeof(string)).GeneratedBy.Custom(typeof(GuidStringGenerator)).Length(36);
				Map(x => x.UserName).Index("UserName_IDX").Length(400).UniqueKey("uniq");
				Map(x => x.FirstName).Not.LazyLoad();
				Map(x => x.LastName).Not.LazyLoad();
				Map(x => x.PasswordHash);
				Map(x => x.CurrentRole);
				Map(x => x.IsAdmin);
				Map(x => x.DeleteTime);
				Map(x => x.Gender);
				Map(x => x.CreateTime);

				Map(x => x.SecurityStamp);
				Map(x => x.IsActive);
				Map(x => x.UserType);
				HasMany(x => x.Logins).Cascade.SaveUpdate();
				HasMany(x => x.Roles).Cascade.SaveUpdate();
				References(x => x.CreatedBy, "CreatedBy").Cascade.SaveUpdate();
				References(x => x.Driver, "Driver").Cascade.SaveUpdate();
			}
		}



		
	}

}
