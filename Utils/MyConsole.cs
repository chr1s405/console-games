using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public struct LevelCell
    {
        private int cellId;
        private string cellString;
        private ConsoleColor cellColor;
        private ConsoleColor fontColor;
        public string CellString { get => cellString; }
        public ConsoleColor BgColor { get => cellColor; }
        public ConsoleColor FgColor { get => fontColor; }
        public LevelCell(int id, string cellString, ConsoleColor bgColor = ConsoleColor.Black, ConsoleColor fontColor = ConsoleColor.White)
        {
            this.cellId = id;
            this.cellString = cellString;
            this.cellColor = bgColor;
            this.fontColor = fontColor;
        }

        public void DrawCell(int x, int y)
        {
            MyConsole.Draw(x, y, cellString, cellColor, fontColor);
        }
    }
    static public class MyConsole
    {
        static public void Draw(int x, int y, string value, ConsoleColor bgColor = ConsoleColor.Black, ConsoleColor fgColor = ConsoleColor.White)
        {
            int lastCursorY = Console.CursorTop; // has to be the max cursory
            bool changeBgColor = Console.BackgroundColor != bgColor;
            bool changeFgColor = Console.ForegroundColor != fgColor;
            if (changeBgColor)
                Console.BackgroundColor = bgColor;
            if (changeFgColor)
                Console.ForegroundColor = fgColor;

            Console.SetCursorPosition(x * 2, y);
            Console.Write(value);

            if (changeBgColor || changeFgColor)
                Console.ResetColor();
            Console.SetCursorPosition(0, lastCursorY);
        }
        static public void Draw(Point2I pos, string value, ConsoleColor bgColor = ConsoleColor.Black, ConsoleColor fgColor = ConsoleColor.White)
        { Draw(pos.x, pos.y, value, bgColor, fgColor); }
        static public void Draw(Point2I pos, LevelCell cell)
        { Draw(pos, cell.CellString, cell.BgColor, cell.FgColor); }
    }
}
