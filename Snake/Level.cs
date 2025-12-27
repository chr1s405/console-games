
namespace Snake
{
    internal class Level: Utils.Level
    {
        public Level(int width, int height): base(width, height, new List<Utils.LevelCell>())
        {
            levelCells.Add(new Utils.LevelCell(levelCells.Count,"  "));
            levelCells.Add(new Utils.LevelCell(levelCells.Count,"[]",ConsoleColor.DarkGray));
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
        }
    }
}
