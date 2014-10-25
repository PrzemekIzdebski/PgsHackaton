using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Amazon.DynamoDBv2.DataModel;
using PgsTwitter.DataAccess;

namespace PgsTwitter.Controllers
{
    public abstract class BaseController : Controller
    {
        protected DynamoDBContext Context { get; private set; }

        protected BaseController()
        {
            Context = Dynamo.GetContext();
        }

        //
        // GET: /Base/
        protected override void Dispose(bool disposing)
        {
            Context.Dispose();
            base.Dispose(disposing);
        }
    }
}
