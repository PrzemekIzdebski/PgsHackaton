using System;
using Amazon.DynamoDBv2.DataModel;

namespace PgsTwitter.Entities
{
    public class Message
    {
        [DynamoDBHashKey]
        public long Id { get; set; }

        [DynamoDBProperty]
        public string Username { get; set; }

        [DynamoDBProperty]
        public string Text { get; set; }

        [DynamoDBProperty]
        public DateTime PostedOn { get; set; }
    }
}