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
            var msgs = MessageService.GetMessagesBy(username);
            return View(msgs);
        }

        public ActionResult My()
        {
            var msgs = MessageService.GetMessagesFor(LoginServices.UserName);
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