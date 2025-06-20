using System.Runtime.CompilerServices;
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
                choice = 0;
                Console.WriteLine("MENU");
                Console.WriteLine("1. GravityGame");
                Console.WriteLine("2. Snake");
                Console.WriteLine("3. PathFinding");
                Console.WriteLine("4. PathMapping");
                Console.WriteLine("0. quit");
                Console.Write("Select option: ");
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                }
                catch { }
                if (choice != 0)
                {
                    Console.Clear();
                    switch (choice)
                    {
                        //case 1: GravityGame.Play(); break;
                        case 2: SnakeGame.Play(); break;
                        case 3: Pathfinding.Play(); break;
                        case 4: PathMapping.Play(); break;
                        case -1: TryingThingsOut(); break;
                    }
                    Console.WriteLine($"Press enter to continue");
                    Console.ReadLine();
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Thanks for playing");
                }
            } while (choice != 0);
        }
        static public void TryingThingsOut()
        {
            Console.WriteLine("=========================");
            Console.WriteLine("EXPERIMENTING & en testig");
            Console.WriteLine("=========================");
            Level level1 = new Level(10, 5, null);
            Level level2 = new Level(new Matrix(10, 5));
            Matrix matrix = new Matrix(new int[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            Level level3 = new Level(matrix);
            Console.WriteLine("level");
            level1.Draw();
            level2.Draw();
            level3.Draw();
            Console.Write("press to view level edit ");
            Console.ReadLine();
            level1.Edit(8, 2, 1);
            level2.Edit(new List<Point2I> {(0,0),(level2.Width-1, 0),(0,level2.Height-1), (level2.Width - 1, level2.Height - 1) }, 1);

            Console.Write("press to continue ");
            Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("matrix");
            Console.WriteLine(matrix);
            Console.WriteLine("index:");
            Console.WriteLine($"[2,0]: {matrix[2, 0]}");
            matrix[2, 0] = 9;
            Console.WriteLine($"[2,0] => 9");
            Console.WriteLine(matrix);
            Console.WriteLine("transpose:");
            matrix.Transpose();
            Console.WriteLine(matrix);
        }
    }
}
