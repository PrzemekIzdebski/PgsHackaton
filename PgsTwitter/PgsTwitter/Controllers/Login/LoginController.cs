using System.Web.Mvc;
using PgsTwitter.Models.Login;
using PgsTwitter.Services;

namespace PgsTwitter.Controllers.Login
{
    public class LoginController : BaseController
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
            LoginServices.LogIn(model.Name, Context);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOut()
        {
            LoginServices.LogOut();
            return RedirectToAction("Login");
        }


    }
}
