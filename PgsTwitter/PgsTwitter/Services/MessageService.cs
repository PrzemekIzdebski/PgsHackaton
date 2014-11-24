using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using PgsTwitter.Common;
using PgsTwitter.Entities;

namespace PgsTwitter.Services
{
    public class MessageService : IMessageService
    {
        private readonly DynamoDBContext _context;


        public MessageService(DynamoDBContext context)
        {
            _context = context;
        }

        public ICollection<Tag> GetAllTags()
        {
            var tags = _context.Scan<Tag>();
            return tags.ToList();
        }

        public ICollection<Message> GetMessages(string username)
        {
            var msgs = _context.Query<Message>(username);
            return msgs.ToList();
        }

        public ICollection<Message> GetMessagesByTag(string tag)
        {
            var msgs = _context.Query<Message>("#" + tag);
            return msgs.ToList();
        }

        public void PostMessage(string username, string text)
        {
            var postedOn = DateTime.Now.Ticks;

            AddMessageToAuthor(username, text, postedOn);
            AddToObservingUsers(username, text, postedOn);
            AddToTags(username, text, postedOn);
        }

        private void AddToTags(string username, string text, long postedOn)
        {
            var tags = ParseTags(text);
            foreach (var tag in tags)
            {
                var msg = new Message
                    {
                        PostedOn = postedOn,
                        Author = username,
                        Text = text,
                        Receiver = tag
                    };
                _context.Save(msg);
            }
        }

        private void AddToObservingUsers(string username, string text, long postedOn)
        {
            var observingUsers = GetObserving(username);
            foreach (var observingUser in observingUsers)
            {
                var msg = new Message
                    {
                        PostedOn = postedOn,
                        Author = username,
                        Text = text,
                        Receiver = observingUser
                    };
                _context.Save(msg);
            }
        }

        private void AddMessageToAuthor(string username, string text, long postedOn)
        {
            var msg = new Message
                {
                    PostedOn = postedOn,
                    Author = username,
                    Text = text,
                    Receiver = username
                };
            _context.Save(msg);
        }

        private List<string> GetObserving(string userName)
        {
            var queryResult = _context.Query<Observing>(userName, new DynamoDBOperationConfig()
            {
                IndexName = PgsTwitter.Entities.Table.ObservedIndex
            });
            return queryResult.Select(observing => observing.ObservingUser).ToList();
        }


        private List<string> ParseTags(string text)
        {
            var result = new List<string>();
            var matches = System.Text.RegularExpressions.Regex.Matches(text, "#[a-zA-Z0-9]*");
            foreach (Match match in matches)
            {
                result.Add(match.Value);
            }
            return result;
        }
    }
}