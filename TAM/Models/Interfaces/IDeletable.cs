using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAM.Models.Interfaces {
	public interface IDeletable {
		DateTime? DeleteTime { get; set; }
	}
}