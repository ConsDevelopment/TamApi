using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tam.Models.Enums;
using FluentNHibernate.Mapping;

namespace Tam.Models {
	public class TripModel {
		public virtual long Id { get; protected set; }
		public virtual DriverVehicleModel DVR { get; set; }
		public virtual ViaModel Via { get; set; }
		public virtual TripStatusType Status { get; set; }
		public TripModel() {

			CreateTime = DateTime.UtcNow;
		}
		public virtual DateTime CreateTime { get; set; }
		public virtual DateTime? UpdateTime { get; set; }
		public virtual DateTime? DeleteTime { get; set; }
		public virtual UserModel UpdatedBy { get; set; }
		public virtual UserModel CreatedBy { get; set; }

		public class TripModelMap : ClassMap<TripModel> {
			public TripModelMap() { 
				Id(x => x.Id);
				Map(x => x.Status).Length(20);
				Map(x => x.UpdateTime);
				Map(x => x.DeleteTime);
				Map(x => x.CreateTime);
				References(x => x.UpdatedBy, "UpdatedBy").Cascade.SaveUpdate();
				References(x => x.CreatedBy, "CreatedBy").Cascade.SaveUpdate();
				References(x => x.DVR, "DVR").Cascade.SaveUpdate();
				References(x => x.Via, "Via").Cascade.SaveUpdate();
			}
		}
	}
}