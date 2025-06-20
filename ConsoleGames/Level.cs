using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
        private Matrix levelGrid = new Matrix();
        private List<LevelCell> levelCells = new List<LevelCell>();
        private int consoleX;
        private int consoleY;

        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        public Matrix LevelGrid { get => levelGrid; set => levelGrid = value; }
        public int ConsoleX { get => consoleX; set => consoleX = value; }
        public int ConsoleY { get => consoleY; set => consoleY = value; }
        public List<LevelCell> LevelCells { get => levelCells; set => levelCells = value; }
        public int this[int x, int y] { get => levelGrid[x, y]; set => levelGrid[x, y] = value; }
        public int this[Point2I pos] { get => levelGrid[pos.x, pos.y]; set => levelGrid[pos.x, pos.y] = value; }

        private Level(List<LevelCell> cells = null)
        {
            LevelCells.Add(new LevelCell(0, "  "));
            LevelCells.Add(new LevelCell(1, "[]", ConsoleColor.DarkGray));
            if (cells is not null)
            {
                foreach (LevelCell cell in cells)
                {
                    LevelCells.Add(cell);
                }
            }
            ConsoleX = int.MinValue;
            ConsoleY = int.MinValue;
        }
        public Level(Matrix level, List<LevelCell> cells = null) : this(cells)
        {
            LevelGrid = new Matrix(level);
            Width = LevelGrid.GetWidth();
            Height = levelGrid.GetHeight();
        }
        public Level(int width, int height, List<LevelCell> cells = null) : this(cells)
        {
            Width = width;
            Height = height;
            LevelGrid = InitLevel(width, height);
        }
        public Matrix InitLevel(int size) { return InitLevel(size, size); }
        public Matrix InitLevel(int width, int height)
        {
            Matrix matrix = new Matrix(new int[height, width]);
            for (int y = 1; y < height; y++)
            {
                for (int x = 1; x < width; x++)
                {
                    matrix[x, y] = 0;
                }
            }
            for (int i = 0; i < Width; i++) { matrix[i, 0] = 1; }
            for (int i = 0; i < Width; i++) { matrix[i, Height - 1] = 1; }
            for (int i = 0; i < Height; i++) { matrix[0, i] = 1; }
            for (int i = 0; i < Height; i++) { matrix[Width - 1, i] = 1; }
            return matrix;
        }
        public void Draw()
        {
            ConsoleX = Console.GetCursorPosition().Left;
            ConsoleY = Console.GetCursorPosition().Top;
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
            int cursorPos = Console.CursorTop;
            Console.SetCursorPosition(ConsoleX + x * 2, ConsoleY + y);
            int cellId = LevelGrid[x, y];
            try
            {
                Console.BackgroundColor = LevelCells[cellId].CellColor;
                Console.Write(LevelCells[cellId].CellString);
                Console.ResetColor();
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Write("##");
            }
            Console.SetCursorPosition(0, consoleX + Height);
        }
        public void Edit(int x, int y, int value)
        {
            this[x, y] = value;
            if (ConsoleX >= 0 && ConsoleY >= 0)
            {
                DrawPartial(x, y);
            }
        }
        public void Edit(Point2I pos, int value)
        {
            Edit(pos.x, pos.y, value);
        }
        public void Edit(List<Point2I> pos, int value)
        {
            for (int i = 0; i < pos.Count; i++)
            {
                Edit(pos[i], value);
            }
        }
        public void EditMove(Point2I destPos, Point2I currPos, int currValue = 0)
        {
            EditMove(destPos, LevelGrid[currPos], currPos, currValue);
        }
        public void EditMove(Point2I destPos, int destValue, Point2I currPos, int currValue = 0)
        {
            Edit(destPos, destValue);
            Edit(currPos, currValue);
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
        public override string ToString()
        {
            string levelString = "";
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    levelString += LevelGrid[x, y].ToString().PadRight(2);
                }
                levelString += "\n";
            }
            return levelString;
        }
    }
}
