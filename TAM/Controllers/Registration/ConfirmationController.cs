using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tam.Models;
using Tam.Models.UserModels;
using Tam.NHibernate;
using Tam.Utilities;

namespace Tam.Controllers.Registration
{
    public class ConfirmationController : Controller
    {
        // GET: Confirmation
        public async Task<ActionResult> DriverConfirmation()
        {
			string id=null ;
			
			var nhs = new NHibernateUserStore();
			UserModel usr = null;
			if (HttpContext.Session["UserId"] != null) {
				id = HttpContext.Session["UserId"].ToString();
			}
			if (id == null) {
				usr = await nhs.FindByStampAsync(CurrentUserSession.userSecurityStampCookie);
			} else {
				usr = await nhs.FindByIdAsync(id);
			}
			ViewData["ApiServer"] = Config.GetApiServerURL();
			
			
			if (usr==null || !usr.IsAdmin) {
				RedirectToAction("Login", "Logins");
			}
			var hds = new NHibernateDriverStore();
			var drivers = await hds.GetAllForValidationAsync();
			return View(drivers);
        }
    }
}