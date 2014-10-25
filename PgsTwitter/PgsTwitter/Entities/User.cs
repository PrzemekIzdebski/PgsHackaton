using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon.DynamoDBv2.DataModel;

namespace PgsTwitter.Entities
{
    [DynamoDBTable(Table.Users)]
    public class User
    {
        public User()
        {
            LikedUsersIds = new List<string>();
        }

        [DynamoDBHashKey]
        public string Username { get; set; }

        [DynamoDBProperty]
        public List<string> LikedUsersIds { get; set; }

        public void AddToLiked(string observedUser)
        {
            LikedUsersIds.Add(observedUser);
            LikedUsersIds = LikedUsersIds.Distinct().ToList();
        }
    }
}