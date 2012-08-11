using AlternateBuilder;
using Xunit;

namespace Owin.Tests
{
    public class InteropTests
    {
        [Fact] 
        public void InterfaceCompiledInShouldBeCastableToInterfaceFromAssembly()
        {
            var builder = new TestBuilder();
            dynamic startup = new InteropTestApp.Startup();
            startup.Configuration(builder);
        }
    }
}
