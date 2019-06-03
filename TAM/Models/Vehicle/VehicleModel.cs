using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tam.Models.Enums;
using FluentNHibernate.Mapping;

namespace Tam.Models {
	public class VehicleModel {
		public virtual long Id { get; protected set; }
		public virtual string PlateNumber { get; set; }
		public virtual string RegisteredOwner { get; set; }
		public virtual VehicleType Vehicle { get; set;  }
		public virtual string FranchiseNo { get; set; }
		public virtual string CRNo { get; set; }
		public virtual string OrNo { get; set; }
		public virtual int Capacity { get; set; }
		public virtual string Make { get; set; }
		public virtual string Model { get; set; }
		public virtual int YearModel { get; set; }

		public VehicleModel() {

			CreateTime = DateTime.UtcNow;
		}
		public virtual DateTime CreateTime { get; set; }
		public virtual DateTime UpdateTime { get; set; }
		public virtual DateTime? DeleteTime { get; set; }
		public virtual UserModel UpdatedBy { get; set; }
		public virtual UserModel CreatedBy { get; set; }

		public class VehicleModelMap : ClassMap<VehicleModel> {
			public VehicleModelMap() { 
				Id(x => x.Id);
				Map(x => x.PlateNumber).Index("PlateNumber_IDX").Length(400).UniqueKey("uniq");
				Map(x => x.RegisteredOwner);
				Map(x => x.Vehicle).CustomType<VehicleType>();
				Map(x => x.FranchiseNo);
				Map(x => x.CRNo);
				Map(x => x.DeleteTime);
				Map(x => x.OrNo);
				Map(x => x.CreateTime);
				Map(x => x.Capacity);
				Map(x => x.UpdateTime);
				Map(x => x.Make);
				Map(x => x.Model);
				Map(x => x.YearModel);
				References(x => x.UpdatedBy, "UpdatedBy").Cascade.SaveUpdate();
				References(x => x.CreatedBy, "CreatedBy").Cascade.SaveUpdate();
			}
		}
	}
}