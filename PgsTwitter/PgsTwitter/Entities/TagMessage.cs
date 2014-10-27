using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon.DynamoDBv2.DataModel;

namespace PgsTwitter.Entities
{
    [DynamoDBTable(Table.TagMessage)]
    public class TagMessage
    {
        [DynamoDBHashKey]
        public string Tag { get; set; }

        [DynamoDBRangeKey]
        public string MessageDigest { get; set; }

        [DynamoDBProperty]
        public string Username { get; set; }

        [DynamoDBProperty]
        public string Text { get; set; }

        [DynamoDBProperty]
        public long PostedOn { get; set; }

        public Message AsMessage()
        {
            return new Message()
                {
                    PostedOn = PostedOn,
                    Text = Text,
                    Username = Username
                };
        }
    }
}