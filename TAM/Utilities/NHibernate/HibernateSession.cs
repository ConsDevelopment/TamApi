using System.Diagnostics;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.AspNet.Identity.EntityFramework;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Envers.Configuration;
using NHibernate.Envers.Event;
using NHibernate.SqlCommand;
using NHibernate.Tool.hbm2ddl;
using TAM.App_Start;
using TAM.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using TAM.Models.Enums;
using TAM.Models.UserModels;

using TAM.Utilities.NHibernate;
using FluentConfiguration = NHibernate.Envers.Configuration.Fluent.FluentConfiguration;
using System.Threading.Tasks;
using NHibernate.Impl;
using System.Linq.Expressions;
using log4net;
using Mapping = NHibernate.Mapping;
using TAM.Utilities.Productivity;

using TAM.Utilities;
using NHibernate.Event;

namespace TAM.Utilities.NHibernate {
	public static class NHSQL {
		public static string NHibernateSQL { get; set; }
		public static bool SaveCommands { get; set; }
	}
	public class NHSQLInterceptor : EmptyInterceptor, IInterceptor {
		protected static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		SqlString IInterceptor.OnPrepareStatement(SqlString sql) {
			NHSQL.NHibernateSQL = sql.ToString();
			if (NHSQL.SaveCommands) {
				//log.Info(NHSQL.NHibernateSQL);
			}

			return sql;
		}
	}
	public class HibernateSession {
		private static Dictionary<Env, ISessionFactory> factories;
		private static Env? CurrentEnv;
		private static object lck = new object();
		static HibernateSession() {
			factories = new Dictionary<Env, ISessionFactory>();
		}
		public static RuntimeNames Names { get; private set; }
		public class RuntimeNames {
			private Configuration cfg;

			public RuntimeNames(Configuration cfg) {
				this.cfg = cfg;
			}

			public string ColumnName<T>(Expression<Func<T, object>> property)
				where T : class, new() {
				var accessor = FluentNHibernate.Utils.Reflection
					.ReflectionHelper.GetAccessor(property);

				var names = accessor.Name.Split('.');

				var classMapping = cfg.GetClassMapping(typeof(T));

				return WalkPropertyChain(classMapping.GetProperty(names.First()), 0, names);
			}

			private string WalkPropertyChain(Mapping.Property property, int index, string[] names) {
				if (property.IsComposite)
					return WalkPropertyChain(((Mapping.Component)property.Value).GetProperty(names[++index]), index, names);

				return property.ColumnIterator.First().Text;
			}

