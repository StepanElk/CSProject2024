using CSproject.Domain;
using Microsoft.EntityFrameworkCore;

namespace CSproject.Infrastructure
{
    public class ConnectionsRepository
    {

        private readonly EFContext _db;

        public ConnectionsRepository(EFContext db)
        {
            _db = db;
        }

        public bool IsConnected(string uuid)
        {
            return _db.Connections.FirstOrDefault(x => x.Uuid == uuid) is not null;
        }

        public void Connect(string uuid , string context)
        {
            _db.Connections.Add(
                    new Connection
                    {
                        Uuid = uuid,
                        Chatroom = "baseChatroom",
                        ContextId = context
                    });
            _db.SaveChanges();

        }
        public Connection SetConnection(string uuid , string context)
        {
            var connection = _db.Connections.Include(x => x.User).FirstOrDefault(x => x.Uuid == uuid);
            _db.Connections.Remove(connection);

            _db.Connections.Add(new Connection
            {
                Uuid = uuid,
                Chatroom = "baseChatroom",
                ContextId = context,
                User = connection.User
            });

            _db.SaveChanges();
            return connection;
        }

        public User GetUserByUuid(string uuid)
        {
            return _db.Connections.Include(x => x.User).FirstOrDefault(x => x.Uuid == uuid).User;
        }
        public Connection GetConnectionByUuid(string uuid)
        {
            return _db.Connections.Include(x => x.User).FirstOrDefault(x => x.Uuid == uuid);
        }

        public void LoginUser(string login ,string uuid)
        {
            var connection = _db.Connections.FirstOrDefault(x => x.Uuid == uuid);
            connection.User = _db.Users.FirstOrDefault(x => x.Login == login);
            _db.SaveChanges();
        }
    }
}
