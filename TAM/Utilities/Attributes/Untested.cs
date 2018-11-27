using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAM {
	[AttributeUsage(AttributeTargets.Method)]
	public class Untested : Attribute {
		public string message;
		public string[] notes;

		public Untested(string message, params string[] toTest) {
			this.message = message;
			this.notes = toTest ?? new string[0];
		}
	}
}