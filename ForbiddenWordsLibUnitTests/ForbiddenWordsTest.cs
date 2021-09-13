using ForbiddenWordsLib;
using Xunit;

namespace ForbiddenWordsLibUnitTests
{
    public class ForbiddenWordsTest
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal(6, MathUtils.Lcm(2, 3));
        }
    }
}
