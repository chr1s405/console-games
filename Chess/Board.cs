using Utils;

namespace Chess
{
    public class Board : Level
    {
        public Board() : base(10, 10, new List<LevelCell>())
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
                    if ((row == 0 || row == 9))
                    {
                        if (col != 0 && col != 9)
                            MyConsole.SetForeGround((col, row), col.ToString(), ConsoleColor.DarkGray);
                    }
                    else
                    {
                        if (col == 0 || col == 9)
                        {
                            string abc = " abcdefgh ";
                            MyConsole.SetForeGround((col, row), abc.Substring(row, 1), ConsoleColor.DarkGray);
                        }
                        else
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
    }
}
