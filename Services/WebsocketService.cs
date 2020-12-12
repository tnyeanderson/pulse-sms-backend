using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Pulse.Hubs;
using Pulse.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace Pulse.Services
{
    public class WebsocketService : Microsoft.Extensions.Hosting.BackgroundService
    {
        private static IHubContext<NotificationsHub> _hub;
        private static readonly System.Timers.Timer _timer = new System.Timers.Timer();
        private readonly ILogger<WebsocketService> _logger;
        private List<WebsocketConnection> _connections;

		public WebsocketService(IServiceProvider services, IHubContext<NotificationsHub> hub, ILogger<WebsocketService> logger)
		{
			_logger = logger;
            _hub = hub;
		}

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)  
        {  
            // Wait for everything to get up and running
            await Task.Delay(1000 * 6, stoppingToken);

            while(!stoppingToken.IsCancellationRequested)  
            {
                SendPing();
                await Task.Delay(1000 * 3, stoppingToken);
            }  
        }

        public async void AddSocket(WebSocket connection, TaskCompletionSource<object> socketFinishedTcs)
        {
            var wsConnection = new WebsocketConnection(connection, socketFinishedTcs);
            _connections.Add(wsConnection);
            await KeepOpen(connection);
        }

        public static async Task KeepOpen(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                //await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }

        public static async Task Echo(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }

        public void SendPing()
        {
            _connections.ForEach(delegate(WebsocketConnection ws) {
                string pingText = "Hello Test";
                byte[] pingBytes = Encoding.UTF8.GetBytes(pingText);
                ws.connection.SendAsync(new ArraySegment<Byte>(pingBytes), WebSocketMessageType.Text, true, CancellationToken.None);
            });
        }
    }
    

}