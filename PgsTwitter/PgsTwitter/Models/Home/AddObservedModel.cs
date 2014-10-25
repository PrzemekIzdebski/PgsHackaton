using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PgsTwitter.Models.Users
{
    public class AddObservedModel
    {
        public string ObservingUser { get; set; }
        public string ObservedUser { get; set; }
    }
}