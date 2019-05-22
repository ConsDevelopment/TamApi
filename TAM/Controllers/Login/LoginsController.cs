using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tam.Utilities;

namespace Tam.Controllers.Login
{
    public class LoginsController : Controller
    {
        // GET: Logins
        public ActionResult Login()
        {
			ViewData["ApiServer"] = Config.GetApiServerURL();
			return View();
        }
    }
}