namespace MarsLander
{
    public struct Coordinate
    {
        public readonly int X;
        public readonly int Y;

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Coordinate WithX(int newX)
        {
            return new Coordinate(newX, Y);
        }

        public Coordinate WithY(int newY)
        {
            return new Coordinate(X, newY);
        }
    }
}
