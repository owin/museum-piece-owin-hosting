using System;
using Owin.Types.Helpers;
using Shouldly;
using Xunit;

namespace Owin.Types.Tests
{
    public class StringSegmentTests
    {
        [Fact]
        public void EqualMatchesEntireSubstring()
        {
            var segment = new StringSegment("abcdefghij", 2, 6);
            segment.Equals("cdefgh", StringComparison.Ordinal).ShouldBe(true);
            segment.Equals("cdefg", StringComparison.Ordinal).ShouldBe(false);
            segment.Equals("cdefghi", StringComparison.Ordinal).ShouldBe(false);
            segment.Equals("cDefgh", StringComparison.Ordinal).ShouldBe(false);
            segment.Equals("cDefgh", StringComparison.OrdinalIgnoreCase).ShouldBe(true);
        }
    }
}
