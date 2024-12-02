using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;

namespace CSproject.Hubs
{
    public interface IChatClient
    {
        public Task RecieveMessage(string username, string message);
    }

    public record UserConnection(string username, string chatRoom);

    public class ChatHub:Hub<IChatClient>
    {
        private static MemoryCache _connections = new(new MemoryCacheOptions());


        public async Task JoinChat(string username , string chatRoom)
        {
            _connections.Set(Context.ConnectionId, new UserConnection(username, chatRoom));
            //Console.WriteLine(Context.ConnectionId);
            await Groups.AddToGroupAsync(Context.ConnectionId, chatRoom);
            await Clients
                .Group(chatRoom)
                .RecieveMessage("Admin", $"{username} присоединился к чату!");
        }


        public async Task SendMessage(string message)
        {
            UserConnection connection = (UserConnection)_connections.Get(Context.ConnectionId);
            //Console.WriteLine(Context.ConnectionId.ToString());
            Console.WriteLine(connection);
            //Console.WriteLine(_connections.Count);

            if (connection is not null)
            {
                await Clients
                    .Group(connection.chatRoom)
                    .RecieveMessage(connection.username,message);
            }
        }
    }
}

