using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace TAM.Utilities.Productivity {
	public class ChromeExtensionComms {
		private static ChromeExtensionComms singleton = null;
		private static HttpListener _listener;
		private static List<String> commands = new List<string>();
		public static void SendCommand(string command, string details = null) {
			if (Config.RunChromeExt()) {
				try {
					if (singleton == null)
						singleton = new ChromeExtensionComms();
					singleton.Send(command, details);

				} catch (Exception) {
					// int a = 0;
				}
			}
		}
		protected void Send(string command, string details = null) {

			if (!Config.RunChromeExt())
				return;

			lock (_listener) {
				var toAdd = command;
				if (details != null)
					toAdd = toAdd + "~" + details;
				commands.Add("\"" + toAdd + "\"");
			}
		}
	}
}