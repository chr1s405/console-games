using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Utils;
using static ConsoleGames.SnakeGame;

namespace ConsoleGames
{
    internal class SnakeGame : Game
    {
        Snake snake;
        Level level;
        Point2I coin;
        int score;
        public SnakeGame()
        {
            snake = new Snake();
            level = new Level(22, 12);
            coin = SpawnCoin(level, snake);
            score = 0;
        }
        public override void Initialize()
        {
            level.Draw();
        }
        override public void Update()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
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
            snake.Move(level, coin);
            if (coin == snake.Body[0])
            {
                score++;
                coin = SpawnCoin(level, snake);
            }
            if (isDead(snake))
                GameOver = true;
        }
        override public void Draw()
        {
            snake.Draw();
            MyConsole.Draw(coin, "0 ");
            level.DrawRestore();
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
    }
    internal class Snake
    {
        private List<Point2I> body = new List<Point2I>();
        private Dir direction;
        public List<Point2I> Body { get => body; }
        public Dir Direction { get => direction; set => direction = value; }
        public Snake()
        {
            Body.Add((2, 2));
            Body.Add((1, 2));
            direction = Dir.right;
        }
        public void Move(Level level, Point2I coin)
        {
            Body.Insert(0, Body[0] + direction);
            if (!(Body[0] == coin))
            {
                level.RestorePos.Add(Body.Last());
                Body.RemoveAt(Body.Count - 1);
            }
            if (Body[0].x == 0) { Body[0] = (level.Width - 2, Body[0].y); }
            if (Body[0].y == 0) { Body[0] = (Body[0].x, level.Height - 2); }
            if (Body[0].x == level.Width - 1) { Body[0] = (1, Body[0].y); }
            if (Body[0].y == level.Height - 1) { Body[0] = (Body[0].x, 1); }
        }
        public void Draw()
        {
            if (Body.Count > 0)
                MyConsole.Draw(body[1], "  ", ConsoleColor.Green);
            MyConsole.Draw(body[0], "[]", ConsoleColor.Green);
        }
    }
}
