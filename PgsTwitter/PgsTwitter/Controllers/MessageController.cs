using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using PgsTwitter.Models.Message;
using PgsTwitter.Services;

namespace PgsTwitter.Controllers
{
    public class MessageController : BaseController
    {
        private IMessageService MessageService { get; set; }

        public MessageController()
        {
            MessageService = new MessageService(Context);
        }

        public ActionResult List(string username)
        {
            //if (username.IsNullOrWhiteSpace())
            //{
            //    username = LoginServices.UserName;
            //}

            var msgs = MessageService.GetMessages(username);
            return View(msgs);
        }

        public ActionResult My()
        {
            var msgs = MessageService.GetMessages(LoginServices.UserName);
            return View("List", msgs);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(AddModel addModel)
        {
            MessageService.PostMessage(LoginServices.UserName, addModel.Text);
            return RedirectToAction("My");
        }
    }
}