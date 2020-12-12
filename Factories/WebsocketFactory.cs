using System;
using Microsoft.Extensions.Hosting;
using Pulse.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Pulse.Factories
    {
    public interface IDataProviderFactory
    {
        WebsocketService Create();
    }

    public class WebsocketFactory : IDataProviderFactory
    {
        private readonly IServiceProvider _services;

        public WebsocketFactory(IServiceProvider services) 
        {
            _services = services;
        }

        public WebsocketService Create()
        {
            return _services.GetService<WebsocketService>();
        }
    }
}