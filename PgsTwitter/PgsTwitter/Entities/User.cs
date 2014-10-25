using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon.DynamoDBv2.DataModel;

namespace PgsTwitter.Entities
{
    [DynamoDBTable(Table.Users)]
    public class User
    {
        [DynamoDBHashKey]
        public string Username { get; set; }


    }
}