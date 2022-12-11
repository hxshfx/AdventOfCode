namespace CoreAoC.Interfaces
{
    public interface IAppHost
    {
        public IServiceProvider ServiceProvider { get; }


        public Task StartApplication();
    }
}
