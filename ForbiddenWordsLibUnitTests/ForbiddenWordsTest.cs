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
            var m = new MathUtils();
            Assert.Equal(2, m.GCD(2, 8));
        }
    }
}
