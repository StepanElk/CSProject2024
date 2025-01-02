using CSproject.Domain;
using CSproject.Infrastructure;
using Microsoft.AspNetCore.SignalR;

namespace CSproject.Application.Hubs
{
    public interface IChatClient
    {
        public Task RecieveMessage(string username, string message, string senderUuid);
        public Task RecieveServerAnswer( int answer );
    }

    public class ChatHub : Hub<IChatClient>
    {
        private readonly UserRepository _userRepository;
        private readonly ConnectionsRepository _connectionsRepository;


        public ChatHub(UserRepository userRepository , ConnectionsRepository connectionsRepository)
        {
            _userRepository = userRepository;
            _connectionsRepository = connectionsRepository;
        }

        public void Connect(string uuid)
        {
            if (!_connectionsRepository.IsConnected(uuid))
                _connectionsRepository.Connect(uuid, Context.ConnectionId);
            else
                SetConnection(uuid);
               
        }

        private async void SetConnection(string uuid)
        {
            var oldConnection = _connectionsRepository.SetConnection(uuid, Context.ConnectionId);

            await Groups.RemoveFromGroupAsync(oldConnection.ContextId, oldConnection.Chatroom);

            await Groups.AddToGroupAsync(Context.ConnectionId, oldConnection.Chatroom);
        }

        //public async void Disconnect(string uuid)
        //{
        //    var chatRoom = _UserConnections[uuid].ChatRoom;
        //    _UserConnections.Remove(uuid);
        //    await Groups.RemoveFromGroupAsync(_Connections[uuid], chatRoom);
        //    _Connections.Remove(uuid);
        //}

        public async Task JoinChat(string uuid)
        {
            var connection = _connectionsRepository.GetConnectionByUuid(uuid);
            await Groups.AddToGroupAsync(Context.ConnectionId, connection.Chatroom);

            await Clients
                .Group(connection.Chatroom)
                .RecieveMessage("Admin", $"{connection.User.Name} присоединился к чату!", "admin");
        }


        public async Task SendMessage(string message, string uuid)
        {
            Connection connection;

            if (_connectionsRepository.IsConnected(uuid))
                connection = _connectionsRepository.GetConnectionByUuid(uuid);
            else throw new ArgumentException($"{uuid} not in connections repository! ");


            if (connection is not null)
            {
                await Clients
                    .Group(connection.Chatroom)
                    .RecieveMessage(connection.User.Name, message, uuid);
            }
            else throw new ArgumentException($"{Context.ConnectionId} not active Connection!");
        }

        public async Task TryAuthorise(string login, string password , string uuid )
        {
            //if (!_Connections.ContainsValue(Context.ConnectionId))
            //    throw new Exception($"Unknown connection with id {Context.ConnectionId}!");

            var answer = _userRepository.CheckUser(login, password);
            if (answer == CheckUserAnswers.Ok)
                 _connectionsRepository.LoginUser(login, uuid);

            await Clients
                .Client(Context.ConnectionId)
                .RecieveServerAnswer((int)answer);
        }

        //public async Task TrySignUp() { }
    }
}

