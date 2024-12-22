using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGames
{
    internal class Snake
    {
        public enum Dir { up, right, down, left }
        public static void Play()
        {
            Level level = new Level(22, 12);
            List<int[]> snake = CreateSnake(level, 1);
            SpawnCoins(level, snake);
            int coins = 0;
            bool isEnd = false;
            while (!isEnd)
            {
                Move(level, snake);
                if (CollectCoin(level, snake, ref coins))
                {
                    SpawnCoins(level, snake);
                }
                if (isDead(snake))
                {
                    isEnd = true;
                }
                level.Draw(snake);
                Console.WriteLine(coins);

                System.Threading.Thread.Sleep(100);
            }
            Console.WriteLine($"your total score is {coins}");
        }
        public static List<int[]> CreateSnake(Level level, int size)
        {
            int x = 5;
            int y = size / (level.Width-2) + 2;
            List<int[]> snake = new List<int[]>();
            for (int i = 0; i < size; i++)
            {
                if (x < 1)
                {
                    y--;
                    snake.Add([1, y, (int)Dir.down]);
                    x = level.Width - 1;
                }
                else
                {
                    snake.Add([x, y, (int)Dir.right]);
                }
                x--;
            }
            return snake;

        }
        public static void Move(Level level, List<int[]> snake)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.LeftArrow: if ((Dir)snake[0][2] == Dir.up || (Dir)snake[0][2] == Dir.down) { snake[0][2] = (int)Dir.left; }; break;
                    case ConsoleKey.RightArrow: if ((Dir)snake[0][2] == Dir.up || (Dir)snake[0][2] == Dir.down) { snake[0][2] = (int)Dir.right; }; break;
                    case ConsoleKey.UpArrow: if ((Dir)snake[0][2] == Dir.left || (Dir)snake[0][2] == Dir.right) { snake[0][2] = (int)Dir.up; }; break;
                    case ConsoleKey.DownArrow: if ((Dir)snake[0][2] == Dir.left || (Dir)snake[0][2] == Dir.right) { snake[0][2] = (int)Dir.down; }; break;
                }
            }
            for (int i = 0; i < snake.Count; i++)
            {
                if ((Dir)snake[i][2] == Dir.left)
                {
                    snake[i][0]--;
                    if (snake[i][0] < 1)
                    {
                        snake[i][0] = level.Width - 2;
                    }
                }
                if ((Dir)snake[i][2] == Dir.right)
                {
                    snake[i][0]++;
                    if (snake[i][0] > level.Width - 2)
                    {
                        snake[i][0] = 1;
                    }
                }
                if ((Dir)snake[i][2] == Dir.up)
                {
                    snake[i][1]--;
                    if (snake[i][1] < 1)
                    {
                        snake[i][1] = level.Height - 2;
                    }
                }
                if ((Dir)snake[i][2] == Dir.down)
                {
                    snake[i][1]++;
                    if (snake[i][1] > level.Height - 2)
                    {
                        snake[i][1] = 1;
                    }
                }
            }
            for (int i = snake.Count - 1; i > 0; i--)
            {
                snake[i][2] = snake[i - 1][2];
            }
        }
        public static bool isDead(List<int[]> snake)
        {
            for (int i = 1; i < snake.Count; i++)
            {
                if (snake[0][0] == snake[i][0] && snake[0][1] == snake[i][1])
                {
                    return true;
                }
            }
            return false;
        }
        public static void SpawnCoins(Level level, List<int[]> snake)
        {
            Random rand = new Random();
            bool isOnSnake = true;
            int x;
            int y;
            do
            {
                isOnSnake = false;
                x = rand.Next(1, level.Width - 1);
                y = rand.Next(1, level.Height - 1);
                for (int i = 0; i < snake.Count; i++)
                {
                    if (x == snake[i][0] && y == snake[i][1])
                    {
                        isOnSnake = true;
                    }
                }
            } while (isOnSnake);
            level.Modify(x, y, 9);
        }
        public static bool CollectCoin(Level level, int[] pos, ref int coins)
        {
            if (level.GetValue(pos[0], pos[1]) == 9)
            {
                coins++;
                level.Modify(pos[0], pos[1], 0);

                return true;
            }
            return false;
        }
        public static bool CollectCoin(Level level, List<int[]> snake, ref int coins)
        {
            if (CollectCoin(level, snake[0], ref coins))
            {
                int x = 0;
                int y = 0;
                if ((Dir)snake.Last()[2] == Dir.up) { y = 1; }
                else if ((Dir)snake.Last()[2] == Dir.down) { y = -1; }
                else if ((Dir)snake.Last()[2] == Dir.left) { x = 1; }
                else if ((Dir)snake.Last()[2] == Dir.right) { x = -1; }
                snake.Add(new int[] { snake.Last()[0] + x, snake.Last()[1] + y, snake.Last()[2] });
                return true;
            }
            return false;
        }
    }
}
