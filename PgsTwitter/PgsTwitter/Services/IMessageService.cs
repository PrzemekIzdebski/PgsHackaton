using System.Collections.Generic;
using PgsTwitter.Entities;

namespace PgsTwitter.Services
{
    public interface IMessageService
    {
        ICollection<Message> GetMessages(string username);
        void PostMessage(string username, string text);
    }
}