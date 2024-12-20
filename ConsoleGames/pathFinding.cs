using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleGames.HelperMethods;


namespace ConsoleGames
{
    class pathFinding
    {
        public static void PathFind()
        {
            //List<List<int[]>> list1 = new List<List<int[]>>{
            //new List<int[]>{new int[] { 8, 2 }},
            //new List<int[]>{new int[] { 3, 4 }},
            //new List<int[]>{new int[] { 8, 7 }}};
            //int[] arr = new int[] { 1,2 };
            //Console.WriteLine(indexOf(list1, arr));
            Console.ResetColor();
            int[] pos1 = new int[] { 10, 2 };
            int[] pos2 = new int[] { 15, 5 };
            int[,] level = CreateLevel(pos1, pos2);
            List<int[]> path = new List<int[]>();
            path.Add(pos1);
            ShowArray(level);
            path = FindPath(ref path, level, pos1, pos2);
            ShowList(path);


        }
        public static int[,] CreateLevel(int levelSize, int[] pos1, int[] pos2)
        {
            return CreateLevel(levelSize, levelSize, pos1, pos2);
        }
        public static int[,] CreateLevel(int levelWidth, int levelHeight, int[] pos1, int[] pos2)
        {
            int[,] level = new int[levelHeight, levelWidth];
            for (int height = 0; height < level.GetLength(0); height++)
            {
                for (int width = 0; width < level.GetLength(1); width++)
                {
                    if (AreEqual(pos1, [width, height]))
                    {
                        level[height, width] = 9;
                    }
                    else if (AreEqual(pos2, [width, height]))
                    {
                        level[height, width] = 8;
                    }
                    else if (((width == 0 || width == level.GetLength(1) - 1) && (0 <= height && height < level.GetLength(0))) ||
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
        public static int[,] CreateLevel(int[] pos1, int[] pos2)
        {
            int[,] level = new int[,]
            {
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1},
                {1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1},
                {1, 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1},
                {1, 0, 0, 1, 1, 0, 1, 0, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1},
                {1, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 1, 0, 0, 1},
                {1, 0, 1, 1, 0, 1, 1, 0, 1, 0, 0, 1, 0, 1, 1, 1, 1, 0, 0, 1},
                {1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
                {1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            };
            level[pos1[1], pos1[0]] = 9;
            level[pos2[1], pos2[0]] = 8;
            return level;
        }
        public static void DrawLevel(int[] pos1, int[] pos2, List<int[]> path, int[,] level, int minX = 0, int minY = 0, int maxWidth = 0, int maxHeight = 0)
        {
            if (maxWidth == 0) { maxWidth = level.GetLength(1); }
            if (maxHeight == 0) { maxHeight = level.GetLength(0); }
            for (int height = minY; height < maxHeight; height++)
            {
                for (int width = minX; width < maxWidth; width++)
                {
                    bool isPath = false;
                    for (int i = 0; i < path.Count; i++)
                    {
                        if (AreEqual(path[i], [width, height]))
                        {
                            isPath = true;
                            break;
                        }
                    }
                    if (AreEqual(pos1, [width, height]))
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.Write("  ");
                        Console.ResetColor();
                    }
                    else if (AreEqual(pos2, [width, height]))
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write("  ");
                        Console.ResetColor();
                    }
                    else if (isPath)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.Write("  ");
                        Console.ResetColor();
                    }
                    else
                    {
                        switch (level[height, width])
                        {
                            case 0: Console.Write("  "); break;
                            case 1: Console.Write("x "); break;
                        }
                    }
                }
                Console.WriteLine();
            }
        }
        public static List<int[]> FindPath(ref List<int[]> path, int[,] level, int[] start, int[] end, double dist = 0)
        {
            int i = 0;
            List<List<int[]>> visited = new List<List<int[]>>();
            List<List<int[]>> queued = new List<List<int[]>>();
            queued.Add(new List<int[]> { start });
            while (queued.Count != 0 && i < 100000)
            {
                if (i > 0)
                {
                }
                List<int[]> currPath = queued.First();

                queued.Remove(queued.First());
                i++;
                if (AreEqual(currPath.Last(), end))
                {
                    Console.WriteLine("found");
                    queued.Clear();
                }
                else if (visited.Count != 0 && indexOf(visited, currPath.Last()) > -1)
                {
                    continue;
                }
                else if (level[currPath.Last()[1], currPath.Last()[0]] == 1)
                {
                    continue;
                }
                else
                {
                    List<int[]> path1 = new List<int[]>(currPath);
                    List<int[]> path2 = new List<int[]>(currPath);
                    List<int[]> path3 = new List<int[]>(currPath);
                    List<int[]> path4 = new List<int[]>(currPath);
                    path1.Add([currPath.Last()[0] + 0, currPath.Last()[1] + 1]);
                    queued.Add(path1);
                    path2.Add([currPath.Last()[0] + 0, currPath.Last()[1] - 1]);
                    queued.Add(path2);
                    path3.Add([currPath.Last()[0] + 1, currPath.Last()[1] + 0]);
                    queued.Add(path3);
                    path4.Add([currPath.Last()[0] - 1, currPath.Last()[1] + 0]);
                    queued.Add(path4);
                }
                visited.Add(currPath);
                    Console.Clear();
                    DrawLevel(start, end, visited.Last(), level);
                    Console.WriteLine(visited.Count);
                    System.Threading.Thread.Sleep(100);
            }
            return visited.Last();
        }
    }
}
