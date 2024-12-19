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
            int[,] level = CreateLevel(20, 10);
            SpawnCoins(level, snake);
            //int cameraHeight = 10;
            //int cameraWidth = 10;
            int coins = 0;
            bool isEnd = false;
            while (!isEnd)
            {
                Move(level, ref snake);
                if (CollectCoin(level, ref snake, ref coins))
                {
                    SpawnCoins(level, snake);
                }
                if (isDead(snake))
                {
                    isEnd = true;
                }
                Console.Clear();
                DrawLevel(snake, coins, level);

                System.Threading.Thread.Sleep(100);
            }
            Console.WriteLine($"your total score is {coins}");
        }
        public static int[,] CreateLevel(int levelSize)
        {
            return CreateLevel(levelSize, levelSize);
        }
        public static int[,] CreateLevel(int levelWidth, int levelHeight)
        {
            int[,] level = new int[levelHeight, levelWidth];
            for (int height = 0; height < level.GetLength(0); height++)
            {
                for (int width = 0; width < level.GetLength(1); width++)
                {
                    if (((width == 0 || width == level.GetLength(1) - 1) && (0 <= height && height < level.GetLength(0))) ||
                    ((height == 0 || height == level.GetLength(0) - 1) && (0 <= width && width < level.GetLength(1))))
                    {
                        level[height, width] = 1;
                    }
                    else
                    {
                        level[height, width] = 0;
                    }
                }
            }
            return level;
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
        public static void DrawLevel(List<int[]> snake, int coins, int[,] level, int minX = 0, int minY = 0, int maxWidth = 0, int maxHeight = 0)
        {
            if (maxWidth == 0) { maxWidth = level.GetLength(1); }
            if (maxHeight == 0) { maxHeight = level.GetLength(0); }
            for (int height = minY; height < maxHeight; height++)
            {
                for (int width = minX; width < maxWidth; width++)
                {
                    bool isOnSnake = false;
                    bool isHead = false;
                    for (int i = 0; i < snake.Count; i++)
                    {
                        if (snake[i][0] == width && snake[i][1] == height)
                        {
                            isOnSnake = true;
                            isHead = (i == 0);
                            break;
                        }
                    }
                    if (isOnSnake)
                    {
                            Console.BackgroundColor = ConsoleColor.Green;
                        if (isHead)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write("[]");
                        }
                        else
                        {
                            Console.Write("  ");
                        }
                        Console.ResetColor();
                    }
                    else
                    {
                        switch (level[height, width])
                        {
                            case 0: Console.Write("  "); break;
                            case 1: Console.Write("x "); break;
                            case 9: Console.Write("o "); break;
                        }
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine(coins);
        }
        public static void DrawLevel(List<int[]> snake, int coins, int cameraWidth, int cameraHeight, int[,] level)
        {
            if (cameraWidth < level.GetLength(1) || cameraHeight < level.GetLength(0))
            {
                int maxCameraX = Math.Min(level.GetLength(1), snake[0][0] + cameraWidth / 2);
                int maxCameraY = Math.Min(level.GetLength(0), snake[0][1] + cameraHeight / 2);
                int minCameraX = Math.Max(0, snake[0][0] - cameraWidth / 2);
                int minCameraY = Math.Max(0, snake[0][1] - cameraHeight / 2);
                if (level.GetLength(1) >= cameraWidth)
                {
                    if (minCameraX == 0) { maxCameraX = cameraWidth; }
                    if (maxCameraX == level.GetLength(1)) { minCameraX = level.GetLength(1) - cameraWidth; }
                }
                if (level.GetLength(1) >= cameraHeight)
                {
                    if (minCameraY == 0) { maxCameraY = cameraHeight; }
                    if (maxCameraY == level.GetLength(0)) { minCameraY = level.GetLength(0) - cameraHeight; }
                }
                DrawLevel(snake, coins, level, minCameraX, minCameraY, maxCameraX, maxCameraY);
            }
            else
            {
                DrawLevel(snake, coins, level);
            }
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
        public static bool CollectCoin(int[,] level, ref List<int[]> snake, ref int coins)
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
