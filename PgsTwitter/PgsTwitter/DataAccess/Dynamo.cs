using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Compilation;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using PgsTwitter.Entities;

namespace PgsTwitter.DataAccess
{
    public static class Dynamo
    {

        public static DynamoDBContext GetContext()
        {
            var config = new AmazonDynamoDBConfig { ServiceURL = "http://localhost:12345" };
            var client = new AmazonDynamoDBClient("QWE", "XYZ", config);
            CreateTablesIfRequired(client);
            var context = new DynamoDBContext(client);
            return context;
        }

        private static void CreateTablesIfRequired(IAmazonDynamoDB client)
        {
            var tables = client.ListTables();

            if (!tables.TableNames.Contains(Table.Users))
            {
                CreateUserTable(client);
            }

            if (!tables.TableNames.Contains(Table.Messages))
            {
                CreateMessageTable(client);
            }

        }

        private static void CreateMessageTable(IAmazonDynamoDB client)
        {
            var createTableRequest = new CreateTableRequest();
            createTableRequest.TableName = Table.Messages;

            createTableRequest.KeySchema = new List<KeySchemaElement>
            {
                new KeySchemaElement
                {
                    AttributeName = "Username",
                    KeyType = KeyType.HASH
                },
                new KeySchemaElement
                {
                    AttributeName = "PostedOn",
                    KeyType = KeyType.RANGE
                }
            };

            createTableRequest.AttributeDefinitions = new List<AttributeDefinition>
            {
                new AttributeDefinition
                {
                    AttributeName = "Username",
                    AttributeType = ScalarAttributeType.S
                },
                new AttributeDefinition
                {
                    AttributeName = "PostedOn",
                    AttributeType = ScalarAttributeType.N
                },
            };

            createTableRequest.ProvisionedThroughput = new ProvisionedThroughput() { ReadCapacityUnits = 1, WriteCapacityUnits = 1 };

            client.CreateTable(createTableRequest);
        }

        private static void CreateUserTable(IAmazonDynamoDB client)
        {
            var createTableRequest = new CreateTableRequest();
            createTableRequest.TableName = Table.Users;

            createTableRequest.KeySchema = new List<KeySchemaElement>
            {
                new KeySchemaElement
                {
                    AttributeName = "Username",
                    KeyType = KeyType.HASH
                }
            };

            createTableRequest.AttributeDefinitions = new List<AttributeDefinition>
            {
                new AttributeDefinition
                {
                    AttributeName = "Username",
                    AttributeType = ScalarAttributeType.S
                }
            };

            createTableRequest.ProvisionedThroughput = new ProvisionedThroughput() { ReadCapacityUnits = 1, WriteCapacityUnits = 1 };

            client.CreateTable(createTableRequest);
        }

    }
}