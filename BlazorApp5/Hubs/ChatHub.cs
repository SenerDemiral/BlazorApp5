using Microsoft.AspNetCore.SignalR;

namespace BlazorApp5.Hubs
{
    public class ChatHub : Hub
    {
        private readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();

        // Clients can call methods that are defined as public
        public async Task<string> Deneme(string user, string message)
        {
            var aaa = user + message;
            return aaa;
            //await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", "SERVER", "PONG");
            //return Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", "SERVER", "PONG");
            //return Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", "SERVER", "PONG");
            //return Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", "SERVER", "PONG");
            //return Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task<IEnumerable<string>> IamHere(string usrName)
        {
            //_connections.Add(usrName, Context.ConnectionId);
            _connections.Add(Context.ConnectionId, Context.ConnectionId);
            var list = _connections.GetUsers();
            return list;
        }

        public async Task InitializeUserList(string usrName)
        {
            //_connections.Add(usrName, Context.ConnectionId);
            _connections.Add(Context.ConnectionId, Context.ConnectionId);
            var list = _connections.GetUsers();

            await Clients.All.SendAsync("ReceiveInitializeUserList", list);
        }
        public async Task Broadcast(string username, string message)
        {
            await Clients.All.SendAsync("Broadcast", username, message);
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"ChatHub {Context.ConnectionId} connected");
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception e)
        {
            _connections.Remove(Context.ConnectionId, Context.ConnectionId);

            var list = _connections.GetUsers();

            await Clients.All.SendAsync("ReceiveInitializeUserList", list);
            Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
            await base.OnDisconnectedAsync(e);
        }
    }
}
