using Shouldly;
using Xunit;

namespace Owin.Types.Tests
{
    public class OwinEnvironmentTests
    {
        [Fact]
        public void ItCanCreateEnvironmentForTestConvenience()
        {
            var env = OwinEnvironment.Create();
            env.Environment.ShouldNotBe(null);
        }
    }
}
