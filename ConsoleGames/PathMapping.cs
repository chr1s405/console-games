using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace ConsoleGames
{
    internal class PathMapping
    {
        static List<LevelCell> levelCells = new List<LevelCell>{
                new LevelCell(2,"  ", ConsoleColor.Green),
                new LevelCell(3,"\\/"),
                new LevelCell(4,"<="),
                new LevelCell(5,"/\\"),
                new LevelCell(6,"=>"),
            };
        public static void Play()
        {

            Level level = CreateLevel();
            Point2I start = new Point2I(9, 4);
            level.Draw();
            level.Edit(start, 2);
            findPaths(level, start, true);
        }
        public static Level CreateLevel()
        {
            return new Level(
            new Matrix(new int[,]{
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                {1, 0, 0, 0, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1},
                {1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1},
                {1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1},
                {1, 0, 1, 1, 1, 1, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1},
                {1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1},
                {1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            }), levelCells);
        }
        public static List<Point2I> findPaths(Level level, Point2I start, bool showPaths = false)
        {
            List<Point2I> queued = new List<Point2I> { start };
            List<Point2I> visited = new List<Point2I>(queued);
            int count = 0;
            while (queued.Count != 0 && count < 500)
            {
                count++;
                Point2I currPos = queued.First();
                queued.RemoveAt(0);
                for (int i = 0; i < 4; i++)
                {
                    Point2I nextPos = currPos + (Dir)i;
                    if (!visited.Contains(nextPos) && !new int[] { 1, i + 3 }.Contains(level[nextPos]))
                    {
                        if (showPaths)
                            level.Edit(nextPos, i + 3);
                        queued.Add(nextPos);
                        visited.Add(nextPos);
                    }
                }
                if (showPaths)
                    System.Threading.Thread.Sleep(20);
            }
            return [];
        }
    }
}
