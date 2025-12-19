using Utils;

namespace Snake
{
    public class Game : Utils.Game
    {
        Snake snake;
        Level level;
        Coin coin;
        int score;
        public override void Initialize()
        {
            snake = new Snake();
            snake = new Snake();
            level = new Level(22, 12);
            coin = new Coin(level, snake.Body);
            score = 0;
            MyConsole.Init(level.Width, level.Height);
        }
        public override void Update()
        {
            ConsoleKey key = Input.Key;
            if (key != ConsoleKey.None)
            {
                if (snake.Direction == Dir.up || snake.Direction == Dir.down)
                {
                    if (key == ConsoleKey.LeftArrow) { snake.Direction = Dir.left; }
                    if (key == ConsoleKey.RightArrow) { snake.Direction = Dir.right; }
                }
                else
                {
                    if (key == ConsoleKey.UpArrow) { snake.Direction = Dir.up; }
                    if (key == ConsoleKey.DownArrow) { snake.Direction = Dir.down; }
                }
            }
            snake.Move(level, coin.Pos);
            Camera.Center(snake.Body[0], level.Width, level.Height);
            if (coin.Pos == snake.Body[0])
            {
                score++;
                coin.SpawnCoin(level, snake.Body);
            }
            GameOver = snake.IsDead();
            if (GameOver) Message = $"Game Over";
        }
        public override void Draw()
        {
            level.Draw();
            coin.Draw();
            snake.Draw();

            MyConsole.Draw();
            Console.WriteLine($"Score: {score}");
        }

    }
}
