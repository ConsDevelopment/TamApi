using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tam.Models.Enums;
using FluentNHibernate.Mapping;

namespace Tam.Models {
	public class DriverModel {
		public virtual long Id { get; protected set; }
		public virtual string FirstName { get; set; }
		public virtual string LastName { get; set; }
		public virtual string Email { get; set;  }
		public virtual string MiddleName { get; set; }
		public virtual GenderType? Gender { get; set; }
		public virtual String Name() {
			return ((FirstName ?? "").Trim() + " " + (LastName ?? "").Trim()).Trim();
		}
		public DriverModel() {

			CreateTime = DateTime.UtcNow;
		}
		public virtual DateTime CreateTime { get; set; }
		public virtual DateTime? DeleteTime { get; set; }
		public virtual DateTime BirthDate { get; set; }
		public virtual string HomeAddress { get; set; }
		public virtual string Town { get; set; }
		public virtual string District { get; set; }
		public virtual string Province { get; set; }
		public virtual string City { get; set; }
		public virtual string MobileNumber { get; set; }
		public virtual string LicenseNumber { get; set; }
		public virtual string Password { get; set; }
		public virtual bool? isAccepted { get; set; }
		public virtual UserModel UpdatedBy { get; set; }

		public class DriverModelMap : ClassMap<DriverModel> {
			public DriverModelMap() { 
				Id(x => x.Id);
				Map(x => x.Email).Index("Email_IDX").Length(400).UniqueKey("uniq");
				Map(x => x.FirstName);
				Map(x => x.LastName);
				Map(x => x.MiddleName);
				Map(x => x.Gender);
				Map(x => x.DeleteTime);
				Map(x => x.BirthDate);
				Map(x => x.CreateTime);
				Map(x => x.HomeAddress);
				Map(x => x.Town);
				Map(x => x.District);
				Map(x => x.Province);
				Map(x => x.City);
				Map(x => x.MobileNumber);
				Map(x => x.LicenseNumber);
				Map(x => x.Password);
				Map(x => x.isAccepted);
				References(x => x.UpdatedBy, "UpdatedBy").Cascade.SaveUpdate();
			}
		}
	}
}