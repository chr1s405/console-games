using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace ConsoleGames
{
    public struct LevelCell
    {
        int cellId;
        string cellString;
        ConsoleColor cellColor;
        public LevelCell(int id, string s, ConsoleColor color = ConsoleColor.Black)
        {
            CellId = id;
            CellString = s;
            CellColor = color;
        }

        public int CellId { get => cellId; set => cellId = value; }
        public string CellString { get => cellString; set => cellString = value; }
        public ConsoleColor CellColor { get => cellColor; set => cellColor = value; }
    }
    internal class Level
    {
        private int width;
        private int height;
        private int[,] levelGrid;
        private List<LevelCell> levelCells = new List<LevelCell>();
        private int consoleX;
        private int consoleY;

        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        public int[,] LevelGrid { get => levelGrid; set => levelGrid = value; }
        public int ConsoleX { get => consoleX; set => consoleX = value; }
        public int ConsoleY { get => consoleY; set => consoleY = value; }
        public List<LevelCell> LevelCells { get => levelCells; set => levelCells = value; }

        public Level(int width, int height, List<LevelCell> cells = null)
        {
            Width = width;
            Height = height;
            InitLevel();
            LevelCells.Add(new LevelCell(0, "  "));
            LevelCells.Add(new LevelCell(1, "[]"));
            if(cells is not null)
            {
                foreach(LevelCell cell in cells)
                {
                    LevelCells.Add(cell);
                }
            }
        }
        public Level(int size) : this(size, size) { }
        public void InitLevel()
        {
            ConsoleX = Console.GetCursorPosition().Left;
            ConsoleY = Console.GetCursorPosition().Top;
            LevelGrid = new int[Width, Height];
            for (int y = 1; y < Height; y++)
            {
                for (int x = 1; x < Width; x++)
                {
                    LevelGrid[x, y] = 0;
                }
            }
            for (int i = 0; i < Width; i++) { LevelGrid[i, 0] = 1; }
            for (int i = 0; i < Width; i++) { LevelGrid[i, Height - 1] = 1; }
            for (int i = 0; i < Height; i++) { LevelGrid[0, i] = 1; }
            for (int i = 0; i < Height; i++) { LevelGrid[Width - 1, i] = 1; }
        }
        public void Draw()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    DrawPartial(x, y);
                }
                Console.WriteLine();
            }
        }
        public void DrawPartial(int x, int y)
        {
            int cellId = LevelGrid[x, y];
            Console.SetCursorPosition(ConsoleX + x*2, ConsoleY + y);
            try
            {
                Console.BackgroundColor = LevelCells[cellId].CellColor;
                Console.Write(LevelCells[cellId].CellString);
                Console.ResetColor();
            }
            catch(ArgumentOutOfRangeException e)
            {
                Console.Write("##");
            }
            Console.SetCursorPosition(0, ConsoleY + Height);
        }
        public void Edit(int x, int y, int value)
        {
            LevelGrid[x, y] = value;
            DrawPartial(x, y);
        }
        public void Edit(Point2I pos, int value)
        {
            Edit(pos.X, pos.Y, value);
        }
        public void EditMove(int destX, int destY, int currX, int currY, int coverValue = 0)
        {
            int value = LevelGrid[currX, currY];
            Edit(currX, currY, coverValue);
            Edit(destX, destY, value);
        }
        public void EditMove(Point2I destPos, Point2I currPos, int coverValue = 0)
        {
            int value = LevelGrid[currPos.X, currPos.Y];
            Edit(currPos, coverValue);
            Edit(destPos, value);
        }

        public void PrintLevel()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Console.Write(LevelGrid[x, y].ToString().PadRight(2));
                }
                Console.WriteLine();
            }
        }
    }
}
