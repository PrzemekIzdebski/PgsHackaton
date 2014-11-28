using System.Collections.Generic;
using PgsTwitter.Entities;

namespace PgsTwitter.Services
{
    public interface IMessageService
    {
        ICollection<Message> GetMessagesBy(string username);
        ICollection<Message> GetMessagesFor(string username);
        void PostMessage(string username, string text);
    }
}