using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PgsTwitter.Models.Home;
using PgsTwitter.Services;

namespace PgsTwitter.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var model = new HomeModel()
                {
                    UserName = LoginServices.UserName
                };
            return View(model);
        }

        public ActionResult List()
        {
            var model = new Message() {Username = "Zenek"};
            return View(new List<Message>(){model});
        }

    }
}
