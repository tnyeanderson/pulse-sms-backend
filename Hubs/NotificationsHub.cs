using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Pulse.Hubs
{
    public class NotificationsHub : Hub
    {
        private readonly IHubContext<NotificationsHub> _hubContext;

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async override Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("welcome");
        }
    }
}