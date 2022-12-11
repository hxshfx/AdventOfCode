using CoreAoC.Factories.Implementation;
using CoreAoC.Factories.Interfaces;
using CoreAoC.Interfaces;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

IAppHostFactory appHostFactory = new AppHostFactory();
IAppHost appHost = appHostFactory.Create();

await appHost.StartApplication();
