using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tam.Models.Enums;
using FluentNHibernate.Mapping;

namespace Tam.Models {
	public class PassengerModel {
		public virtual long Id { get; protected set; }
		public virtual string Email { get; set; }
		public virtual string FirstName { get; set; }
		public virtual string LastName { get; set; }
		public virtual string MiddleName { get; set; }
		public virtual DateTime? BirthDate { get; set; }
		public virtual string Town { get; set; }
		public virtual string Province { get; set; }
		public virtual string City { get; set; }
		public virtual string MobileNumber { get; set; }
		public virtual string Password { get; set; }
		public virtual GenderType? Gender { get; set; }

		public virtual String Name() {
			return ((FirstName ?? "").Trim() + " " + (LastName ?? "").Trim()).Trim();
		}
		public PassengerModel() {
			
			CreateTime = DateTime.UtcNow;
		}
		public virtual DateTime CreateTime { get; set; }
		public virtual DateTime? DeleteTime { get; set; }
		public virtual DateTime? UpdateTime { get; set; }
		
		public virtual UserModel UpdatedBy { get; set; }

		public class PassengerModelMap : ClassMap<PassengerModel> {
			public PassengerModelMap() { 
				Id(x => x.Id);
				Map(x => x.Email).Index("Email_IDX").Length(30).UniqueKey("uniq");
				Map(x => x.FirstName).Length(30);
				Map(x => x.LastName).Length(30);
				Map(x => x.MiddleName).Length(30);
				Map(x => x.Gender).Length(10);
				Map(x => x.DeleteTime);
				Map(x => x.BirthDate);
				Map(x => x.CreateTime);
				Map(x => x.UpdateTime);
				Map(x => x.Town).Length(20);
				Map(x => x.Province).Length(20);
				Map(x => x.City).Length(20);
				Map(x => x.MobileNumber).Length(20);
				Map(x => x.Password);
				References(x => x.UpdatedBy, "UpdatedBy").Cascade.SaveUpdate();
			}
		}
	}
}