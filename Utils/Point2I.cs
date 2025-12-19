
namespace Utils
{
    public struct Point2I
    {
        public int x;
        public int y;

        public Point2I(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Point2I() : this(0, 0) { }
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
                default: return left; break;
            }
        }
        public static Point2I operator -(Point2I left, Point2I right)
        {
            return left + -right;
        }
        public static Point2I operator -(Point2I left, Dir dir)
        {
            return -(-left + dir);
        }
        public static Point2I operator *(Point2I left, int right)
        {
            return (left.x * right, left.y * right);
        }
        public static bool operator ==(Point2I left, Point2I right)
        {
            return left.x == right.x && left.y == right.y;
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

            return ((Point2I)obj).x == x && ((Point2I)obj).y == y;
        }
    }
}
