using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleGames.Snake;

namespace ConsoleGames
{
    internal class GravityGame
    {
        static List<LevelCell> levelCells = new List<LevelCell>{
                new LevelCell(2, "  ", ConsoleColor.Green),
            };
        static int timer = 0;
        public static void Play()
        {
            Level level = new Level(20, 10);
            List<int[]> obstacles = new List<int[]>();
            int[] player = new int[] { 10, level.Height - 5, 1, 0 };
            int score = 0;
            bool isEnd = false;
            while (!isEnd)
            {
                CreateObstacle(level, obstacles);
                MoveObstacles(obstacles);
                Jump(player);
                Move(level, player);
                if (isDead(player, level))
                {
                    isEnd = true;
                }
                level.Draw(player, obstacles, levelCells);
                Console.WriteLine(score);
                System.Threading.Thread.Sleep(100);
                score++;
            }
        }
        public static void Move(Level level, int[] player)
        {
            player[1] += player[2];
            if (level.LevelAdpt[player[1], player[0] + 1] == 1)
            {
                player[1] -= player[2];
                player[3] = 1;
            }
            if (level.LevelAdpt[player[1], player[0] + 1] == 1)
            {
                player[0]--;
            }
        }
        public static void Jump(int[] player)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey key;
                do
                {
                    key = Console.ReadKey(true).Key;
                } while (Console.KeyAvailable);
                if (player[3] == 1)
                {
                    if (key == ConsoleKey.Spacebar || key == ConsoleKey.UpArrow)
                    {
                        player[2] = -player[2];
                        player[3] = 0;
                    }
                }

            }
        }
        public static void CreateObstacle(Level level, List<int[]> obstacles)
        {
            Random rand = new Random();
            int height = rand.Next(level.Height);
            if (rand.Next(3) == 1)
            {
                for (int i = 0; i < rand.Next(5, 10); i++)
                {
                    if (height == 0 || height == level.Height - 1)
                    {
                        obstacles.Add(new int[] { level.Width + i, height, 0 });
                    }
                    else
                    {
                        obstacles.Add(new int[] { level.Width + i, height, 1 });
                    }
                }
            }
        }
        public static void MoveObstacles(List<int[]> obstacles)
        {
            for (int i = 0; i < obstacles.Count; i++)
            {
                obstacles[i][0]--;
                if (obstacles[i][0] == 0)
                {
                    obstacles.RemoveAt(i);
                    i--;
                }
            }
        }
        public static bool isDead(int[] player, Level level)
        {
            return player[0] < 1 || player[1] < 1 || player[1] > level.Height - 2;
        }
    }
}
