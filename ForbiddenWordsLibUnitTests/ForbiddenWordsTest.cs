using System;
using Xunit;
using ForbiddenWordsLib;

namespace ForbiddenWordsLibUnitTests
{
    public class ForbiddenWordsTest
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal(2, MathUtils.GCD(2, 8));
        }
    }
}
