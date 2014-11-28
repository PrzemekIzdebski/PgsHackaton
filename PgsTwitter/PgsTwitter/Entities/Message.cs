using System;
using Amazon.DynamoDBv2.DataModel;

namespace PgsTwitter.Entities
{
    [DynamoDBTable(Table.Messages)]
    public class Message
    {
        [DynamoDBHashKey]
        public string Username { get; set; }

        [DynamoDBProperty]
        public string Text { get; set; }

        [DynamoDBProperty]
        public string Id { get; set; }

        [DynamoDBRangeKey]
        public long PostedOn { get; set; }
    }
    [DynamoDBTable(Table.MessageFollowers)]
    public class MessageFollower
    {
        [DynamoDBHashKey]
        public string Author { get; set; }

        [DynamoDBRangeKey]
        public string Follower { get; set; }

        [DynamoDBProperty]
        public string MessageId { get; set; }
    }
}