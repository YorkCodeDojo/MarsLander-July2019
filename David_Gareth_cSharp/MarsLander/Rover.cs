using System;

namespace MarsLander
{
    public class Rover
    {
        public Coordinate Location { get; private set; }
        public Direction Direction { get; private set; }

        public Rover(Direction startDirection = default)
        {
            Direction = startDirection;
        }

        public void TurnLeft()
        {
            switch (Direction)
            {
                case Direction.North:
                    Direction = Direction.West;
                    break;
                case Direction.West:
                    Direction = Direction.South;
                    break;
                case Direction.South:
                    Direction = Direction.East;
                    break;
                case Direction.East:
                    Direction = Direction.North;
                    break;
                default:
                    break;
            }
        }

        public void TurnRight()
        {
            TurnLeft();
            TurnLeft();
            TurnLeft();
        }

        public bool TryMove(Func<Coordinate, bool> CanEnter)
        {
            var newLocation = Location;

            switch (Direction)
            {
                case Direction.North:
                    newLocation = Location.WithY(Location.Y + 1);
                    break;
                case Direction.West:
                    newLocation = Location.WithX(Location.X - 1);
                    break;
                case Direction.South:
                    newLocation = Location.WithY(Location.Y - 1);
                    break;
                case Direction.East:
                    newLocation = Location.WithX(Location.X + 1);
                    break;
                default:
                    break;
            }

            if (CanEnter(newLocation))
            {
                Location = newLocation;

                return true;
            }

            return false;
        }
    }

    public enum Direction
    {
        North,
        West,
        South,
        East
    }
}
