//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection.Emit;
//using System.Text;
//using System.Threading.Tasks;
//using Utils;
//using static ConsoleGames.PathMapping;
//using static ConsoleGames.SnakeGame;
//using static System.Formats.Asn1.AsnWriter;

//namespace ConsoleGames
//{
//    internal class PathMapping : Game
//    {
//        Level level;
//        Player player;
//        Enemy enemy1;
//        Enemy enemy2;
//        Enemy enemy3;
//        Dir[,] pathMap;
//        bool hasMoved;
//        public PathMapping()
//        {
//            level = CreateLevel();
//            player = new Player((9, 4));
//            enemy1 = new Enemy(level);
//            enemy2 = new Enemy(level);
//            enemy3 = new Enemy(level);
//            pathMap = findPaths(level, player.Pos);
//            hasMoved = true;
//        }
//        public override void Initialize()
//        {
//            level.Draw();
//            for (int row = 0; row < pathMap.GetLength(1); row++)
//            {
//                for (int col = 0; col < pathMap.GetLength(0); col++)
//                {
//                    switch (pathMap[col, row])
//                    {
//                        case Dir.up: MyConsole.Draw(col, row, "/\\", ConsoleColor.Gray); break;
//                        case Dir.right: MyConsole.Draw(col, row, "=>", ConsoleColor.Gray); break;
//                        case Dir.down: MyConsole.Draw(col, row, "\\/", ConsoleColor.Gray); break;
//                        case Dir.left: MyConsole.Draw(col, row, "<=", ConsoleColor.Gray); break;
//                    }
//                }
//            }
//        }
//        public override void Update()
//        {
//            enemy1.Move(pathMap, level);
//            enemy2.Move(pathMap, level);
//            enemy3.Move(pathMap, level);
//            if (enemy1.Pos == player.Pos && enemy2.Pos == player.Pos && enemy3.Pos == player.Pos)
//            {
//                GameOver = true;
//            }
//        }
//        public override void Draw()
//        {
//            for (int i = level.RestorePos.Count - 1; i >= 0; i--)
//            {
//                Point2I pos = level.RestorePos[i];
//                switch (pathMap[pos.x, pos.y])
//                {
//                    case Dir.up: MyConsole.Draw(pos.x, pos.y, "/\\", ConsoleColor.Gray); break;
//                    case Dir.right: MyConsole.Draw(pos.x, pos.y, "=>", ConsoleColor.Gray); break;
//                    case Dir.down: MyConsole.Draw(pos.x, pos.y, "\\/", ConsoleColor.Gray); break;
//                    case Dir.left: MyConsole.Draw(pos.x, pos.y, "<=", ConsoleColor.Gray); break;
//                }
//                level.RestorePos.RemoveAt(level.RestorePos.Count - 1);
//            }
//            enemy1.Draw();
//            enemy2.Draw();
//            enemy3.Draw();
//            player.Draw();
//        }
//        public static Dir[,] findPaths(Level level, Point2I start)
//        {
//            List<Point2I> queued = new List<Point2I> { start };
//            List<Point2I> visited = new List<Point2I>(queued);
//            Dir[,] pathMap = new Dir[level.Width, level.Height];
//            int count = 0;
//            while (queued.Count != 0 && count < 500)
//            {
//                count++;
//                Point2I currPos = queued.First();
//                queued.RemoveAt(0);
//                for (int i = 0; i < 4; i++)
//                {
//                    Point2I nextPos = currPos + (Dir)(i + 1);
//                    if (!visited.Contains(nextPos) && !(level[nextPos] == 1))
//                    {
//                        pathMap[nextPos.x, nextPos.y] = (Dir)(((i + 2) % 4) + 1);
//                        queued.Add(nextPos);
//                        visited.Add(nextPos);
//                    }
//                }
//            }
//            return pathMap;
//        }
//        public struct Player
//        {
//            private Point2I pos;
//            public Point2I Pos { get => pos; set => pos = value; }
//            public Player(Point2I pos)
//            {
//                Pos = pos;
//            }
//            public void Move(Level level, Dir dir)
//            {
//                Point2I newPos = Pos + dir;
//                if (!(level[newPos] == 1))
//                {
//                    Pos += dir;
//                }
//            }
//            public void Draw()
//            {
//                MyConsole.Draw(Pos, "^^", ConsoleColor.Green);
//            }
//        }
//        public struct Enemy
//        {
//            private Point2I pos;
//            public Point2I Pos { get => pos; set => pos = value; }
//            public Enemy(Level level)
//            {
//                Pos = GetRandomPosition(level);
//            }
//            public void Move(Dir[,] pathMap, Level level)
//            {
//                if (pathMap[Pos.x, Pos.y] != Dir.none)
//                {
//                    level.RestorePos.Add(Pos);
//                    Pos += pathMap[Pos.x, Pos.y];
//                }
//            }
//            public void Draw()
//            {
//                MyConsole.Draw(Pos, "°°", ConsoleColor.Red);
//            }
//            private Point2I GetRandomPosition(Level level)
//            {
//                Point2I pos;
//                Random rand = new Random();
//                int count = 0;
//                do
//                {
//                    pos = (rand.Next(level.Width), rand.Next(level.Height));
//                    count++;
//                }
//                while (!(level[pos] == 0) && count < 100);
//                return pos;
//            }
//        }
//        public static Level CreateLevel()
//        {
//            return new Level(
//            new int[,]{
//                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
//                {1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
//                {1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 1},
//                {1, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
//                {1, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 1},
//                {1, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 0, 0, 0, 1},
//                {1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
//                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1},
//                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
//            });
//        }
//    }
//}
