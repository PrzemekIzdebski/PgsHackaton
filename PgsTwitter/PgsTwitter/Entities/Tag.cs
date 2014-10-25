using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon.DynamoDBv2.DataModel;

namespace PgsTwitter.Entities
{
    [DynamoDBTable(Table.Tag)]
    public class Tag
    {
        [DynamoDBHashKey]
        public string Name { get; set; }

        [DynamoDBProperty]
        public int Count { get; set; }
    }
}