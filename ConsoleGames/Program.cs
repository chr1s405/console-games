using System.Runtime.CompilerServices;
using System.IO;
using System.Reflection;
using static ConsoleGames.Program;
using Microsoft.Win32.SafeHandles;
using System.Xml.Serialization;

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
                Console.WriteLine("1. Snake");
                Console.WriteLine("2. PathFinding");
                Console.WriteLine("0. quit");
                Console.Write("Select option: ");
                choice = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                switch (choice)
                {
                    case 1: Snake.Play(); break;
                    case 2: pathFinding.PathFind(); break;
                }
                if (choice != 0)
                {
                    Console.WriteLine($"Press enter to continue");
                    Console.ReadLine();
                    Console.Clear();
                }
            } while (choice != 0);
        }
    }
}
