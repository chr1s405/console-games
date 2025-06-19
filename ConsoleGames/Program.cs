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
                        case 1: GravityGame.Play(); break;
                        case 2: SnakeGame.Play(); break;
                        case 3: Pathfinding.Play(); break;
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
            Console.WriteLine(new Point2I(2, 6));
            Console.WriteLine(new Point2I(2, 6) + (4, 2));
            Console.WriteLine(new Point2I(2, 6) - (4, 2));
            Console.WriteLine(new Point2I(2, 6) - Dir.left);
            Console.WriteLine(new Point2I(2, 6) - Dir.right);
            Console.WriteLine(new Point2I(2, 6) - Dir.up);
            Console.WriteLine(new Point2I(2, 6) + Dir.up);
        }
    }
}
