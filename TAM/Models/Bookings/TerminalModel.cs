using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tam.Models.Enums;
using FluentNHibernate.Mapping;

namespace Tam.Models {
	public class TerminalModel {
		public virtual long Id { get; protected set; }
		public virtual string Name { get; set; }
		public virtual string Location { get; set; }
		public virtual long Longititude { get; set; }
		public virtual long Latitude { get; set; }
		public virtual string Address { get; set; }
		public virtual ICollection<VehicleModel> Vehicles { get; set; }
		public TerminalModel() {

			CreateTime = DateTime.UtcNow;
		}
		public virtual DateTime CreateTime { get; set; }
		public virtual DateTime? UpdateTime { get; set; }
		public virtual DateTime? DeleteTime { get; set; }
		public virtual UserModel UpdatedBy { get; set; }
		public virtual UserModel CreatedBy { get; set; }

		public class TerminalModelMap : ClassMap<TerminalModel> {
			public TerminalModelMap() { 
				Id(x => x.Id);
				Map(x => x.Name).Length(30);
				Map(x => x.Location).Length(30);
				Map(x => x.Longititude);
				Map(x => x.Latitude);
				Map(x => x.Address).Length(300);
				Map(x => x.UpdateTime);
				Map(x => x.DeleteTime);
				Map(x => x.CreateTime);
				References(x => x.UpdatedBy, "UpdatedBy").Cascade.SaveUpdate();
				References(x => x.CreatedBy, "CreatedBy").Cascade.SaveUpdate();
				HasManyToMany(x => x.Vehicles).Cascade.All().Table("TerminalVehicle");
			}
		}
	}
}