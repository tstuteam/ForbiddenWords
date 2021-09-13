using ForbiddenWordsLib;
using Xunit;

namespace ForbiddenWordsLibUnitTests
{
    public class ForbiddenWordsTest
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal(2, MathUtils.Gcd(2, 8));
        }
    }
}
