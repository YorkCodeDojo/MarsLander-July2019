using System;

namespace MarsLander
{
    public class CommandModule
    {
        private readonly Rover _rover;
        private Func<Coordinate, bool> _availableLocation;

        public CommandModule(Rover rover, Func<Coordinate, bool> availableLocation)
        {
            _rover = rover;
            _availableLocation = availableLocation;
        }

        public void Execute(string commands, Func<bool> continueOnError)
        {
            foreach (var instruction in commands)
            {
                switch (instruction)
                {
                    case 'F':
                        if (!_rover.TryMove(_availableLocation))
                        {
                            if (!continueOnError()) return;
                        }
                        break;
                    case 'L':
                        _rover.TurnLeft();
                        break;
                    case 'R':
                        _rover.TurnRight();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
