using CoreAoC.Engine;
using CoreAoC.Factories.Interfaces;
using CoreAoC.Interfaces;

namespace CoreAoC.Factories.Implementation
{
    public class DataReaderFactory : IDataReaderFactory
    {
        public IInputReader CreateInputReader()
            => new DataReader();

        public IOutputReader CreateOutputReader()
            => new DataReader();
    }
}
