using System;
using MarsLander;
using Xunit;

namespace MarsLanderTests
{
    public class RoverTests
    {
        [Fact]
        public void TheRoverStartsAt0_0()
        {
            var rover = new Rover();

            Assert.Equal(0, rover.Location.X);
            Assert.Equal(0, rover.Location.Y);
        }

        [Fact]
        public void TheRoverStartsFacingNorth()
        {
            var rover = new Rover();

            Assert.Equal(Direction.North, rover.Direction);
        }

        [Theory]
        [InlineData(Direction.North, Direction.West)]
        [InlineData(Direction.West, Direction.South)]
        [InlineData(Direction.South, Direction.East)]
        [InlineData(Direction.East, Direction.North)]
        public void TheRoverCanTurnLeft(Direction startDirection, Direction expectedDirection)
        {
            var rover = new Rover(startDirection);
            rover.TurnLeft();

            Assert.Equal(expectedDirection, rover.Direction);
        }

        [Theory]
        [InlineData(Direction.North, Direction.East)]
        [InlineData(Direction.East, Direction.South)]
        [InlineData(Direction.South, Direction.West)]
        [InlineData(Direction.West, Direction.North)]
        public void TheRoverCanTurnRight(Direction startDirection, Direction expectedDirection)
        {
            var rover = new Rover(startDirection);
            rover.TurnRight();

            Assert.Equal(expectedDirection, rover.Direction);
        }

        //[Theory]
        //[InlineData(Direction.North, 0, 1)]
        //[InlineData(Direction.East, 1, 0)]
        //[InlineData(Direction.South, 0, -1)]
        //[InlineData(Direction.West, -1, 0)]
        //public void TheRoverCanMove(Direction startDirection, int expectedX, int expectedY)
        //{
        //    var rover = new Rover(startDirection);
        //    var roverMoved = rover.TryMove((x) => true);

        //    Assert.True(roverMoved);
        //    Assert.Equal(expectedX, rover.Location.X);
        //    Assert.Equal(expectedY, rover.Location.Y);
        //}

        [Theory]
        [InlineData(Direction.North, 0, 1)]
        [InlineData(Direction.East, 1, 0)]
        [InlineData(Direction.South, 0, -1)]
        [InlineData(Direction.West, -1, 0)]
        public void TheRoverCanMove(Direction startDirection, int expectedX, int expectedY)
        {
            var rover = new Rover(startDirection);
            var roverMoved = rover.TryMove((_) => true);

            Assert.True(roverMoved);
            Assert.Equal(expectedX, rover.Location.X);
            Assert.Equal(expectedY, rover.Location.Y);
        }

        [Fact]
        public void TheRoverCannotMoveIfItsBlocked()
        {
            var rover = new Rover();
            var roverMoved = rover.TryMove((_) => false);

            Assert.False(roverMoved);
            Assert.Equal(0, rover.Location.X);
            Assert.Equal(0, rover.Location.Y);
        }

    }
}
