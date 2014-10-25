using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using PgsTwitter.Controllers.Login;
using PgsTwitter.Models.Login;
using PgsTwitter.Services;

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

        [SkipLoginCheck]
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            LoginServices.LogIn(model.Name);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOut()
        {
            LoginServices.LogOut();
            return RedirectToAction("Login");
        }


    }
}
