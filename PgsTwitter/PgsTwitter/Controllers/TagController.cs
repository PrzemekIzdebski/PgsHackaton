using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PgsTwitter.Models.Tag;
using PgsTwitter.Services;

namespace PgsTwitter.Controllers
{
    public class TagController : BaseController
    {
        public ActionResult Index()
        {
            var service = new MessageService(Context);
            var tags = service.GetAllTags();
            var model = tags.Select(tag => new TagModel()
                {
                    Count = tag.Count,
                    Name = tag.Name
                });
            return View(model);
        }

    }
}