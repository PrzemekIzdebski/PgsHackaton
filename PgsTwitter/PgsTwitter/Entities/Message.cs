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

        [DynamoDBRangeKey]
        public long PostedOn { get; set; }
    }
}