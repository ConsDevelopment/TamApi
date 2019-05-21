
using System.Web.Optimization;
using Tam.Utilities;


namespace Tam
{
	public class BundleConfig {

		private static Bundle UpdateMinification(Bundle scripts) {
			if (Config.DisableMinification()) {
				scripts.Transforms.Clear();
			}
			return scripts;
		}


		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles) {

            //JQuery(bundles);
            //Bootstrap(bundles);
            //Compatability(bundles);
            //Main(bundles);

            login(bundles);
			Confirm(bundles);

			BundleTable.EnableOptimizations = Config.OptimizationEnabled();

		}


		//private static void Compatability(BundleCollection bundles) {
		//	bundles.Add(UpdateMinification(new ScriptBundle("~/bundles/compatability").Include(
		//			  "~/Scripts/Main/iefixes.js"
		//	)));
		//}

		//private static void Main(BundleCollection bundles) {

		//	var list = new List<string>() {
		//		"~/Scripts/Main/time.js",
		//		"~/Scripts/Main/linq.js",
		//		"~/Scripts/Main/radial.js",
		//		"~/Scripts/Main/modals.js",
		//		"~/Scripts/Main/datepickers.js",
		//		"~/Scripts/Main/support.js",
		//		"~/Scripts/Main/backwardcompatability.js",
		//		"~/Scripts/Main/ajaxintercepters.js",
		//		"~/Scripts/Main/datatable.js",
		//		"~/Scripts/Main/tours.js",
		//		"~/Scripts/Main/alerts.js",
		//		"~/Scripts/Main/clickableclass.js",
		//		"~/Scripts/Main/profilepicture.js",
		//		"~/Scripts/Main/libraries.js",
		//		"~/Scripts/Main/chart.js",
		//		"~/Scripts/Main/realtime.js",
		//	};


		//	//Only intercept logs if not local...
		//	if (Config.GetEnv() != Env.local_mysql)
		//		list.Add("~/Scripts/Main/log-helper.js");

		//	list.AddRange(new[] {
  //              /*"~/Scripts/jquery.signalR-{version}.js",Was deleted*/
  //              "~/Scripts/jquery/jquery.tablesorter.js",
		//		"~/Scripts/Main/finally.js",
		//		"~/Scripts/Main/intercom.min.js",
		//		"~/Scripts/L10/jquery-ui.color.js",
		//		"~/Scripts/jquery/jquery.tabbable.js",
		//		"~/Scripts/components/milestones.js",
		//		"~/Scripts/Main/keyboard.js",
		//		"~/Scripts/Main/tooltips.js",
		//		"~/Scripts/Main/beta.js"
		//	});

		//	bundles.Add(UpdateMinification(new ScriptBundle("~/bundles/main").Include(list.ToArray())));

		//}


		

		

		

		
        private static void login(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/Content/Login/login")
                .Include("~/Content/Login/login.css"));
            bundles.Add(UpdateMinification(new ScriptBundle("~/scripts/Logins/Login").Include(
                      "~/scripts/Logins/Login.js"
            )));
        }
		private static void Confirm(BundleCollection bundles) {

			
			bundles.Add(UpdateMinification(new ScriptBundle("~/scripts/Confirmation/Confirm").Include(
					  "~/scripts/Confirmation/Confirm.js"
			)));
		}



		//private static void Bootstrap(BundleCollection bundles) {
		//	// Use the development version of Modernizr to develop with and learn from. Then, when you're
		//	// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
		//	//bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
		//	//			"~/Scripts/modernizr-*"));

		//	bundles.Add(UpdateMinification(new ScriptBundle("~/bundles/bootstrap").Include(
		//			  "~/Scripts/components/posneg.js",
		//			  "~/Scripts/components/tristate.js",
		//			  "~/Scripts/components/fivestate.js",
		//			  "~/Scripts/components/checktree.js",
		//			  "~/Scripts/components/rockstate.js",
		//			  "~/Scripts/components/approvereject.js",
		//			  "~/Scripts/components/completeincomplete.js",
		//			  "~/Scripts/select2.min.js",
		//			  "~/Scripts/bootstrap.js",
		//			  "~/Scripts/respond.js",
		//			  "~/Scripts/bootstrap-slider.js",
		//			  "~/Scripts/bootstrap-datepicker.js")));

		//	bundles.Add(new StyleBundle("~/Content/css").Include(
		//			"~/Content/components/posneg.css",
		//			"~/Content/components/tristate.css",
		//			"~/Content/components/fivestate.css",
		//			"~/Content/components/table.css",
		//			"~/Content/components/checktree.css",
		//			"~/Content/components/rockstate.css",
		//			"~/Content/components/approvereject.css",
		//			"~/Content/components/CompleteIncomplete.css",
		//			"~/Content/select2-bootstrap.css",
		//			"~/Content/select2.css",
		//			"~/Content/datepicker.css",
		//			"~/Content/bootstrap/bootstrap.css",
		//			//"~/Content/Bootstrap-tabs.css",
		//			"~/Content/bootstrap.vertical-tabs.css",
		//			"~/Content/slider.css",
		//			"~/Content/site.css",
		//			"~/Content/bootstrap/custom/Site.css",
		//			"~/Content/Fonts.css",
		//			"~/Content/jquery.qtip.css"));
		//}

		//private static void JQuery(BundleCollection bundles) {
		//	bundles.Add(UpdateMinification(new ScriptBundle("~/bundles/jquery")
		//					.Include("~/Scripts/jquery-{version}.js")
		//					.Include("~/Scripts/jquery.unobtrusive-ajax.js")
		//					.Include("~/Scripts/jquery/jquery.qtip.js")
		//					//.Include("~/Scripts/jquery/jquery.attrchange.js")
		//					));

		//	bundles.Add(UpdateMinification(new ScriptBundle("~/bundles/animations")
		//		.Include("~/Scripts/animations/*.js")
		//		.Include("~/Scripts/jquery/*.js")
		//		));

		//	bundles.Add(UpdateMinification(new ScriptBundle("~/bundles/jqueryval").Include(
		//				"~/Scripts/jquery.validate*")));
		//}

	}
}
