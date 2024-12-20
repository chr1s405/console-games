using System.Runtime.CompilerServices;
using System.IO;
using System.Reflection;
using static ConsoleGames.Program;
using Microsoft.Win32.SafeHandles;

namespace ConsoleGames
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Snake.Play();
            pathFinding.PathFind();
        }
    }
}
