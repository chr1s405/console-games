//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ConsoleGames
//{
//    internal class GravityGame
//    {
//        static List<LevelCell> levelCells = new List<LevelCell>{
//                new LevelCell(2, "  ", ConsoleColor.Green),
//            };
//        public static void Play()
//        {
//            Level level = new Level(32, 11);
//            List<int[]> tileSets = new List<int[]>();
//            int[] player = new int[] { 10, level.Height - 5, 1, 0 };
//            int score = 0;
//            int speed = 100;
//            bool isEnd = false;
//            while (!isEnd)
//            {
//                AddTileSet(level, tileSets, score);
//                MoveObstacles(tileSets);
//                Move(level, player);
//                Jump(player);
//                if (isDead(player, level))
//                {
//                    isEnd = true;
//                }
//                level.Reset();
//                for (int i = 0; i < tileSets.Count(); i++)
//                {
//                    level.Edit(tileSets[i][0], tileSets[i][1], tileSets[i][2]);
//                }
//                level.Edit(player, 2);
//                level.Draw(levelCells);
//                Console.WriteLine(score);
//                System.Threading.Thread.Sleep(speed);
//                score++;
//                if (speed > 20)
//                {
//                    if (score % 100 == 0)
//                    {
//                        speed -= 10;
//                    }
//                }
//            }
//        }
//        public static void Move(Level level, int[] player)
//        {
//            player[1] += player[2];
//            if (level.LevelAdpt[player[1], player[0]] == 1)
//            {
//                player[1] -= player[2];
//                player[3] = 1;
//            }
//            else
//            {
//                player[3] = 0;
//            }
//            if (level.LevelAdpt[player[1], player[0] + 1] == 1)
//            {
//                player[0]--;
//            }
//        }
//        public static void Jump(int[] player)
//        {
//            if (Console.KeyAvailable)
//            {
//                ConsoleKey key;
//                do
//                {
//                    key = Console.ReadKey(true).Key;
//                } while (Console.KeyAvailable);
//                if (player[3] == 1)
//                {
//                    if (key == ConsoleKey.Spacebar || key == ConsoleKey.UpArrow)
//                    {
//                        player[2] = -player[2];
//                        player[3] = 0;
//                    }
//                }

//            }
//        }
//        public static bool isDead(int[] player, Level level)
//        {
//            return player[0] < 1 || player[1] < 1 || player[1] > level.Height - 2;
//        }
//        public static void MoveObstacles(List<int[]> tileSets)
//        {
//            for (int i = 0; i < tileSets.Count; i++)
//            {
//                tileSets[i][0]--;
//                if (tileSets[i][0] == 0)
//                {
//                    tileSets.RemoveAt(i);
//                    i--;
//                }
//            }
//        }
//        public static void AddTileSet(Level level, List<int[]> tileSets, int score)
//        {
//            Random rand = new Random();
//            if (score % 35 == 0)
//            {
//                tileSets.AddRange(CreateTileSet(level, rand.Next(6)));
//            }
//        }
//        public static List<int[]> CreatePlatform(Level level, int id, int width, int x, int y)
//        {
//            List<int[]> platform = new List<int[]>();
//            for (int i = 0; i < width; i++)
//            {
//                platform.Add(new int[] { level.Width + x + i, y, id });
//            }
//            return platform;
//        }
//        public static List<int[]> CreateTileSet(Level level, int id)
//        {
//            List<int[]> tileSet = new List<int[]>();
//            Random rand = new Random();
//            if (id == 0)
//            {
//                int height;
//                for (int i = 0; i < rand.Next(5, 20); i++)
//                {
//                    height = rand.Next(level.Height);
//                    for (int j = 0; j < rand.Next(5, 10); j++)
//                    {
//                        if (height == 0 || height == level.Height - 1)
//                        {
//                            tileSet.Add(new int[] { level.Width + i + j, height, 0 });
//                        }
//                        else
//                        {
//                            tileSet.Add(new int[] { level.Width + i + j, height, 1 });
//                        }
//                    }
//                }
//            }
//            if (id == 1)
//            {
//                //xxxxxxxxxx                    xxxxxxxxxx//
//                //                                        //
//                //          xxxxxxxxxxxxxxxxxxxx          //
//                //                                        //
//                //xxxxxxxxxx                    xxxxxxxxxx//
//                tileSet.AddRange(CreatePlatform(level, 0, 20, 10, 0));
//                tileSet.AddRange(CreatePlatform(level, 0, 20, 10, level.Height - 1));

//                tileSet.AddRange(CreatePlatform(level, 1, 20, 10, (level.Height - 1) / 2));
//            }
//            if (id == 2)
//            {
//                //xxxxxxxxxx                         xxxxx//
//                //                                        //
//                //              xxxxxxxxxxxxxxxxx         //
//                //                                        //
//                //              xxxxxxxxxxxxxxxxx         //
//                //                                        //
//                //xxxxxxxxxx                         xxxxx//
//                tileSet.AddRange(CreatePlatform(level, 0, 25, 10, 0));
//                tileSet.AddRange(CreatePlatform(level, 0, 25, 10, level.Height - 1));

