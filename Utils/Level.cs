using System.Net.Http.Headers;
using System.Reflection.Emit;

namespace Utils
{
    public class Level
    {
        protected int[,] grid;
        protected List<LevelCell> levelCells = new List<LevelCell>();
        public int this[Point2I pos] { get => this[pos.x, pos.y]; }
        public int this[int x, int y] { get => grid[x, y]; }
        public int Width { get => grid.GetLength(0); }
        public int Height { get => grid.GetLength(1); }

        public Level(int width, int height, List<LevelCell> cells = null)
            : this(cells)
        {
            this.grid = new int[width, height];
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    if (row == 0 || row == height - 1 || col == 0 || col == width - 1)
                        this.grid[col, row] = 1;
                    else
                        this.grid[col, row] = 0;
                }
            }
            Camera.Init(Width, Height);
        }
        public Level(int[,] level, List<LevelCell> cells = null)
            : this(cells)
        {
            int width = level.GetLength(1);
            int height = level.GetLength(0);
            this.grid = new int[width, height];
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    this.grid[col, row] = level[row, col];
                }
            }
            Camera.Init(Width, Height);
        }
        public Level(List<LevelCell> cells = null)
        {
            if (cells is null)
            {
                levelCells.Add(new LevelCell(levelCells.Count, "  "));
                levelCells.Add(new LevelCell(levelCells.Count, "[]", ConsoleColor.DarkGray));
            }
            else
            {
                foreach (LevelCell cell in cells)
                {
                    levelCells.Add(cell);
                }
            }
        }
        public virtual void Draw()
        {
            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    LevelCell cell = levelCells[grid[col, row]];
                    MyConsole.SetCell((col, row), cell.CellString, cell.BgColor, cell.FgColor);
                }
            }

        }
    }
}
