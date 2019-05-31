using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tam.Models.Enums;
using FluentNHibernate.Mapping;

namespace Tam.Models {
	public class DriverVehicleModel {
		public virtual long Id { get; protected set; }
		public virtual VehicleModel Vehicle { get; set; }
		public virtual StatusType Status { get; set;  }
		public virtual RouteModel Route { get; set; }

		public DriverVehicleModel() {

			CreateTime = DateTime.UtcNow;
		}
		public virtual DateTime CreateTime { get; set; }
		public virtual DateTime UpdateTime { get; set; }
		public virtual DateTime? DeleteTime { get; set; }
		public virtual UserModel UpdatedBy { get; set; }
		public virtual UserModel CreatedBy { get; set; }

		public class DriverVehicleModelMap : ClassMap<DriverVehicleModel> {
			public DriverVehicleModelMap() { 
				Id(x => x.Id);
				Map(x => x.Status).CustomType<StatusType>();
				Map(x => x.DeleteTime);
				Map(x => x.CreateTime);
				Map(x => x.UpdateTime);
				References(x => x.UpdatedBy, "UpdatedBy").Cascade.SaveUpdate();
				References(x => x.CreatedBy, "CreatedBy").Cascade.SaveUpdate();
				References(x => x.Vehicle, "Vehicle").Cascade.SaveUpdate();
				References(x => x.Route, "Route").Cascade.SaveUpdate();
			}
		}
	}
}