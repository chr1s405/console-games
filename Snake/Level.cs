
namespace Snake
{
    internal class Level: Utils.Level
    {
        public Level(int width, int height): base(width, height, new List<Utils.LevelCell>())
        {
            levelCells.Add(new Utils.LevelCell(levelCells.Count,"  "));
            levelCells.Add(new Utils.LevelCell(levelCells.Count,"[]",ConsoleColor.DarkGray));
        }
    }
}
