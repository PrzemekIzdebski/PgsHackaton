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
            var msgs = _context.Query<TagMessage>(tag);
            return msgs.Select(msg => msg.AsMessage()).ToList();
        }

        public void PostMessage(string username, string text)
        {
            var msg = new Message
            {
                PostedOn = DateTime.Now.Ticks,
                Text = text,
                Receiver = username
            };
            _context.Save(msg);
            var tags = ParseTags(text);
            foreach (var tag in tags)
            {
                AddToTag(tag, msg);
            }
        }

        private void AddToTag(string tagName, Message message)
        {
            var tag = _context.Load<Tag>(tagName);
            if (tag == null)
            {
                tag = new Tag()
                    {
                        Name = tagName,
                        Count = 0
                    };
            }
            tag.Count += 1;
            _context.Save(tag);
            var tagMessage = new TagMessage()
                {
                    Tag = tagName,
                    Username = message.Receiver,
                    PostedOn = message.PostedOn,
                    Text = message.Text,
                    MessageDigest = message.Digest
                };
            _context.Save(tagMessage);
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