using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PgsTwitter.Controllers.Login;
using PgsTwitter.Models.Login;

namespace PgsTwitter.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        [SkipLoginCheck]
        public ActionResult Login()
        {
            var model = new LoginModel();
            return View(model);
        }

    }
}
