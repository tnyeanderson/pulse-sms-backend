using Pulse.Factories;
using Pulse.Services;
using System;
using System.Diagnostics;  
using System.IO;  
using System.Net.WebSockets;
using System.Threading.Tasks;  
using Microsoft.AspNetCore.Builder;  
using Microsoft.AspNetCore.Http;  
using Microsoft.Extensions.DependencyInjection;
  
namespace Pulse.Middlewares  
{  
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project  
    public class WebsocketMiddleware  
    {  
        private readonly RequestDelegate _next;
        private WebsocketService _wsService;
  
        public WebsocketMiddleware(RequestDelegate next)
        {  
            _next = next;
        }
  
        public async Task Invoke(HttpContext context)
        {
            var wsFactory = context.RequestServices.GetService<WebsocketFactory>();
            var _wsService = context.RequestServices.GetService<WebsocketService>();

            if (context.Request.Path == "/api/v1/stream")
            {
			    if (context.WebSockets.IsWebSocketRequest)
			    {
			        using (WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync())
			        {
						var socketFinishedTcs = new TaskCompletionSource<object>();
        				//await _wsService.AddSocket(webSocket, socketFinishedTcs);
        				await socketFinishedTcs.Task;
			        }
			    }
			    else
			    {
			        context.Response.StatusCode = 400;
			    }
			}
			else
			{
			    await _next(context);
			}
        }  
    }  
  
    // Extension method used to add the middleware to the HTTP request pipeline.  
    public static class CustomMiddlewareExtensions  
    {  
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder)  
        {  
            return builder.UseMiddleware<WebsocketMiddleware>();  
        }  
    }  
}