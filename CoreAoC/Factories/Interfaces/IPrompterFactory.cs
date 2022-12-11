using CoreAoC.Interfaces;

namespace CoreAoC.Factories.Interfaces
{
    public interface IPrompterFactory
    {
        internal IProblemManager Manager { get; }


        public IPrompter Create();
    }
}
