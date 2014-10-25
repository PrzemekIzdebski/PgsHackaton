using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PgsTwitter.Models.Home;
using PgsTwitter.Services;

namespace PgsTwitter.Controllers
{
    public class HomeController : BaseController
    {


        //
        // GET: /Home/

        public ActionResult Index()
        {
            var userServices = new UserServices(Context);
            var otherUsers = userServices.List();
            var usersNames = otherUsers.Select(user => user.Username).ToList();

            var model = new HomeModel()
                {
                    UserName = LoginServices.UserName,
                    OtherUsersNames = usersNames
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
