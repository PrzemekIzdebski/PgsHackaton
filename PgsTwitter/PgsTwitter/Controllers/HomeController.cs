using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PgsTwitter.Models.Home;
using PgsTwitter.Models.Users;
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
            var usersNames = otherUsers.Select(u => u.Username).ToList();
            var loggedUserName = LoginServices.UserName;

            var likedUsers = userServices.GetObserved(loggedUserName);
            var likedByUsers = userServices.GetObserving(loggedUserName);

            var model = new HomeModel()
                {
                    UserName = loggedUserName,
                    OtherUsersNames = usersNames,
                    LikeUserNames = likedUsers,
                    ObservedByUsers = likedByUsers
                };

            return View(model);
        }

        public ActionResult List()
        {
            var model = new Message() {Username = "Zenek"};
            return View(new List<Message>(){model});
        }

        public ActionResult AddObserved(AddObservedModel addObservedModel)
        {
            var usersServices = new UserServices(Context);
            usersServices.AddToObserved(addObservedModel);
            return RedirectToAction("Index");
        }

    }
}