			public string TableName<T>() where T : class, new() {
				return cfg.GetClassMapping(typeof(T)).Table.Name;
			}
		}
		public static ISessionFactory GetDatabaseSessionFactory(Env? environmentOverride_testOnly = null) {
			Configuration c;
			var env = environmentOverride_testOnly ?? CurrentEnv ?? Config.GetEnv();
			CurrentEnv = env;
			//if (factories == null)
			//	factories = new Dictionary<Env, ISessionFactory>();
			if (!factories.ContainsKey(env)) {
				lock (lck) {
					ChromeExtensionComms.SendCommand("dbStart");
					var config = System.Configuration.ConfigurationManager.AppSettings;
					var connectionStrings = System.Configuration.ConfigurationManager.ConnectionStrings;

					switch (environmentOverride_testOnly ?? Config.GetEnv()) {
						
						case Env.local_mysql: {
								try {
									c = new Configuration();
									c.SetInterceptor(new NHSQLInterceptor());
									//SetupAudit(c);
									factories[env] = Fluently.Configure(c).Database(
												MySQLConfiguration.Standard.Dialect<MySQL5Dialect>().ConnectionString(connectionStrings["DefaultConnectionLocalMysql"].ConnectionString)/*.ShowSql()*/)
									   .Mappings(m => {
										   m.FluentMappings.AddFromAssemblyOf<UserModel>()
											   .Conventions.Add<StringColumnLengthConvention>();

									   })
									   .CurrentSessionContext("web")
									   .ExposeConfiguration(SetupAudit)
									   .ExposeConfiguration(BuildProductionMySqlSchema)
									   .BuildSessionFactory();
								} catch (Exception e) {
									var mbox = e.Message;
									if (e.InnerException != null && e.InnerException.Message != null)
										mbox = e.InnerException.Message;

									ChromeExtensionComms.SendCommand("dbError", mbox);
									throw e;
								}
								break;
							}
						
						
						default:
							throw new Exception("No database type");
					}
					Names = new RuntimeNames(c);
					ChromeExtensionComms.SendCommand("dbComplete");
				}
			}
			return factories[env];

		}
		private static void BuildProductionMySqlSchema(Configuration config) {
			var sw = Stopwatch.StartNew();
			//UPDATE DATABASE:
			var updates = new List<string>();
			//Microsoft.VisualStudio.Profiler.DataCollection.MarkProfile(1);

			if (Config.ShouldUpdateDB()) {
				var su = new SchemaUpdate(config);
				su.Execute(updates.Add, true);
				Config.DbUpdateSuccessful();
			}
			//Microsoft.VisualStudio.Profiler.DataCollection.MarkProfile(3);

			var end = sw.Elapsed;

			var auditEvents = new AuditEventListener();
			config.EventListeners.PreInsertEventListeners = new IPreInsertEventListener[] { auditEvents };
			config.EventListeners.PreUpdateEventListeners = new IPreUpdateEventListener[] { auditEvents };


			config.SetProperty("command_timeout", "600");
			//KILL/CREATE DATABASE:
			//new SchemaExport(config).Execute(true, true, false);
			// DELETE THE EXISTING DB ON EACH RUN
			/*if (!File.Exists(DbFile))
			{
				new SchemaExport(config).Create(false, true);
			}
			else
			{
				new SchemaUpdate(config).Execute(false, true);
			}*/

		}
		private static void SetupAudit(Configuration nhConf) {
										  
			var enversConf = new FluentConfiguration();
			nhConf.SetEnversProperty(ConfigurationKey.StoreDataAtDelete, true);
			nhConf.SetEnversProperty(ConfigurationKey.AuditStrategyValidityStoreRevendTimestamp, true);
			nhConf.SetEnversProperty(ConfigurationKey.AuditStrategy, typeof(CustomValidityAuditStrategy));


			

			
			
			//enversConf.Audit<UserOrganizationModel>()
			//	.ExcludeRelationData(x => x.Groups)
			//	.ExcludeRelationData(x => x.ManagingGroups)
			//	.Exclude(x => x.Cache);
			//.ExcludeRelationData(x => x.CustomQuestions);
			//enversConf.Audit<PositionDurationModel>();
			//enversConf.Audit<QuestionModel>();
			//enversConf.Audit<TeamDurationModel>();
			//enversConf.Audit<ManagerDuration>();
			//enversConf.Audit<OrganizationTeamModel>();
			//enversConf.Audit<OrganizationPositionModel>();
			//enversConf.Audit<PositionModel>();

			//enversConf.Audit<OrganizationModel>();
			//enversConf.Audit<ResponsibilityGroupModel>();
			//enversConf.Audit<ResponsibilityModel>();
			enversConf.Audit<TempUserModel>();
			//enversConf.Audit<UserLookup>()
			//	.Exclude(x => x.LastLogin);
			enversConf.Audit<UserModel>();
			enversConf.Audit<UserLogin>();
			enversConf.Audit<UserRoleModel>();
			enversConf.Audit<IdentityUserClaim>();

			//enversConf.Audit<PaymentSpringsToken>();
			//enversConf.Audit<ScheduledTask>();


			//enversConf.Audit<Dashboard>();
			//enversConf.Audit<TileModel>();

			//enversConf.Audit<AbstractVCProvider>();
			//enversConf.Audit<ZoomUserLink>();
			//enversConf.Audit<WebhookDetails>();
			//enversConf.Audit<WebhookEventsSubscription>();
			//enversConf.Audit<Task_Camunda>();
			//enversConf.Audit<ProcessDef_Camunda>();
			//enversConf.Audit<ProcessDef_CamundaFile>();
			//enversConf.Audit<ProcessInstance_Camunda>();
			//enversConf.Audit<Task_Camunda>();
			//enversConf.Audit<TokenIdentifier>();
			nhConf.IntegrateWithEnvers(enversConf);
		}
		public static ISession GetCurrentSession(bool singleSession = true, Env? environmentOverride_TestOnly = null) {

			if (singleSession && !(HttpContext.Current == null || HttpContext.Current.Items == null) && HttpContext.Current.Items["IsTest"] == null) {
				try {
					var session = GetExistingSingleRequestSession();
					if (session == null) {
						session = new SingleRequestSession(GetDatabaseSessionFactory(environmentOverride_TestOnly).OpenSession()); // Create session, like SessionFactory.createSession()...
						HttpContext.Current.Items.Add("NHibernateSession", session);
					} else {
						session.AddContext();
					}
					return session;
				} catch (Exception) {
					//Something went wrong.. revert
					//var a = "Error";
				}
			}
			if (!(HttpContext.Current == null || HttpContext.Current.Items == null) && HttpContext.Current.Items["IsTest"] != null)
				return GetDatabaseSessionFactory(environmentOverride_TestOnly).OpenSession();
			if (singleSession == false)
				return GetDatabaseSessionFactory(environmentOverride_TestOnly).OpenSession();

			return new SingleRequestSession(GetDatabaseSessionFactory(environmentOverride_TestOnly).OpenSession(), true);
			//GetDatabaseSessionFactory().OpenSession();
			/*while(true)
			{
				lock (lck)
				{
					if ( Session == null || !Session.IsOpen )
					{
						Session = GetDatabaseSessionFactory().OpenSession();
						return Session;
					}
				}
				Thread.Sleep(10);
			}*/
		}
		private static SingleRequestSession GetExistingSingleRequestSession() {
			if (!(HttpContext.Current == null || HttpContext.Current.Items == null) && HttpContext.Current.Items["IsTest"] == null) {
				try {
					var session = (SingleRequestSession)HttpContext.Current.Items["NHibernateSession"];
					return session;
				} catch (Exception) {
					//Something went wrong.. revert
					//var a = "Error";
				}
			}
			return null;
		}

	}
}