//                tileSet.AddRange(CreatePlatform(level, 1, 18, 14, (level.Height - 1) / 2 + 2));
//                tileSet.AddRange(CreatePlatform(level, 1, 18, 14, (level.Height - 1) / 2 - 2));
//            }
//            if (id == 3)
//            {
//                //xxxxxxxxxx                         xxxxx//
//                //                                        //
//                //               xxxxx     xxxxx          //
//                //                                        //
//                //          xxxxx     xxxxx     xxxxx     //
//                //                                        //
//                //xxxxxxxxxx                         xxxxx//
//                tileSet.AddRange(CreatePlatform(level, 0, 30, 5, 0));
//                tileSet.AddRange(CreatePlatform(level, 0, 30, 5, level.Height - 1));

//                tileSet.AddRange(CreatePlatform(level, 1, 5, 15, (level.Height - 1) / 2 + 2));
//                tileSet.AddRange(CreatePlatform(level, 1, 5, 25, (level.Height - 1) / 2 + 2));
//                tileSet.AddRange(CreatePlatform(level, 1, 5, 10, (level.Height - 1) / 2 - 2));
//                tileSet.AddRange(CreatePlatform(level, 1, 5, 20, (level.Height - 1) / 2 - 2));
//                tileSet.AddRange(CreatePlatform(level, 1, 5, 30, (level.Height - 1) / 2 - 2));
//            }
//            if (id == 4)
//            {
//                //xxxxxxxxxx                    xxxxxxxxxx//
//                //                                        //
//                //            xxxxx                       //
//                //                                        //
//                //                xxxxx                   //
//                //                                        //
//                //                    xxxxx               //
//                //                                        //
//                //                        xxxxx           //
//                //                                        //
//                //xxxxxxxxxx                    xxxxxxxxxx//
//                tileSet.AddRange(CreatePlatform(level, 0, 20, 10, 0));
//                tileSet.AddRange(CreatePlatform(level, 0, 20, 10, level.Height - 1));
//                for (int i = 0; i < (level.Height - 1) / 2; i++)
//                {
//                    tileSet.AddRange(CreatePlatform(level, 1, 5, 12 + i * 4, 2 + 2 * i));
//                }
//            }
//            if (id == 5)
//            {
//                //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx//
//                //      x        xxxxxxxxxx        x      //
//                //      xxxx                    xxxx      //
//                //               xxxxxxxxxx               //
//                //      xxxx                    xxxx      //
//                //      x        xxxxxxxxxx        x      //
//                //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx//
//                tileSet.AddRange(CreatePlatform(level, 1, 1, 6, 0));
//                tileSet.AddRange(CreatePlatform(level, 1, 1, 6, 1));
//                tileSet.AddRange(CreatePlatform(level, 1, 1, 6, 2));
//                tileSet.AddRange(CreatePlatform(level, 1, 4, 6, 3));
//                tileSet.AddRange(CreatePlatform(level, 1, 1, 6, level.Height - 1));
//                tileSet.AddRange(CreatePlatform(level, 1, 1, 6, level.Height - 2));
//                tileSet.AddRange(CreatePlatform(level, 1, 1, 6, level.Height - 3));
//                tileSet.AddRange(CreatePlatform(level, 1, 4, 6, level.Height - 4));
//                tileSet.AddRange(CreatePlatform(level, 1, 1, 33, 0));
//                tileSet.AddRange(CreatePlatform(level, 1, 1, 33, 1));
//                tileSet.AddRange(CreatePlatform(level, 1, 1, 33, 2));
//                tileSet.AddRange(CreatePlatform(level, 1, 4, 30, 3));
//                tileSet.AddRange(CreatePlatform(level, 1, 1, 33, level.Height - 1));
//                tileSet.AddRange(CreatePlatform(level, 1, 1, 33, level.Height - 2));
//                tileSet.AddRange(CreatePlatform(level, 1, 1, 33, level.Height - 3));
//                tileSet.AddRange(CreatePlatform(level, 1, 4, 30, level.Height - 4));

//                tileSet.AddRange(CreatePlatform(level, 1, 10, 15, (level.Height - 1) / 2));
//                tileSet.AddRange(CreatePlatform(level, 1, 10, 15, (level.Height - 1) / 2 + 4));
//                tileSet.AddRange(CreatePlatform(level, 1, 10, 15, (level.Height - 1) / 2 - 4));
//            }
//            if (rand.Next(2) == 0)
//            {
//                switch (id)
//                {
//                    case 3:
//                    case 4:
//                        for (int i = 0; i < tileSet.Count; i++)
//                        {
//                            tileSet[i][1] = level.Height - 1 - tileSet[i][1];
//                        }; break;
//                }

//            }
//            return tileSet;
//        }
//    }
//}
