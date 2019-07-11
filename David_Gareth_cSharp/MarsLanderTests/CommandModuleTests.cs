using MarsLander;
using Xunit;

namespace MarsLanderTests
{
    public class CommandModuleTests
    {
        [Fact]
        public void EmptyCommandDoesnotMoveRover()
        {
            var rover = new Rover();
            var world = new BoundedWorld(5, 5);

            var commandModule = new CommandModule(rover, world.IsWithinBoundary);
            commandModule.Execute("", IgnoreErrors);

            Assert.Equal(0, rover.Location.X);
            Assert.Equal(0, rover.Location.Y);
        }

        [Theory]
        [InlineData("F", 0, 1, Direction.North)]
        [InlineData("L", 0, 0, Direction.West)]
        [InlineData("R", 0, 0, Direction.East)]
        public void RoverFollowsSimpleCommandsToExpectedCoordinate(string command, int expectedX, int expectedY, Direction expectedDirection)
        {
            var rover = new Rover();
            var world = new BoundedWorld(3, 3);

            var commandModule = new CommandModule(rover, world.IsWithinBoundary);
            commandModule.Execute(command, IgnoreErrors);

            Assert.Equal(expectedX, rover.Location.X);
            Assert.Equal(expectedY, rover.Location.Y);
            Assert.Equal(expectedDirection, rover.Direction);
        }

        [Theory]
        [InlineData("RFLFFRFR", 2, 2, Direction.South)]
        [InlineData("FFFFFRF", 1, 4, Direction.East)]
        [InlineData("FFRFFFFFFRF", 4, 1, Direction.South)]
        public void RoverFollowsCompleteCommandsToExpectedCoordinate(string command, int expectedX, int expectedY, Direction expectedDirection)
        {
            var rover = new Rover();
            var world = new BoundedWorld(5, 5);

            var commandModule = new CommandModule(rover, world.IsWithinBoundary);
            commandModule.Execute(command, IgnoreErrors);

            Assert.Equal(expectedX, rover.Location.X);
            Assert.Equal(expectedY, rover.Location.Y);
            Assert.Equal(expectedDirection, rover.Direction);
        }

        [Theory]
        [InlineData("RFLFFRFR", 2, 2, Direction.South, false)]
        [InlineData("FFFFFRF", 0, 4, Direction.North, true)]
        [InlineData("FFRFFFFFFRF", 4, 2, Direction.East, true)]
        public void RoverFollowsCompleteCommandsToExpectedCoordinateWithFailReportingPolicy(string command, int expectedX, int expectedY, Direction expectedDirection, bool errorsOccured)
        {
            var rover = new Rover();
            var world = new BoundedWorld(5, 5);
            var policy = new StopOnErrorPolicy();

            var commandModule = new CommandModule(rover, world.IsWithinBoundary);

            var errorHappened = false;
            commandModule.Execute(command, () => { errorHappened = true; return false; });

            Assert.Equal(errorsOccured, errorHappened);
            Assert.Equal(expectedX, rover.Location.X);
            Assert.Equal(expectedY, rover.Location.Y);
            Assert.Equal(expectedDirection, rover.Direction);

            //commandModule.Execute(command, policy.StopOnError);
            //Assert.Equal(errorsOccured, policy.ErrorHappened);
        }

        private bool IgnoreErrors() { return true; }


    }

    public class StopOnErrorPolicy
    {
        public bool ErrorHappened { get; private set; }

        public bool StopOnError()
        {
            ErrorHappened = true;
            return false;
        }
    }
}
