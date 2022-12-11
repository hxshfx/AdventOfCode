using CoreAoC.Factories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
