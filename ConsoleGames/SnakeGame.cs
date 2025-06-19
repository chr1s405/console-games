using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace ConsoleGames
{

    internal class SnakeGame
    {
        static List<LevelCell> levelCells = new List<LevelCell>{
                new LevelCell(2, "o "),
                new LevelCell(3, "[]", ConsoleColor.Green),
                new LevelCell(4, "  ", ConsoleColor.Green),
            };
        public static void Play()
        {
            Level level = new Level(22, 12, levelCells);
            Snake snake = new Snake();
            Point2I coin = SpawnCoin(level, snake);
            level.Edit(coin, 2);
            bool isEnd = false;
            while (!isEnd)
            {
                if (snake.Eat(level, coin))
                {
                    level.Edit(coin, 4);
                    coin = SpawnCoin(level, snake);
                    level.Edit(coin, 2);
                }
                level.Edit(snake.Body.Last(), 0);
                Move(snake, level);
                level.Edit(snake.Body[0], 4);
                level.Draw();
                level.PrintLevel();
                if (isDead(snake))
                {
                    isEnd = true;
                    break;
                }

                System.Threading.Thread.Sleep(100);
            }
        }

        public static void Move(Snake snake, Level level)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                if (snake.Direction[0] == Dir.up || snake.Direction[0] == Dir.down)
                {
                    if (key == ConsoleKey.LeftArrow) { snake.Direction[0] = Dir.left; }
                    if (key == ConsoleKey.RightArrow) { snake.Direction[0] = Dir.right; }
                }
                if (snake.Direction[0] == Dir.left || snake.Direction[0] == Dir.right)
                {
                    if (key == ConsoleKey.UpArrow) { snake.Direction[0] = Dir.up; }
                    if (key == ConsoleKey.DownArrow) { snake.Direction[0] = Dir.down; }
                }
            }
            snake.Move(snake.Direction[0], level);
        }
        public static bool isDead(Snake snake)
        {
            for (int i = 1; i < snake.Body.Count; i++)
            {
                if (snake.Body[0] == snake.Body[i])
                {
                    return true;
                }
            }
            return false;
        }
        public static Point2I SpawnCoin(Level level, Snake snake)
        {
            Random rand = new Random();
            bool isOnSnake;
            Point2I coin = new Point2I();
            int x;
            int y;
            do
            {
                isOnSnake = false;
                coin = (rand.Next(1, level.Width - 1), rand.Next(1, level.Height - 1));
                for (int i = 0; i < snake.Body.Count; i++)
                {
                    if (snake.Body[i] == coin)
                    {
                        isOnSnake = true;
                    }
                }
            } while (isOnSnake);
            return coin;
        }
        public static bool CollectCoin(Snake snake, Point2I coin)
        {
            return snake.Body[0] == coin;
        }


        internal struct Snake
        {
            private List<Point2I> body = new List<Point2I>();
            private List<Dir> direction = new List<Dir>();
            public List<Point2I> Body { get => body; }
            public List<Dir> Direction { get => direction; }
            public Snake()
            {
                Body.Add((2, 2));
                direction.Add(Dir.right);
            }

            public void Grow(Level level)
            {
                Body.Add(Body.Last() - direction.Last());
                direction.Add(direction.Last());
                if (Body.Last().X == 0)
                    Body[Body.Count - 1] = (level.Width - 2, Body.Last().Y);

                if (Body.Last().Y == 0)
                    Body[Body.Count - 1] = (Body.Last().X, level.Height - 2);

                if (Body.Last().X == level.Width - 1)
                    Body[Body.Count - 1] = (1, Body.Last().Y);

                if (Body.Last().Y == level.Height - 1)
                    Body[Body.Count - 1] = (Body.Last().X, 1);
            }
            public void Move(Dir newDir, Level level)
            {
                Body.Insert(0, Body[0] + newDir);
                direction.Insert(0, newDir);
                Body.RemoveAt(Body.Count - 1);
                direction.RemoveAt(Body.Count - 1);
                if (Body[0].X == 0) { Body[0] = (level.Width - 2, Body[0].Y); }
                if (Body[0].Y == 0) { Body[0] = (Body[0].X, level.Height - 2); }
                if (Body[0].X == level.Width - 1) { Body[0] = (1, Body[0].Y); }
                if (Body[0].Y == level.Height - 1) { Body[0] = (Body[0].X, 1); }
            }
            public bool Eat(Level level, Point2I collectible) 
            { 
                if (Body[0] == collectible)
                {
                    Grow(level);
                    return true;
                }
                return false;
            }
        }
    }
}
