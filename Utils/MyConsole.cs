using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace Utils
{
    static public class MyConsole
    {
        private struct GridCell
        {
            public string Sprite;
            public ConsoleColor ForegroundColor;
            public ConsoleColor BackgroundColor;
            public GridCell(GridCell cell)
            {
                Sprite = cell.Sprite;
                BackgroundColor = cell.BackgroundColor;
                ForegroundColor = cell.ForegroundColor;
            }
            public GridCell(string sprite = "  ", ConsoleColor fgColor = ConsoleColor.White, ConsoleColor bgColor = ConsoleColor.Black)
            {
                Sprite = sprite;
                BackgroundColor = bgColor;
                ForegroundColor = fgColor;
            }
            static public bool operator ==(GridCell left, GridCell right)
            {
                return (left.Sprite == right.Sprite) && (left.BackgroundColor == right.BackgroundColor) && (left.ForegroundColor == right.ForegroundColor);
            }
            static public bool operator !=(GridCell left, GridCell right)
            {
                return !(left == right);
            }
        }
        static private GridCell[,] grid = new GridCell[0, 0];
        static private GridCell[,] onScreenGrid = new GridCell[0, 0];
        static public void Init(int width = 10, int height = 10)
        {
            grid = new GridCell[width, height];
            onScreenGrid = new GridCell[width, height];
        }
        static public void SetCell(Point2I pos, string sprite, ConsoleColor bgColor = ConsoleColor.Black, ConsoleColor fgColor = ConsoleColor.White)
        {
            if (pos.x >= grid.GetLength(0) || pos.y >= grid.GetLength(1))
                Resize(Math.Max(grid.GetLength(0), pos.x+1), Math.Max(grid.GetLength(1), pos.y+1));
            grid[pos.x, pos.y].Sprite = sprite;
            grid[pos.x, pos.y].BackgroundColor = bgColor;
            grid[pos.x, pos.y].ForegroundColor = fgColor;
        }
        static public void Draw()
        {
            for (int row = 0; row < grid.GetLength(1); row++)
            {
                for (int col = 0; col < grid.GetLength(0); col++)
                {
                    GridCell cell = grid[col, row];
                    if (cell != onScreenGrid[col, row])
                    {
                        if (Console.ForegroundColor != cell.ForegroundColor)
                            Console.ForegroundColor = cell.ForegroundColor;
                        if (Console.BackgroundColor != cell.BackgroundColor)
                            Console.BackgroundColor = cell.BackgroundColor;
                        Console.CursorLeft = col * 2;
                        Console.CursorTop = row;
                        Console.Write(cell.Sprite.PadRight(2));
                        onScreenGrid[col, row] = new GridCell(cell);
                    }
                }
            }

            Console.CursorLeft = 0;
            Console.CursorTop = grid.GetLength(1);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        static public void Draw2()
        {
            Console.Clear();
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                string text = "";
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    GridCell cell = grid[col + Camera.X, row + Camera.Y];
                    text += cell.Sprite.PadRight(2);
                }
                Console.WriteLine(text);
            }
        }
        static private void Resize(int width, int height)
        {
            GridCell[,] newGrid = new GridCell[width, height];
            GridCell[,] newScreenGrid = new GridCell[width, height];
            for (int row = 0; row < grid.GetLength(1); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    newGrid[col, row] = grid[col, row];
                    newScreenGrid[col, row] = onScreenGrid[col, row];
                }
            }
            grid = newGrid;
            onScreenGrid = newScreenGrid;
            newGrid = null;
            newScreenGrid = null;
        }
    }
}
