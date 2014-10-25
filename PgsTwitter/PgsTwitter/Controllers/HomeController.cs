using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PgsTwitter.Models.Home;

namespace PgsTwitter.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            var model = new Message() {Username = "Zenek"};
            return View(new List<Message>(){model});
        }

    }
}
