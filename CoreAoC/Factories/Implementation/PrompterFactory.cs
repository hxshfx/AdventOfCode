using CoreAoC.Engine;
using CoreAoC.Factories.Interfaces;
using CoreAoC.Interfaces;

namespace CoreAoC.Factories.Implementation
{
    public class PrompterFactory : IPrompterFactory
    {
        public IProblemManager Manager { get; }


        public PrompterFactory(IProblemManager manager)
            => Manager = manager;


        public IPrompter Create()
            => new Prompter(Manager);
    }
}
