// <copyright file="OwinHeadersTests.cs" company="Microsoft Open Technologies, Inc.">
// Copyright 2013 Microsoft Open Technologies, Inc. All rights reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

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

        // [Fact] //TODO: don't split quoted commas
        public void QuotedCommaShouldNotBeSplit()
        {
            var req = OwinRequest.Create();
            req.SetHeaderUnmodified("x-custom", "\"one,two\"");
            req.GetHeaderSplit("x-custom").ShouldBe(new[] { "\"one,two\"" });
            req.AddHeaderUnmodified("x-custom", "\"three,four\"");
            req.GetHeaderUnmodified("x-custom").ShouldBe(new[] { "\"one,two\"", "\"three,four\"" });
            req.GetHeaderSplit("x-custom").ShouldBe(new[] { "\"one,two\"", "\"three,four\"" });
            req.AddHeaderJoined("x-custom", "\"five,six\"");
            req.GetHeaderUnmodified("x-custom").ShouldBe(new[] { "\"one,two\",\"three,four\",\"five,six\"" });
            req.GetHeaderSplit("x-custom").ShouldBe(new[] { "\"one,two\"", "\"three,four\"", "\"five,six\"" });
        }
    }
}
