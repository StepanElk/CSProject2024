using CSproject.Domain;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using System;

namespace CSproject.Infrastructure
{
    public class MessagesRepository
    {

        private readonly EFContext _db;

        public MessagesRepository(EFContext db)
        {
            _db = db;
        }

        public string RegisterSendedMessage(string content , string uuid , string login = null)
        {
            var user = login is null
                ? _db.Connections.Include(x => x.User).FirstOrDefault(x => x.Uuid == uuid).User
                : _db.Users.FirstOrDefault(x => x.Login == login);

            var message = new Message
            {
                Content = content,
                SendDate = DateTime.Now,
                User =user,
                Status = true,
                Type = "text"
            };
            _db.Messages.Add(message);

            _db.SaveChanges();
            message.IsMine = login is null;
            message.UserName = user.Name;
            return JsonSerializer.Serialize(message); 
        }

        public string GetMessages(string login)
        {
            var messages = _db.Messages.Include(x => x.User).ToList();
            messages.ForEach(x => {x.IsMine = x.User.Login == login; x.UserName = x.User.Name; }) ;
            return JsonSerializer.Serialize(messages);
            
        }
    }
}
