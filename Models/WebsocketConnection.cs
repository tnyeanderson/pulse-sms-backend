using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Pulse.Models
{
	public class WebsocketConnection
	{
		public WebSocket connection { get; set; }
        public TaskCompletionSource<object> socketFinishedTcs { get; set; }

		public WebsocketConnection(WebSocket webSocket, TaskCompletionSource<object> finished)
		{
			connection = webSocket;
			socketFinishedTcs = finished;
		}
	}
}
