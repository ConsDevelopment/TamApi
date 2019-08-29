using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tam.Models.Enums;
using FluentNHibernate.Mapping;

namespace Tam.Models {
	public class BookingModel {
		public virtual long Id { get; protected set; }
		public virtual PassengerModel Passenger { get; set; }
		public virtual string Origin { get; set; }
		public virtual string Destination { get; set; }
		public virtual TripStatusType Status { get; set; }
		public virtual TerminalModel Terminal { get; set; }
		public BookingModel() {

			CreateTime = DateTime.UtcNow;
		}
		public virtual DateTime CreateTime { get; set; }
		public virtual DateTime? UpdateTime { get; set; }
		public virtual DateTime? DeleteTime { get; set; }
		public virtual UserModel UpdatedBy { get; set; }
		public virtual UserModel CreatedBy { get; set; }

		public class BookingModelMap : ClassMap<BookingModel> {
			public BookingModelMap() { 
				Id(x => x.Id);
				Map(x => x.Origin).Length(30);
				Map(x => x.Destination).Length(30);
				Map(x => x.Status).Length(300);
				Map(x => x.UpdateTime);
				Map(x => x.DeleteTime);
				Map(x => x.CreateTime);
				References(x => x.UpdatedBy, "UpdatedBy").Cascade.SaveUpdate();
				References(x => x.CreatedBy, "CreatedBy").Cascade.SaveUpdate();
				References(x => x.Passenger, "Passenger").Cascade.SaveUpdate();
			}
		}
	}
}