using Shouldly;
using Xunit;

namespace Owin.Types.Tests
{
    public class OwinHeadersTests
    {
        [Fact]
        public void MissingHeaderReturnsNull()
        {
            var req = OwinRequest.Create();
            req.GetHeader("missing").ShouldBe(null);
            req.GetHeaderUnmodified("missing").ShouldBe(null);
            req.GetHeaderSplit("missing").ShouldBe(null);
        }

        [Fact]
        public void SingleHeaderReturnedAsSingle()
        {
            var req = OwinRequest.Create();
            req.Headers["x-custom"] = new[] { "one" };
            req.GetHeader("x-custom").ShouldBe("one");
            req.GetHeaderSplit("x-custom").ShouldBe(new[] { "one" });
            req.GetHeaderUnmodified("x-custom").ShouldBe(new[] { "one" });
        }

        [Fact]
        public void MultipleHeaderJoinedByGetHeader()
        {
            var req = OwinRequest.Create();
            req.Headers["x-custom"] = new[] { "one", "two" };
            req.GetHeader("x-custom").ShouldBe("one,two");
            req.GetHeaderSplit("x-custom").ShouldBe(new[] { "one", "two" });
            req.GetHeaderUnmodified("x-custom").ShouldBe(new[] { "one", "two" });
        }

        [Fact]
        public void DelimitedHeaderSplitByGetHeaderSplit()
        {
            var req = OwinRequest.Create();
            req.Headers["x-custom"] = new[] { "one,two" };
            req.GetHeader("x-custom").ShouldBe("one,two");
            req.GetHeaderSplit("x-custom").ShouldBe(new[] { "one", "two" });
            req.GetHeaderUnmodified("x-custom").ShouldBe(new[] { "one,two" });
        }

        [Fact]
        public void ComplexHeaderReturnedIntactByGetHeaderUnmodified()
        {
            var req = OwinRequest.Create();
            req.Headers["x-custom"] = new[] { "one,two", "three" };
            req.GetHeader("x-custom").ShouldBe("one,two,three");
            req.GetHeaderSplit("x-custom").ShouldBe(new[] { "one", "two", "three" });
            req.GetHeaderUnmodified("x-custom").ShouldBe(new[] { "one,two", "three" });
        }

        [Fact]
        public void SetHeaderAssignsSingleValue()
        {
            var req = OwinRequest.Create();
            req.SetHeader("x-custom", "one");
            req.Headers["x-custom"].ShouldBe(new[] { "one" });
            req.SetHeader("x-custom", "two,three");
            req.Headers["x-custom"].ShouldBe(new[] { "two,three" });
        }

        [Fact]
        public void SetHeaderJoinedAssignsSingleJoinedValue()
        {
            var req = OwinRequest.Create();
            req.SetHeaderJoined("x-custom", "one");
            req.Headers["x-custom"].ShouldBe(new[] { "one" });
            req.SetHeaderJoined("x-custom", "two", "three");
            req.Headers["x-custom"].ShouldBe(new[] { "two,three" });
            req.SetHeaderJoined("x-custom", "four,five", "six");
            req.Headers["x-custom"].ShouldBe(new[] { "four,five,six" });
        }

        [Fact]
        public void SetHeaderUnmodifiedAssignsArrayAsProvided()
        {
            var req = OwinRequest.Create();
            req.SetHeaderUnmodified("x-custom", "one");
            req.Headers["x-custom"].ShouldBe(new[] { "one" });
            req.SetHeaderUnmodified("x-custom", "two", "three");
            req.Headers["x-custom"].ShouldBe(new[] { "two", "three" });
            req.SetHeaderUnmodified("x-custom", "four,five", "six");
            req.Headers["x-custom"].ShouldBe(new[] { "four,five", "six" });
        }

        [Fact]
        public void AddHeaderAppendedAsAdditionalArrayItem()
        {
            var req = OwinRequest.Create();
            req.AddHeader("x-custom", "one");
            req.Headers["x-custom"].ShouldBe(new[] { "one" });
            req.AddHeader("x-custom", "two,three");
            req.Headers["x-custom"].ShouldBe(new[] { "one", "two,three" });
        }

        [Fact]
        public void AddHeaderJoinedAppendedToSingleArrayItem()
        {
            var req = OwinRequest.Create();
            req.AddHeaderJoined("x-custom", "one");
            req.Headers["x-custom"].ShouldBe(new[] { "one" });
            req.AddHeaderJoined("x-custom", "two,three");
            req.Headers["x-custom"].ShouldBe(new[] { "one,two,three" });
            req.AddHeaderJoined("x-custom", "four,five", "six");
            req.Headers["x-custom"].ShouldBe(new[] { "one,two,three,four,five,six" });
        }

        [Fact]
        public void AddHeaderJoinedCollapsesExistingArray()
        {
            var req = OwinRequest.Create();
            req.SetHeaderUnmodified("x-custom", "one", "two,three");
            req.Headers["x-custom"].ShouldBe(new[] { "one", "two,three" });
            req.AddHeaderJoined("x-custom", "four,five", "six");
            req.Headers["x-custom"].ShouldBe(new[] { "one,two,three,four,five,six" });
        }

        [Fact]
        public void AddHeaderUnmodifiedAppendedAsGiven()
        {
            var req = OwinRequest.Create();
            req.AddHeaderUnmodified("x-custom", "one");
            req.Headers["x-custom"].ShouldBe(new[] { "one" });
            req.AddHeaderUnmodified("x-custom", "two,three");
            req.Headers["x-custom"].ShouldBe(new[] { "one", "two,three" });
            req.AddHeaderUnmodified("x-custom", "four,five", "six");
            req.Headers["x-custom"].ShouldBe(new[] { "one", "two,three", "four,five", "six" });
        }
    }
}
