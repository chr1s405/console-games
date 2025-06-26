using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Utils;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleGames
{

    //public struct LevelGrid
    //{
    //    private List<int>[,] grid;

    //    public int Width { get => grid.GetLength(0); }
    //    public int Height { get => grid.GetLength(1); }
    //    public List<int> this[int x, int y] { get => grid[x, y]; set => grid[x, y] = value; }
    //    public List<int> this[Point2I pos] { get => this[pos.x, pos.y]; set => this[pos.x, pos.y] = value; }

    //    public LevelGrid(int width, int height)
    //    {
    //        this.grid = new List<int>[width, height];
    //        for (int row = 0; row < height; row++)
    //        {
    //            for (int col = 0; col < width; col++)
    //            {
    //                if (row == 0 || row == height - 1 || col == 0 || col == width - 1)
    //                    this.grid[col, row] = new List<int> { 1 };
    //                else
    //                    this.grid[col, row] = new List<int> { 0 };
    //            }
    //        }
    //    }
    //    public LevelGrid(int[,] level)
    //    {
    //        int width = level.GetLength(1);
    //        int height = level.GetLength(0);
    //        this.grid = new List<int>[width, height];
    //        for (int row = 0; row < height; row++)
    //        {
    //            for (int col = 0; col < width; col++)
    //            {
    //                this.grid[col, row] = new List<int> { level[row, col] };
    //            }
    //        }
    //    }
    //    public LevelGrid(List<int>[,] level)
    //    {
    //        int width = level.GetLength(1);
    //        int height = level.GetLength(0);
    //        this.grid = new List<int>[width, height];
    //        for (int row = 0; row < height; row++)
    //        {
    //            for (int col = 0; col < width; col++)
    //            {
    //                this.grid[col, row] = new List<int>(level[col, row]);
    //            }
    //        }
    //    }
    //    public LevelGrid(LevelGrid level)
    //    {
    //        int width = level.Width;
    //        int height = level.Height;
    //        this.grid = new List<int>[width, height];
    //        for (int row = 0; row < height; row++)
    //        {
    //            for (int col = 0; col < width; col++)
    //            {
    //                this.grid[col, row] = new List<int>(level.grid[col, row]);
    //            }
    //        }
    //    }

    //    public void Add(Point2I pos, int value, int index = -1)
    //    {
    //        if (index == -1)
    //            this[pos].Add(value);
    //        else
    //            this[pos].Insert(index, value);
    //    }
    //    public void Remove(Point2I pos, int value)
    //    {
    //        this[pos].Remove(value);
    //    }
    //    public void RemoveAt(Point2I pos, int index)
    //    {
    //        this[pos].RemoveAt(index);
    //    }
    //    public void Move(Point2I pos, Point2I dest, int value, int newValue)
    //    {
    //        Remove(pos, value);
    //        Add(dest, newValue);
    //    }
    //    public void Edit(Point2I pos, int value, int newValue)
    //    {
    //        for (int i = 0; i < this[pos].Count; i++)
    //        {
    //            if (this[pos][i] == value)
    //            {
    //                this[pos][i] = newValue; return;
    //            }
    //        }
    //        throw new Exception($"{value} was not found in {ToString(pos.x, pos.y)}");
    //    }
    //    public void ToBottom(Point2I pos, int value)
    //    {
    //        this[pos].Insert(1, value);
    //        Remove(pos, value);
    //    }

    //    public string ToString(int x, int y)
    //    {
    //        List<int> list = grid[x, y];
    //        string listString = "";
    //        for (int i = 0; i < list.Count; i++)
    //        {
    //            listString += $"{list[i]}{(i == list.Count - 1 ? "" : ",")}";
    //        }
    //        return $"[{listString}]";
    //    }
    //    public override string ToString()
    //    {
    //        string text = "";
    //        int maxSize = 0;
    //        foreach (List<int> list in grid)
    //        {
    //            if (list.Count > maxSize)
    //                maxSize = list.Count;
    //        }
    //        text += "\n";
    //        for (int row = 0; row < Height; row++)
    //        {
    //            for (int i = 0; i < maxSize; i++)
    //            {
    //                int leftPad = i + 2;
    //                char leftPadChar = ' ';
    //                int rightPad = maxSize + 2;
    //                char rightPadChar = ' ';
    //                for (int col = 0; col < Width; col++)
    //                {
    //                    if (i > this[col, row].Count - 1)
    //                        text += ".".PadLeft(leftPad, leftPadChar).PadRight(rightPad, rightPadChar);
    //                    else
    //                        text += this[col, row][i].ToString().PadLeft(leftPad, leftPadChar).PadRight(rightPad, rightPadChar);
    //                    if (col < Width - 1)
    //                        text += "|";
    //                }
    //                text += "\n";
    //            }
    //            if (row < Height - 1)
    //            {
    //                for (int col = 0; col < Width; col++)
    //                {
    //                    text += "".PadRight(maxSize + 2, '-') + " ";
    //                }
    //                text += "\n";
    //            }
    //        }
    //        return text;
    //    }
    //}
    internal class Level
    {
        private int[,] grid;
        private List<LevelCell> levelCells = new List<LevelCell>();
        private List<Point2I> restorePos = new List<Point2I>();
        //private int consoleX;
        private int consoleY;
        public int this[Point2I pos] { get => this[pos.x, pos.y]; }
        public int this[int x, int y] { get => grid[x, y]; }
        //public int[,] Grid { get => grid; }
        public int Width { get => grid.GetLength(0); }
        public int Height { get => grid.GetLength(1); }
        public List<Point2I> RestorePos { get => restorePos; set => restorePos = value; }

        public Level(int width, int height, List<LevelCell> cells = null) : this(cells)
        {
            this.grid = new int[width, height];
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    if (row == 0 || row == height - 1 || col == 0 || col == width - 1)
                        this.grid[col, row] = 1;
                    else
                        this.grid[col, row] = 0;
                }
            }
        }
        public Level(int[,] level, List<LevelCell> cells = null) : this(cells)
        {
            int width = level.GetLength(1);
            int height = level.GetLength(0);
            this.grid = new int[width, height];
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    this.grid[col, row] = level[row, col];
                }
            }
        }
        private Level(List<LevelCell> cells = null)
        {
            levelCells.Add(new LevelCell(0, "  "));
            levelCells.Add(new LevelCell(1, "[]", ConsoleColor.DarkGray));
            if (cells is not null)
            {
                foreach (LevelCell cell in cells)
                {
                    levelCells.Add(cell);
                }
            }
            //this.consoleX = int.MinValue;
            this.consoleY = int.MinValue;
        }

        public void Draw()
        {
            //consoleX = Console.GetCursorPosition().Left;
            consoleY = Console.GetCursorPosition().Top;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Draw(x, y);
                }
            }
        }
        public void Draw(Point2I pos) { Draw(pos.x, pos.y); }
        public void Draw(int x, int y)
        {
            MyConsole.Draw((x, consoleY + y), levelCells[this[x, y]]);
        }
        public void DrawRestore()
        {
            for (int i = restorePos.Count - 1; i >= 0; i--)
            {
                Point2I pos = restorePos[i];
                levelCells[this[pos]].DrawCell(pos.x, pos.y);
                restorePos.RemoveAt(i);
            }
        }
        //public void Add(Point2I pos, int value)
        //{
        //    grid.Add(pos, value);
        //    if (consoleX >= 0 && consoleY >= 0)
        //        DrawPartial(pos);
        //}
        //public void Add(List<Point2I> pos, int value)
        //{
        //    for (int i = 0; i < pos.Count; i++)
        //    {
        //        Add(pos[i], value);
        //    }
        //}
        //public void AddToBottom(Point2I pos, int value)
        //{
        //    grid.Add(pos, value, 1);
        //    if (consoleX >= 0 && consoleY >= 0)
        //        DrawPartial(pos);
        //}
        //public void AddToBottom(List<Point2I> pos, int value)
        //{
        //    for (int i = 0; i < pos.Count; i++)
        //    {
        //        AddToBottom(pos[i], value);
        //    }
        //}
        //public void Remove(Point2I pos, int value)
        //{
        //    grid.Remove(pos, value);
        //    if (consoleX >= 0 && consoleY >= 0)
        //        DrawPartial(pos);
        //}
        //public void Remove(List<Point2I> pos, int value)
        //{
        //    for (int i = 0; i < pos.Count; i++)
        //    {
        //        Remove(pos[i], value);
        //    }
        //}
        //public void RemoveBottom(Point2I pos)
        //{
        //    grid.RemoveAt(pos, 1);
        //    if (consoleX >= 0 && consoleY >= 0)
        //        DrawPartial(pos);
        //}
        //public void Edit(Point2I pos, Point2I dest)
        //{
        //    Edit(pos, dest, grid[pos].Last());
        //}
        //public void Edit(Point2I pos, Point2I dest, int value)
        //{
        //    grid.Move(pos, dest, value, value);
        //    if (consoleX >= 0 && consoleY >= 0)
        //    { DrawPartial(pos); DrawPartial(dest); }
        //}
        //public void Edit(Point2I pos, Point2I dest, int value, int newValue)
        //{
        //    grid.Move(pos, dest, value, newValue);
        //    if (consoleX >= 0 && consoleY >= 0)
        //    { DrawPartial(pos); DrawPartial(dest); }

        //}
        //public void Edit(Point2I pos, int value)
        //{
        //    Edit(pos, grid[pos].Last(), value);
        //}
        //public void Edit(Point2I pos, int value, int newValue)
        //{
        //    grid.Edit(pos, value, newValue);
        //    if (consoleX >= 0 && consoleY >= 0)
        //        DrawPartial(pos);
        //}
        //public void ToTop(Point2I pos, int value)
        //{
        //    grid.Remove(pos, value);
        //    grid.Add(pos, value);
        //    if (consoleX >= 0 && consoleY >= 0)
        //        DrawPartial(pos);
        //}
        //public void ToBottom(Point2I pos, int value)
        //{
        //    grid.ToBottom(pos, value);
        //    if (consoleX >= 0 && consoleY >= 0)
        //        DrawPartial(pos);
        //}
        public override string ToString()
        {
            return grid.ToString();
        }
    }
}
