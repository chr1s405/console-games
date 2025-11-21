namespace Utils
{
    public enum Dir { none, up, right, down, left }
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
    public struct Matrix
    {
        private int[,] array;
        public int this[int x, int y] { get => array[x, y]; set => array[x, y] = value; }
        public int this[Point2I pos] { get => array[pos.x, pos.y]; set => array[pos.x, pos.y] = value; }
        public Matrix(Matrix matrix)
        {
            this.array = matrix.array;
        }
        public Matrix(int width, int height) : this(new int[height, width]) { }
        public Matrix(int[,] array)
        {
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);
            this.array = new int[cols, rows];
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    this.array[col, row] = array[row, col];
                }
            }
        }
        public void Transpose()
        {
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);
            int[,] arrCopy = new int[rows, cols];
            Array.Copy(array, arrCopy, array.Length);
            array = new int[cols, rows];
            for (int y = 0; y < array.GetLength(1); y++)
            {
                for (int x = 0; x < array.GetLength(0); x++)
                {
                    array[x, y] = arrCopy[y, x];
                }
            }
            arrCopy = null;
        }
        public int GetWidth() { return array.GetLength(0); }
        public int GetHeight() { return array.GetLength(1); }
        public override string ToString()
        {
            string arrString = "";
            for (int y = 0; y < array.GetLength(1); y++)
            {
                for (int x = 0; x < array.GetLength(0); x++)
                {
                    arrString += ($"{array[x, y].ToString().PadRight(3)}");
                }
                arrString += ("\n");
            }
            return arrString;
        }
    }
}
