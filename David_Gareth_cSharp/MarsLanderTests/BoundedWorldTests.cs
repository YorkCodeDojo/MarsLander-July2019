using MarsLander;
using Xunit;

namespace MarsLanderTests
{
    public class BoundedWorldTests
    {

        [Theory]
        [InlineData(-1, -1, false)]
        [InlineData(-1, 2, false)]
        [InlineData(2, -1, false)]
        [InlineData(5, 0, false)]
        [InlineData(0, 0, true)]
        [InlineData(4, 4, true)]
        [InlineData(2, 3, true)]
        [InlineData(0, 3, true)]
        public void IsWithinBoundary(int x, int y, bool expectedResult)
        {
            var world = new BoundedWorld(5, 5);
            var actualResult = world.IsWithinBoundary(new Coordinate(x, y));

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
