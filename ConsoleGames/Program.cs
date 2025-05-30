﻿using System.Runtime.CompilerServices;
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
                Console.WriteLine("1. GravityGame");
                Console.WriteLine("2. Snake");
                Console.WriteLine("3. PathFinding");
                //Console.WriteLine("4. PathMap");
                Console.WriteLine("0. quit");
                Console.Write("Select option: ");
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    choice = 0;
                }
                Console.Clear();
                switch (choice)
                {
                    case 1: GravityGame.Play(); break;
                    case 2: Snake.Play(); break;
                    case 3: Pathfinding.Play(); break;
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
