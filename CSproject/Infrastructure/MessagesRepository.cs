using CSproject.Domain;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Encodings.Web;

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
            message.UserPhoto = user.Photo;
            return JsonSerializer.Serialize(message); 
        }
        
        public string GetMessages(string login)
        {
            var messages = _db.Messages.Include(x => x.User).ToList();

            messages.ForEach(x => {
                x.IsMine = x.User.Login == login;
                x.UserName = x.User.Name;
                x.UserPhoto = x.User.Photo; 
            }) ;

            _db.SaveChanges();
            List<object> objects = _db.Messages.Include(x => x.User).Cast<object>().ToList();
            return  JsonSerializer.Serialize<object>(objects);
        }

        public string RegisterNewEvent(
            string uuid,
            string name,
            string description,
            string location,
            DateOnly date,
            string photo)
        {
            var user = _db.Connections.Include(x => x.User).FirstOrDefault(x => x.Uuid == uuid).User;
            var message = new EventMessage
            {
                EventName = name,
                EventDescription = description,
                Location = location,
                Photo = photo,
                EventDate = date,
                SendDate = DateTime.Now,
                User = user,
                Status = true,
                Type = "event"
            };
            _db.EventMessages.Add(message);

            _db.SaveChanges();
            message.UserName = user.Name;
            message.UserPhoto = user.Photo;
            return JsonSerializer.Serialize(message);
        }
    }
}
