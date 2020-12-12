using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pulse.Helpers;
using Pulse.Hubs;
using Pulse.Middlewares;
using Pulse.Services;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Pulse
{
	public static class WebsocketRouteBuilder
	{
	    public static IEndpointConventionBuilder MapWebsockets(this IEndpointRouteBuilder endpoints, string pattern)
	    {
	        var pipeline = endpoints.CreateApplicationBuilder()
	            .UseMiddleware<WebsocketMiddleware>()
	            .Build();

	        return endpoints.Map(pattern, pipeline).WithDisplayName("Websocket stream");
	    }
	}

	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddResponseCompression();
			services.AddControllers();
			services.AddSignalR(hubOptions =>
			{
				hubOptions.KeepAliveInterval = TimeSpan.FromSeconds(3);
			});
			//services.AddHostedService<WebsocketService>();

			// Skip Firebase for now as it is not set up
			// FirebaseHelper.Init(Environment.GetEnvironmentVariable("FIREBASE_SERVER_KEY"));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				
				app.UseCors(builder => builder.WithOrigins("*")
                    					.AllowAnyMethod()
										.AllowAnyHeader());
				app.UseDeveloperExceptionPage();
				app.Use(async (context, next) =>
				{
					Console.WriteLine(context.Request.Path);
					await next.Invoke();
				});
			}

			app.UseWebSockets();
			//app.UseMiddleware<WebSocketMiddleware>();

			app.UseResponseCompression();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				//endpoints.Map("/api/v1/stream", endpoints.CreateApplicationBuilder()
	            //	.UseMiddleware<WebsocketMiddleware>()
	            //	.Build());

				endpoints.MapControllers();
				//endpoints.MapHub<NotificationsHub>("/api/v1/stream");
			});
		}
	}
}
