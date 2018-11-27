using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAM {
	public static class ObjectExtensions {
		public static bool ToBooleanJS(this String s) {
			if (s == null)
				return false;
			var l = s.ToLower();
			return l.Contains("true") || l == "on";
		}
	}
}