using CoreAoC.Factories.Interfaces;
using CoreAoC.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoreAoC.Hosting
{
    internal class AppHost : IAppHost
    {
        public IServiceProvider ServiceProvider 
            => _host.Services;


        private readonly IHost _host;


        public AppHost()
            => _host = HostBuilder.Build();


        public async Task StartApplication()
        {
            IPrompterFactory prompterFactory = _host.Services.GetRequiredService<IPrompterFactory>();
            await Task.Factory.StartNew(() => prompterFactory.Create().VisualizerThread());
        }
    }
}
