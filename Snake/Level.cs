using Utils;

namespace Snake
{
    public class Level: Utils.Level
    {
        public Level(int width, int height, List<LevelCell> levelCells = null) : base(width, height, levelCells) { }
        public override void Draw()
        {
            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    MyConsole.SetCell((col, row), levelCells[grid[col,row]].CellString);
                }
            }
            //initializeLevelString();
        }
    }
}
