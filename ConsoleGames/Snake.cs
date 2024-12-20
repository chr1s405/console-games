using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGames
{
    internal class Snake
    {
        public enum Dir { up, right, down, left }
        public static void Play()
        {
            List<int[]> snake = CreateSnake(1);
            Level level = new Level(20, 10);
            SpawnCoins(level.GetLevel, snake);
            int coins = 0;
            bool isEnd = false;
            while (!isEnd)
            {
                Move(level.GetLevel, ref snake);
                if (CollectCoin(level.GetLevel, snake, ref coins))
                {
                    SpawnCoins(level.GetLevel, snake);
                }
                if (isDead(snake))
                {
                    isEnd = true;
                }
                Console.Clear();
                level.Draw(snake);
                Console.WriteLine(coins);

                System.Threading.Thread.Sleep(100);
            }
            Console.WriteLine($"your total score is {coins}");
        }
        public static List<int[]> CreateSnake(int size)
        {
            List<int[]> snake = new List<int[]>();
            for (int i = 0; i < size; i++)
            {
                snake.Add([size - i, 1, 1]);
            }
            return snake;

        }
        public static void Move(int[,] level, ref List<int[]> snake)
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
                        snake[i][0] = level.GetLength(1) - 2;
                    }
                }
                if ((Dir)snake[i][2] == Dir.right)
                {
                    snake[i][0]++;
                    if (snake[i][0] > level.GetLength(1) - 2)
                    {
                        snake[i][0] = 1;
                    }
                }
                if ((Dir)snake[i][2] == Dir.up)
                {
                    snake[i][1]--;
                    if (snake[i][1] < 1)
                    {
                        snake[i][1] = level.GetLength(0) - 2;
                    }
                }
                if ((Dir)snake[i][2] == Dir.down)
                {
                    snake[i][1]++;
                    if (snake[i][1] > level.GetLength(0) - 2)
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
        public static void SpawnCoins(int[,] level, List<int[]> snake)
        {
            Random rand = new Random();
            bool isOnSnake = true;
            int x;
            int y;
            do
            {
                isOnSnake = false;
                x = rand.Next(1, level.GetLength(1) - 1);
                y = rand.Next(1, level.GetLength(0) - 1);
                for (int i = 0; i < snake.Count; i++)
                {
                    if (x == snake[i][0] && y == snake[i][1])
                    {
                        isOnSnake = true;
                    }
                }
            } while (isOnSnake);
            level[y, x] = 9;
        }
        public static bool CollectCoin(int[,] level, int[] pos, ref int coins)
        {
            if (level[pos[1], pos[0]] == 9)
            {
                coins++;
                level[pos[1], pos[0]] = 0;

                return true;
            }
            return false;
        }
        public static bool CollectCoin(int[,] level, List<int[]> snake, ref int coins)
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
