using System;

namespace MarsLander
{
    public class BoundedWorld
    {
        public Coordinate UpperBound { get; private set; }
        public BoundedWorld(int width, int height)
        {
            UpperBound = new Coordinate(width, height);
        }

        public bool IsWithinBoundary(Coordinate coordinate)
        {
            bool InInterval(int point, int upperBound) => 0 <= point && point < upperBound;

            return InInterval(coordinate.X, UpperBound.X) &&
                   InInterval(coordinate.Y, UpperBound.Y);
        }

    }
}
