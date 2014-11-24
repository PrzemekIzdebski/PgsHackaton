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


            if (!tables.TableNames.Contains(Table.Observing))
            {
                CreateObservingTable(client);
            }

            if (!tables.TableNames.Contains(Table.Tag))
            {
                CreateTagTable(client);
            }

            if (!tables.TableNames.Contains(Table.TagMessage))
            {
                CreateTagMessageTable(client);
            }
        }

        private static void CreateTagMessageTable(IAmazonDynamoDB client)
        {
            var createTableRequest = new CreateTableRequest();
            createTableRequest.TableName = Table.TagMessage;

            createTableRequest.KeySchema = new List<KeySchemaElement>
            {
                new KeySchemaElement
                {
                    AttributeName = "Tag",
                    KeyType = KeyType.HASH
                },
                new KeySchemaElement
                {
                    AttributeName = "MessageDigest",
                    KeyType = KeyType.RANGE
                }
            };

            createTableRequest.LocalSecondaryIndexes = new List<LocalSecondaryIndex>()
                {
                    new LocalSecondaryIndex()
                        {
                            IndexName = Table.TagMessageIndex,
                            KeySchema = new List<KeySchemaElement>
                            {
                                new KeySchemaElement
                                {
                                    AttributeName = "Tag",
                                    KeyType = KeyType.HASH
                                },
                                new KeySchemaElement
                                {
                                    AttributeName = "PostedOn",
                                    KeyType = KeyType.RANGE
                                }
                            },
                            Projection = new Projection()
                                {
                                    ProjectionType = ProjectionType.ALL
                                }

                        }
                };

            createTableRequest.AttributeDefinitions = new List<AttributeDefinition>
            {
                new AttributeDefinition
                {
                    AttributeName = "Tag",
                    AttributeType = ScalarAttributeType.S
                },
                new AttributeDefinition
                {
                    AttributeName = "MessageDigest",
                    AttributeType = ScalarAttributeType.S
                },
                new AttributeDefinition
                {
                    AttributeName = "PostedOn",
                    AttributeType = ScalarAttributeType.N
                }
            };

            createTableRequest.ProvisionedThroughput = new ProvisionedThroughput() { ReadCapacityUnits = 1, WriteCapacityUnits = 1 };

            client.CreateTable(createTableRequest);
        }

        private static void CreateTagTable(IAmazonDynamoDB client)
        {
            var createTableRequest = new CreateTableRequest();
            createTableRequest.TableName = Table.Tag;

            createTableRequest.KeySchema = new List<KeySchemaElement>
            {
                new KeySchemaElement
                {
                    AttributeName = "Name",
                    KeyType = KeyType.HASH
                }
            };

            createTableRequest.AttributeDefinitions = new List<AttributeDefinition>
            {
                new AttributeDefinition
                {
                    AttributeName = "Name",
                    AttributeType = ScalarAttributeType.S
                }
            };

            createTableRequest.ProvisionedThroughput = new ProvisionedThroughput() { ReadCapacityUnits = 1, WriteCapacityUnits = 1 };

            client.CreateTable(createTableRequest);
        }

        private static void CreateObservingTable(IAmazonDynamoDB client)
        {
            var createTableRequest = new CreateTableRequest();
            createTableRequest.TableName = Table.Observing;

            createTableRequest.KeySchema = new List<KeySchemaElement>
            {
                new KeySchemaElement
                {
                    AttributeName = "ObservingUser",
                    KeyType = KeyType.HASH
                },
                new KeySchemaElement()
                    {
                        AttributeName = "ObservedUser",
                        KeyType = KeyType.RANGE
                    }
            };

            createTableRequest.GlobalSecondaryIndexes = new List<GlobalSecondaryIndex>()
                {
                    new GlobalSecondaryIndex()
                        {
                            IndexName = Table.ObservedIndex,
                            KeySchema = new List<KeySchemaElement>
                            {
                                new KeySchemaElement
                                {
                                    AttributeName = "ObservedUser",
                                    KeyType = KeyType.HASH
                                }
                            },
                            ProvisionedThroughput = new ProvisionedThroughput() { ReadCapacityUnits = 1, WriteCapacityUnits = 1 },
                            Projection = new Projection()
                                {
                                    ProjectionType = ProjectionType.ALL
                                }
                        }
                };

            createTableRequest.AttributeDefinitions = new List<AttributeDefinition>
            {
                new AttributeDefinition
                {
                    AttributeName = "ObservingUser",
                    AttributeType = ScalarAttributeType.S
                },
                new AttributeDefinition
                {
                    AttributeName = "ObservedUser",
                    AttributeType = ScalarAttributeType.S
                }
            };

            createTableRequest.ProvisionedThroughput = new ProvisionedThroughput() { ReadCapacityUnits = 1, WriteCapacityUnits = 1 };

            client.CreateTable(createTableRequest);
        }

        private static void CreateMessageTable(IAmazonDynamoDB client)
        {
            var createTableRequest = new CreateTableRequest();
            createTableRequest.TableName = Table.Messages;

            createTableRequest.KeySchema = new List<KeySchemaElement>
            {
                new KeySchemaElement
                {
                    AttributeName = "Receiver",
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
                    AttributeName = "Receiver",
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