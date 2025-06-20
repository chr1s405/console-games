using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Utils;
using static ConsoleGames.PathMapping;
using static ConsoleGames.SnakeGame;
using static System.Formats.Asn1.AsnWriter;

namespace ConsoleGames
{
    internal class PathMapping
    {
        static List<LevelCell> levelCells = new List<LevelCell>{
                new LevelCell(2,"/\\"),
                new LevelCell(3,"=>"),
                new LevelCell(4,"\\/"),
                new LevelCell(5,"<="),
                new LevelCell(6,"^^", ConsoleColor.Green),
                new LevelCell(7,"°°", ConsoleColor.Red),
                new LevelCell(8,"--", ConsoleColor.Red),
            };
        public static void Play()
        {

            Level level = CreateLevel();
            Player player = new Player((9, 4));
            Enemy enemy = new Enemy(level);
            level.Draw();
            level.Edit(player.Pos, 6);
            Matrix paths = findPaths(level, player.Pos, false);
            bool isEnd = false;
            Console.WriteLine("Press enter to exit");
            Console.WriteLine(paths);
            while (!isEnd)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.LeftArrow) { player.Move(level, Dir.left); }
                    if (key == ConsoleKey.RightArrow) { player.Move(level, Dir.right); }
                    if (key == ConsoleKey.UpArrow) { player.Move(level, Dir.up); }
                    if (key == ConsoleKey.DownArrow) { player.Move(level, Dir.down); }
                    if (key == ConsoleKey.Enter) { isEnd = true; }
                    paths = findPaths(level, player.Pos, true);
                    System.Threading.Thread.Sleep(100);
                    enemy.Move(paths, level);
                }
                System.Threading.Thread.Sleep(100);
            }
        }
        public static Matrix findPaths(Level level, Point2I start, bool showPaths = false)
        {
            List<Point2I> queued = new List<Point2I> { start };
            List<Point2I> visited = new List<Point2I>(queued);
            Matrix paths = new Matrix(level.Width, level.Height);
            paths[start] = 0;
            int count = 0;
            while (queued.Count != 0 && count < 500)
            {
                count++;
                Point2I currPos = queued.First();
                queued.RemoveAt(0);
                for (int i = 0; i < 4; i++)
                {
                    Point2I nextPos = currPos + (Dir)i;
                    if (!visited.Contains(nextPos) && !new int[] { 1 }.Contains(level[nextPos]))
                    {
                        if (showPaths)
                            level.Edit(nextPos, ((i + 2) % 4) + 2);
                        paths[nextPos] = ((i + 2) % 4) + 1;
                        queued.Add(nextPos);
                        visited.Add(nextPos);
                    }
                }
            }
            return paths;
        }
        public struct Player
        {
            private Point2I pos;
            public Point2I Pos { get => pos; set => pos = value; }
            public Player(Point2I pos)
            {
                Pos = pos;
            }
            public void Move(Level level, Dir dir)
            {
                Point2I newPos = Pos + dir;
                if (level[newPos] != 1)
                {
                    level.EditMove(newPos, 6, Pos);
                    Pos += dir;
                }
            }
        }
        public struct Enemy
        {
            private Point2I pos;
            private int knockOut = 0;
            public Point2I Pos { get => pos; set => pos = value; }
            public Enemy(Level level)
            {
                Pos = GetRandomPosition(level);
                level.Edit(Pos, 7);
            }
            public void Move(Matrix pathMap, Level level)
            {
                if (knockOut == 0)
                {
                    Point2I newPos = Pos + (Dir)(pathMap[Pos] - 1);
                    level.EditMove(newPos, 7, Pos);
                    Pos = newPos;
                    //knockOut++;
                }
                else
                {
                    knockOut--;
                    level.Edit(Pos, 8);
                }
                if ((Pos + (Dir)(pathMap[Pos] - 1) == Pos)) { 
                    knockOut = 5;
                    Pos = GetRandomPosition(level);
                }
            }
            private Point2I GetRandomPosition(Level level)
            {
                Point2I pos;
                Random rand = new Random();
                int count = 0;
                do
                {
                    pos = (rand.Next(level.Width), rand.Next(level.Height));
                    count++;
                }
                while (level[pos] != 0 && count < 100);
                return pos;
            }
        }
        public static Level CreateLevel()
        {
            return new Level(
            new Matrix(new int[,]{
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1},
                {1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                {1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 1},
                {1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1},
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            }), levelCells);
        }
    }
}
