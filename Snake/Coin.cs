using Utils;

namespace Snake
{
    internal class Coin
    {
        public Point2I Pos;
        public Coin(Level level, List<Point2I> snake)
        {
            SpawnCoin(level, snake);
        }
        public void SpawnCoin(Level level, List<Point2I> snake)
        {
            Random rand = new Random();
            bool isOnSnake;
            Point2I newPos = new Point2I();
            do
            {
                isOnSnake = false;
                newPos = (rand.Next(1, level.Width - 1), rand.Next(1, level.Height - 1));
                for (int i = 0; i < snake.Count; i++)
                {
                    if (snake[i] == newPos)
                    {
                        isOnSnake = true;
                    }
                }
            } while (isOnSnake);
            Pos = newPos;
        }
        public void Draw()
        {
            MyConsole.SetCell(Pos, "  ", ConsoleColor.Red);
        }
    }
}
