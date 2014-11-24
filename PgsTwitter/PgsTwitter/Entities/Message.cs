using System;
using Amazon.DynamoDBv2.DataModel;

namespace PgsTwitter.Entities
{
    [DynamoDBTable(Table.Messages)]
    public class Message
    {   
        [DynamoDBHashKey]
        public string Receiver { get; set; }

        [DynamoDBProperty]
        public string Author { get; set; }

        [DynamoDBProperty]
        public string Text { get; set; }

        [DynamoDBRangeKey]
        public long PostedOn { get; set; }

        public string Digest
        {
            get { return string.Format("{0} {1}", Receiver, PostedOn); }
        }
    }
}