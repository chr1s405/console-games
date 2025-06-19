using System.Diagnostics.CodeAnalysis;

namespace Utils
{
    public enum Dir { up, right, down, left }
    public struct Point2I
    {
        private int x;
        private int y;
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }

        public Point2I()
        {
            X = 0;
            Y = 0;
        }
        public Point2I(int x, int y)
        {
            X = x;
            Y = y;
        }
        public static implicit operator Point2I((int x, int y) pos)
        {
            return new Point2I(pos.x, pos.y);
        }
        public static Point2I operator +(Point2I point)
        {
            return point;
        }
        public static Point2I operator -(Point2I point)
        {
            return new Point2I(-point.x, -point.y);
        }
        public static Point2I operator +(Point2I left, Point2I right)
        {
            return new Point2I(left.x + right.x, left.y + right.y);
        }
        public static Point2I operator +(Point2I left, Dir dir)
        {
            switch (dir)
            {
                case Dir.left: return left + (-1, 0); break;
                case Dir.right: return left + (1, 0); break;
                case Dir.up: return left + (0, -1); break;
                case Dir.down: return left + (0, 1); break;
            }
            throw new Exception("there is no matching direction");
        }
        public static Point2I operator -(Point2I left, Point2I right)
        {
            return left + -right;
        }
        public static Point2I operator -(Point2I left, Dir dir)
        {
            return -(-left + dir);
        }
        public static bool operator ==(Point2I left, Point2I right)
        {
            return left.X == right.X && left.Y == right.Y;
        }
        public static bool operator !=(Point2I left, Point2I right)
        {
            return !(left == right);
        }
        public override string ToString()
        {
            return $"({x}, {y})";
        }
        public override bool Equals(object? obj)
        {
            if (obj is null || !(obj is Point2I))
                return false;

            return ((Point2I)obj).X == X && ((Point2I)obj).y == Y;
        }
    }
}
