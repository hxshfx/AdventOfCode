using CoreAoC.Interfaces;

namespace CoreAoC.Factories.Interfaces
{
    public interface IDataReaderFactory
    {
        public IInputReader CreateInputReader();

        public IOutputReader CreateOutputReader();
    }
}
