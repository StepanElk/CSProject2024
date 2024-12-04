using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;

namespace CSproject.Hubs
{
    public interface IChatClient
    {
        public Task RecieveMessage(string username, string message , string senderUuid);
    }

    public record UserConnection(string Username, string ChatRoom , string Uuid);
    public record Connection( string ConnectionId, string Uuid);


    public class ChatHub:Hub<IChatClient>
    {
        private static readonly Dictionary<string , UserConnection> _UserConnections = new();
        private static readonly Dictionary<string , string> _Connections = new();


        public void Connect(string uuid)
        {
            
            if (!_Connections.ContainsValue(Context.ConnectionId))
            {
                if (!_Connections.ContainsKey(uuid))
                {
                    _Connections.Add(Context.ConnectionId , uuid);
                }
                else
                {
                    SetConnection(uuid );
                }
            }
        }

        private async void SetConnection(string uuid)
        {
            var chatRoom = _UserConnections[uuid].ChatRoom;
            var oldConnection = _Connections[uuid];
            await Groups.RemoveFromGroupAsync(oldConnection, chatRoom);
            _Connections.Remove(uuid);

            _Connections.Add(uuid , Context.ConnectionId);
            await Groups.AddToGroupAsync(Context.ConnectionId, chatRoom);
        }
        
        private async void Disconnect(string uuid)
        {
            var chatRoom = _UserConnections[uuid].ChatRoom;
            _UserConnections.Remove(uuid);
            await Groups.RemoveFromGroupAsync(_Connections[uuid], chatRoom);
            _Connections.Remove(uuid);
        }

        public void LoginUser(string username, string chatRoom, string uuid)
        {
            _UserConnections.Add(uuid, new UserConnection(username, chatRoom, uuid));
        }

        public async Task JoinChat( string uuid)
        {
            var user = _UserConnections[uuid];
            await Groups.AddToGroupAsync(Context.ConnectionId, user.ChatRoom);
            
            await Clients
                .Group(user.ChatRoom)
                .RecieveMessage("Admin", $"{user.Username} присоединился к чату!" , "admin");
        }


        public async Task SendMessage(string message , string uuid)
        {
            UserConnection connection;

            if (_UserConnections.ContainsKey(uuid))
                connection = _UserConnections[uuid];
            else throw new ArgumentException($"{uuid} not in UserConnection dict! ");


            //Console.WriteLine(connection);


            if (connection is not null)
            {
                await Clients
                    .Group(connection.ChatRoom)
                    .RecieveMessage(connection.Username, message , uuid);
            }
            else throw new ArgumentException($"{Context.ConnectionId} not in active Connections dict!");
        }
    }
}

