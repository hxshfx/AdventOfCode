using CoreAoC.Factories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace TestingProject.Utils
{
    public abstract class CoreTesters<TService>
        where TService : notnull
    {
        protected readonly TService _service;


        public CoreTesters(IAppHostFactory appHostFactory)
            => _service = appHostFactory.Create().ServiceProvider.GetRequiredService<TService>();
    }
}
