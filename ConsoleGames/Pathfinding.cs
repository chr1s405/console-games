//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Utils;


//namespace ConsoleGames
//{
//    class Pathfinding : Game
//    {
//        Level level;
//        Point2I start;
//        Point2I end;
//        List<Point2I> path = new List<Point2I>();
//        public Pathfinding()
//        {
//            level = CreateLevel();
//            start = new Point2I(10, 2);
//            end = new Point2I(15, 5);
//        }
//        public override void Initialize()
//        {
//            level.Draw();
//            MyConsole.Draw(start, "  ", ConsoleColor.Green);
//            MyConsole.Draw(end, "  ", ConsoleColor.Red);
//            path = FindPath(level, start, end, true);
//            GameOver = true;
//        }
//        public static List<Point2I> FindPath(Level level, Point2I start, Point2I end, bool showPaths = false)
//        {
//            int counter = 0;
//            List<Point2I> visited = new List<Point2I>();
//            List<List<Point2I>> queued = new List<List<Point2I>>();
//            queued.Add(new List<Point2I> { start });
//            List<Point2I> prevPath = new List<Point2I>();
//            while (queued.Count != 0)
//            {
//                counter++;
//                if (counter > 1000)
//                {
//                    return [];
//                }
//                List<Point2I> currPath = queued.First();
//                queued.Remove(queued.First());
//                if (currPath.Last() == end)
//                {
//                    queued.Clear();
//                    return currPath;
//                }
//                else if (visited.Count != 0 && visited.Contains(currPath.Last()))
//                {
//                    continue;
//                }
//                else if (level[currPath.Last()] == 1)
//                {
//                    continue;
//                }
//                else
//                {
//                    List<Point2I> path1 = new List<Point2I>(currPath);
//                    List<Point2I> path2 = new List<Point2I>(currPath);
//                    List<Point2I> path3 = new List<Point2I>(currPath);
//                    List<Point2I> path4 = new List<Point2I>(currPath);
//                    path1.Add(currPath.Last() + (0, 1));
//                    queued.Add(path1);
//                    path2.Add(currPath.Last() - (0, 1));
//                    queued.Add(path2);
//                    path3.Add(currPath.Last() + (1, 0));
//                    queued.Add(path3);
//                    path4.Add(currPath.Last() - (1, 0));
//                    queued.Add(path4);
//                }
//                visited.Add(currPath.Last());

//                if (showPaths)
//                {
//                    foreach (Point2I pos in prevPath)
//                    {
//                        if (!currPath.Contains(pos))
//                            MyConsole.Draw(pos, "  ");
//                    }
//                    for (int i = 0; i < currPath.Count; i++)
//                    {
//                        if (i == 0)
//                        { MyConsole.Draw(currPath[i], "  ", ConsoleColor.Green); }
//                        else
//                        { MyConsole.Draw(currPath[i], "  ", ConsoleColor.Gray); }
//                    }
//                    prevPath = currPath;
//                    System.Threading.Thread.Sleep(20);
//                }
//            }
//            return [];
//        }
//        public static Level CreateLevel()
//        {
//            return new Level(
//            new int[,]{
//                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
//                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1},
//                {1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1},
//                {1, 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1},
//                {1, 0, 0, 1, 1, 0, 1, 0, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1},
//                {1, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 1, 0, 0, 1},
//                {1, 0, 1, 1, 0, 1, 1, 0, 1, 0, 0, 1, 0, 1, 1, 1, 1, 0, 0, 1},
//                {1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
//                {1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1},
//                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
//            });
//        }
//    }
//}
