using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tam.Models.Enums;
using FluentNHibernate.Mapping;

namespace Tam.Models {
	public class ViaModel {
		public virtual long Id { get; protected set; }
		public virtual string Name { get; set; }
		public virtual RouteModel Route { get; set; }
		public ViaModel() {

			CreateTime = DateTime.UtcNow;
		}
		public virtual DateTime CreateTime { get; set; }
		public virtual DateTime? UpdateTime { get; set; }
		public virtual DateTime? DeleteTime { get; set; }
		public virtual UserModel UpdatedBy { get; set; }
		public virtual UserModel CreatedBy { get; set; }

		public class ViaModelMap : ClassMap<ViaModel> {
			public ViaModelMap() { 
				Id(x => x.Id);
				Map(x => x.Name).Length(30);
				
				Map(x => x.UpdateTime);
				Map(x => x.DeleteTime);
				Map(x => x.CreateTime);
				References(x => x.UpdatedBy, "UpdatedBy").Cascade.SaveUpdate();
				References(x => x.CreatedBy, "CreatedBy").Cascade.SaveUpdate();
				References(x => x.Route, "Route").Cascade.SaveUpdate();
			}
		}
	}
}