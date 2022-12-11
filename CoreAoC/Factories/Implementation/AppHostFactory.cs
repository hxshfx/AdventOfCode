using CoreAoC.Factories.Interfaces;
using CoreAoC.Hosting;
using CoreAoC.Interfaces;

namespace CoreAoC.Factories.Implementation
{
    public class AppHostFactory : IAppHostFactory
    {
        public IAppHost Create()
            => new AppHost();
    }
}
