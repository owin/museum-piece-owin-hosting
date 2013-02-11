// <copyright file="StringSegmentTests.cs" company="Microsoft Open Technologies, Inc.">
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
