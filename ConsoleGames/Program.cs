﻿using System.Runtime.CompilerServices;
using System.IO;
using System.Reflection;
using Microsoft.Win32.SafeHandles;
using System.Xml.Serialization;
using Utils;

namespace ConsoleGames
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int choice;
            do
            {
                Console.WriteLine("MENU");
                Console.WriteLine("=== Games ===");
                Console.WriteLine("1. Snake");
                //Console.WriteLine("2. GravityGame");
                Console.WriteLine();
                Console.WriteLine("=== Concepts ===");
                Console.WriteLine("3. PathFinding");
                Console.WriteLine("4. PathMapping");
                Console.WriteLine("");
                Console.WriteLine("0. quit");
                Console.Write("Select option: ");

                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                }
                catch { choice = 0; }
                switch (choice)
                {
                    case 1: Play(new SnakeGame()); break;
                    //case 2: Play(new GravityGame()); break;
                    case 3: Play(new Pathfinding()); break;
                    case 4: Play(new PathMapping()); break;
                    case -1: TryingThingsOut(); break;
                }
                Console.WriteLine($"Press enter to continue");
                Console.ReadLine();
                Console.Clear();
            } while (choice != 0);
            Console.WriteLine("Thanks for playing");
        }
        static public void Play(Game game)
        {
            game.Initialize();
            while (!game.GameOver)
            {
                game.Update();
                game.Draw();
                System.Threading.Thread.Sleep(100);
            }
        }
        static public void TryingThingsOut()
        {
            Console.WriteLine("=========================");
            Console.WriteLine("EXPERIMENTING & en testig");
            Console.WriteLine("=========================");

            Console.WriteLine("matrix");
            Matrix matrix = new Matrix(new int[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            Console.WriteLine(matrix);
            Console.WriteLine("index:");
            Console.WriteLine($"[2,0]: {matrix[2, 0]}");
            matrix[2, 0] = 9;
            Console.WriteLine($"[2,0] => 9");
            Console.WriteLine(matrix);
            Console.WriteLine("transpose:");
            matrix.Transpose();
            Console.WriteLine(matrix);

            //Console.WriteLine("level");
            //Level level1 = new Level(10, 5, null);
            //Level level2 = new Level(new Matrix(10, 5));
            //Level level3 = new Level(matrix);
            //level1.Draw();
            //level2.Draw();
            //level3.Draw();
            //Console.Write("press to view level edit ");
            //Console.ReadLine();
            //level1.Edit(8, 2, 1);
            //level2.Edit(new List<Point2I> {(0,0),(level2.Width-1, 0),(0,level2.Height-1), (level2.Width - 1, level2.Height - 1) }, 1);
            //Console.Write("press to continue ");
            //Console.ReadLine();
            //Console.WriteLine();


        }
    }
}
