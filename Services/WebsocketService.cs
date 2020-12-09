using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Pulse.Hubs;
using System.Threading.Tasks;
using System.Threading;


namespace Pulse.Services
{
    public class WebsocketService : Microsoft.Extensions.Hosting.BackgroundService
    {
        private readonly IHubContext<NotificationsHub> _hub;
        private static readonly System.Timers.Timer _timer = new System.Timers.Timer();
        private readonly ILogger<WebsocketService> _logger;

		public WebsocketService(IHubContext<NotificationsHub> hub, ILogger<WebsocketService> logger)
		{
			_logger = logger;
            _hub = hub;
		}

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)  
        {  
            while(!stoppingToken.IsCancellationRequested)  
            {
                SendPing();
                await Task.Delay(1000 * 3, stoppingToken);
            }  
        }

        public void SendPing()
        {
            _hub.Clients.All.SendAsync("welcome");
        }
    }
    

}