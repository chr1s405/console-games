using Utils;

namespace Chess
{
    public class Board : Level
    {
        public Board() : base(8, 8, new List<LevelCell>())
        {
            levelCells.Add(new LevelCell(levelCells.Count, "  "));
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    this.grid[col, row] = 0;
                }
            }
        }
        public override void Draw()
        {
            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    LevelCell cell = levelCells[grid[col, row]];
                    ConsoleColor color;
                    if ((col + row) % 2 == 0)
                    {
                        color = ConsoleColor.DarkGray;
                    }
                    else
                    {
                        color = ConsoleColor.Gray;
                    }
                    MyConsole.SetBackground((col, row), color);
                    MyConsole.SetForeGround((col, row), cell.CellString, cell.FgColor);
                }
            }
        }
    }
}
