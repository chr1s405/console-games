
namespace Utils
{
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
