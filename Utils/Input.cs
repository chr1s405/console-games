using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class Input
    {
        static public ConsoleKey Key { 
            get
            {
                if (Console.KeyAvailable)
                {
                    return Console.ReadKey(true).Key;
                }
                return ConsoleKey.None;
            }
        }
    }
}
