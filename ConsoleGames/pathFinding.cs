using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static ConsoleGames.HelperMethods;


namespace ConsoleGames
{
    class pathFinding
    {
        public static void PathFind()
        {
            int[] pos1 = new int[] { 10, 2 };
            int[] pos2 = new int[] { 15, 5 };
            Level level = CreateLevel();
            List<int[]> path = new List<int[]>();
            path.Add(pos1);
            path = FindPath(ref path, level, pos1, pos2);
            for (int i = 0; i < path.Count; i++)
            {
                for (int j = 0; j < path[i].GetLength(0); j++)
                {
                    Console.Write(path[i][j] + " ");
                }
                Console.WriteLine();
            }


        }
        public static Level CreateLevel()
        {
            return new Level(
            new int[,]{
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
            });
        }
        
        public static List<int[]> FindPath(ref List<int[]> path, Level level, int[] start, int[] end, double dist = 0)
        {
            int counter = 0;
            List<List<int[]>> visited = new List<List<int[]>>();
            List<List<int[]>> queued = new List<List<int[]>>();
            queued.Add(new List<int[]> { start });
            while (queued.Count != 0)
            {
                counter++;
                if (counter > 1000)
                {
                    return [];
                }
                List<int[]> currPath = queued.First();
                queued.Remove(queued.First());
                if (currPath.Last()[0] == end[0] && currPath.Last()[1] == end[1])
                {
                    Console.WriteLine("found");
                    queued.Clear();
                }
                else if (visited.Count != 0 && isPresent(visited, currPath.Last()))
                {
                    continue;
                }
                else if (level.GetLevel[currPath.Last()[1], currPath.Last()[0]] == 1)
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
                level.Draw(start, end, visited.Last());
                System.Threading.Thread.Sleep(100);
            }
            return visited.Last();
        }
        public static bool isPresent(List<List<int[]>> list, int[] arr)
        { 
            for (int i = 0; i < list.Count; i++)
            {
                bool matches = true;
                for (int j = 0; j < arr.GetLength(0); j++)
                {
                    if (list[i].Last()[j] != arr[j])
                    {
                        matches = false;
                    }
                }
                if (matches)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
