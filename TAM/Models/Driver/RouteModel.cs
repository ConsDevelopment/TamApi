using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tam.Models.Enums;
using FluentNHibernate.Mapping;

namespace Tam.Models {
	public class RouteModel {
		public virtual long Id { get; protected set; }
		public virtual string Origin { get; set; }
		public virtual string Destination { get; set; }
		public RouteModel() {

			CreateTime = DateTime.UtcNow;
		}
		public virtual DateTime CreateTime { get; set; }
		public virtual DateTime UpdateTime { get; set; }
		public virtual DateTime? DeleteTime { get; set; }
		public virtual UserModel UpdatedBy { get; set; }
		public virtual UserModel CreatedBy { get; set; }

		public class RouteModelMap : ClassMap<RouteModel> {
			public RouteModelMap() { 
				Id(x => x.Id);
				Map(x => x.Origin);
				Map(x => x.Destination);
				Map(x => x.UpdateTime);
				Map(x => x.DeleteTime);
				Map(x => x.CreateTime);
				References(x => x.UpdatedBy, "UpdatedBy").Cascade.SaveUpdate();
				References(x => x.CreatedBy, "CreatedBy").Cascade.SaveUpdate();
			}
		}
	}
}