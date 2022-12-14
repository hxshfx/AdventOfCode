using CoreAoC.Factories.Implementation;
using CoreAoC.Factories.Interfaces;
using TestingProject.Utils;
using Xunit;

namespace TestingProject.Tests.CoreTesters
{
    public class PrompterTests : CoreTesters<IPrompterFactory>
    {
        public PrompterTests()
            : base(new AppHostFactory()) { }


        [Fact]
        public void TestCreatePrompter()
            => Assert.NotNull(_service.Create());


        [Fact]
        public void TestVisualizerThread()
            => Assert.IsAssignableFrom<TypeInitializationException>(Record.Exception(() => _service.Create().VisualizerThread()));
    }
}
