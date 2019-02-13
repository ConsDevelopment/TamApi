using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tam.Models;
using Tam.Models.Enums;
using System.IO;
using System.Threading;
//using FireSharp.Config;
using System.Collections.Specialized;
using System.Configuration;
using NHibernate;

namespace Tam.Utilities {
	public class Config {
		public static void DbUpdateSuccessful() {
			var env = GetEnv();
			if (env == Env.production)
				return;

			var version = GetAppSetting("dbVersion", "0");
			var dir = Path.Combine(Path.GetTempPath(), "Tam");

			if (!Directory.Exists(dir)) {
				Directory.CreateDirectory(dir);
			}

			var file = Path.Combine(dir, "dbversion" + env + ".txt");
			if (!File.Exists(file))
				File.CreateText(file).Close();
			while (FileUtilities.IsFileLocked(new FileInfo(file))) {
				Thread.Sleep(100);
			}
			File.WriteAllText(file, version);
		}
		public static Env GetEnv() {
			Env result;
			var env = GetAppSetting("Env");
			if (env != null && Enum.TryParse(env.ToLower(), out result)) {
				return result;
			}
			return Env.mssql;
			//throw new Exception("Invalid Environment");
		}
		public static string GetAppSetting(string key, string deflt = null) {
			var config = System.Configuration.ConfigurationManager.AppSettings;
			return config[key] ?? deflt;
		}
		public static bool RunChromeExt() {
			switch (GetEnv()) {
				case Env.local_test_sqlite:
					return true;
				case Env.mssql:
					return false;
				case Env.test_server:
					return false;
				case Env.local_sqlite:
					return false;
				//case Env.local_mysql:
				//	return GetAppSetting("RunExt", "false").ToBooleanJS();
				case Env.production:
					return false;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		public static bool ShouldUpdateDB() {
			var version = GetAppSetting("dbVersion", "0");
			if (version == "0")
				return true;

			var env = GetEnv();

			switch (env) {
				case Env.local_test_sqlite:
					return true;
			
				case Env.mssql: {
						var dir = Path.Combine(Path.GetTempPath(), "Tam");
						var file = Path.Combine(dir, "dbversion" + env + ".txt");
						if (!Directory.Exists(dir))
							Directory.CreateDirectory(dir);
						if (!File.Exists(file)) {
							File.Create(file);
							while (!File.Exists(file)) {
								Thread.Sleep(100);
							}
							Thread.Sleep(100);
						}
						if (version == File.ReadAllText(file))
							return false;
						return true;
					}
				case Env.test_server: {
						var dir = Path.Combine(Path.GetTempPath(), "Tam");
						var file = Path.Combine(dir, "dbversion" + env + ".txt");
						if (!Directory.Exists(dir))
							Directory.CreateDirectory(dir);
						if (!File.Exists(file)) {
							File.Create(file);
							while (!File.Exists(file)) {
								Thread.Sleep(100);
							}
							Thread.Sleep(100);
						}
						if (version == File.ReadAllText(file))
							return false;
						return true;
					}
				case Env.production:
					return true;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

	}	
}
