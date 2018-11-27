using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAM.Models.Enums {
	public enum Env {
		invalid,
		local_test_sqlite,
		local_mysql,
		local_sqlite,
		production
	}
}