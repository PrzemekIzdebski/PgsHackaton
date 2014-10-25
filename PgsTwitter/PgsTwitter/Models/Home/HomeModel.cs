using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PgsTwitter.Models.Home
{
    public class HomeModel
    {
        public string UserName { get; set; }
        public List<string> OtherUsersNames { get; set; }
        public List<string> LikeUserNames { get; set; }
        public List<string> ObservedByUsers { get; set; } 

    }
}