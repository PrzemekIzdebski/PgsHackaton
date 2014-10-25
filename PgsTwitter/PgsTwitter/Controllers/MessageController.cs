using System.Web.Mvc;
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

        public ActionResult List()
        {
            var msgs = MessageService.GetMessages(LoginServices.UserName);
            return View(msgs);
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
            return RedirectToAction("List");
        }
    }
}