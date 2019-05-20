using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tam.NHibernate;
using Tam.Utilities;

namespace Tam.Controllers.Registration
{
    public class ConfirmationController : Controller
    {
        // GET: Confirmation
        public async Task<ActionResult> DriverConfirmation(string Id)
        {
			var id = HttpContext.Session["UserId"].ToString();
			ViewData["ApiServer"] = Config.GetApiServerURL();
			var nhs = new NHibernateUserStore();
			var usr = nhs.FindByIdAsync(id);
			if (!usr.Result.IsAdmin) {
				RedirectToAction("Login", "Logins");
			}
			var hds = new NHibernateDriverStore();
			var drivers = await hds.GetAllForValidationAsync();
			return View(drivers);
        }
    }
}