using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon.DynamoDBv2.DataModel;

namespace PgsTwitter.Entities
{
    [DynamoDBTable(Table.Observing)]
    public class Observing
    {
        [DynamoDBHashKey]
        public String ObservingUser { get; set; }
        [DynamoDBRangeKey]
        public String ObservedUser { get; set; }
        
    }
}