using CSproject.Domain;
using CSproject.Infrastructure;
using Microsoft.AspNetCore.SignalR;

namespace CSproject.Application.Hubs
{
    public interface IChatClient
    {
        public Task RecieveMessage(string json , string uuid);
        public Task RecieveServerAnswer( int answer );
        public Task RecieveChatMessages(string json);
    }

    public class ChatHub : Hub<IChatClient>
    {
        private readonly UserRepository _userRepository;
        private readonly ConnectionsRepository _connectionsRepository;
        private readonly MessagesRepository _messagesRepository;

        public ChatHub(
            UserRepository userRepository ,
            ConnectionsRepository connectionsRepository , 
            MessagesRepository messagesRepository
            )
        {
            _userRepository = userRepository;
            _connectionsRepository = connectionsRepository;
            _messagesRepository = messagesRepository;
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

        public async Task JoinChat(string uuid)
        {
            var connection = _connectionsRepository.GetConnectionByUuid(uuid);
            await Groups.AddToGroupAsync(Context.ConnectionId, connection.Chatroom);
        }


        public async Task SendMessage(string content, string uuid)
        {
            Connection connection;

            if (_connectionsRepository.IsConnected(uuid))
                connection = _connectionsRepository.GetConnectionByUuid(uuid);
            else throw new ArgumentException($"{uuid} not in connections repository!");


            if (connection is not null)
            {
                var message = _messagesRepository.RegisterSendedMessage(content, uuid);
                await Clients
                    .Group(connection.Chatroom)
                    .RecieveMessage(message , uuid);
            }
            else throw new ArgumentException($"{Context.ConnectionId} not active Connection!");
        }

        public async void LoadMessages(string uuid)
        {
            var login = _connectionsRepository.GetUserByUuid(uuid).Login;
            var json = _messagesRepository.GetMessages(login);
            await Clients
                .Client(Context.ConnectionId)
                .RecieveChatMessages(json);
        }

        public async Task TryAuthorise(string login, string password , string uuid )
        {
            var answer = _userRepository.CheckUser(login, password);
            if (answer == TryLoginAnswers.Ok)
                 _connectionsRepository.LoginUser(login, uuid);

            await Clients
                .Client(Context.ConnectionId)
                .RecieveServerAnswer((int)answer);
        }

        public async Task TrySignUp(
            string login , 
            string uuid,
            string name ,
            string codeword ,
            string sex , 
            string password ,
            string photo){

            var  answer = TrySignUpAnswers.Ok;
            if (_userRepository.IsUserExist(login))
                answer = TrySignUpAnswers.WrongLogin;
            else if(! _userRepository.IsUserExist(codeword))
                answer = TrySignUpAnswers.WrongCodeword;

            if (answer == TrySignUpAnswers.Ok)
            {
                _userRepository.SignUp(
                    new User { Name = name , Login = login , Sex = sex , Password = password ,Photo = photo});
                _connectionsRepository.LoginUser(login, uuid);

                var connection = _connectionsRepository.GetConnectionByUuid(uuid);
                var message = _messagesRepository
                    .RegisterSendedMessage($"{connection.User.Name} присоединился к чату!", uuid, "admin");

                await Clients
                .Group(connection.Chatroom)
                    .RecieveMessage(message, uuid);
            }

            await Clients
                .Client(Context.ConnectionId)
                .RecieveServerAnswer((int)answer);
        }

        public async Task PostEvent(
            string uuid ,
            string name , 
            string description ,
            string location,
            string date ,
            string photo)
        {
            var covertedDate = new DateOnly(
                int.Parse( date.Split('-')[0]),
                int.Parse(date.Split('-')[1]),
                int.Parse(date.Split('-')[2]));

            var connection = _connectionsRepository.GetConnectionByUuid(uuid);
            var message = _messagesRepository.RegisterNewEvent(uuid,name,description,location, covertedDate, photo);
            await Clients
                .Group(connection.Chatroom)
                .RecieveMessage(message, uuid);
        }

        public async Task PostTrain(
            string uuid ,
            string name,
            string description,
            int duration,
            int calories,
            string photo)
        {
            var connection = _connectionsRepository.GetConnectionByUuid(uuid);
            var message = _messagesRepository.RegisterNewTrain(uuid, name,description, duration,calories,photo);
            await Clients
                .Group(connection.Chatroom)
                .RecieveMessage(message, uuid);
        }
    }
}

