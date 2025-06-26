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
    internal class Level
    {
        private int[,] grid;
        private Camera camera = new Camera();
        private List<Point2I> restorePos = new List<Point2I>();
        private List<LevelCell> levelCells = new List<LevelCell>();
        //private int consoleX;
        private int consoleY;
        public int this[Point2I pos] { get => this[pos.x, pos.y]; }
        public int this[int x, int y] { get => grid[x, y]; }
        public int Width { get => grid.GetLength(0); }
        public int Height { get => grid.GetLength(1); }
        public Camera Camera { get => camera; }
        public List<Point2I> RestorePos { get => restorePos; set => restorePos = value; }

        public Level(int width, int height, Camera camera = null, List<LevelCell> cells = null)
            : this(cells)
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
            CameraSetup(camera);
        }
        public Level(int[,] level, Camera camera = null, List<LevelCell> cells = null)
            : this(cells)
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
            CameraSetup(camera);
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
        private void CameraSetup(Camera camera)
        {
            if (camera is null)
                camera = new Camera();
            this.camera.X = camera.X;
            this.camera.Y = camera.Y;
            this.Camera.Width = (camera.Width == 0 || camera.Width > this.Width) ? this.Width : camera.Width;
            this.Camera.Height = (camera.Height == 0 || camera.Height > this.Height) ? this.Height : camera.Height;
        }

        public void Draw() { Draw((Width / 2, Height / 2)); }
        public void Draw(Point2I center)
        {
            camera.Center(center, Width, Height);
            consoleY = Console.GetCursorPosition().Top;
            int minX = camera.X;
            int minY = camera.Y;
            int maxX = Math.Min(minX + camera.Width, Width);
            int maxY = Math.Min(minY + camera.Height, Height);
            for (int y = minY; y < maxY; y++)
            {
                for (int x = minX; x < maxX; x++)
                {
                    MyConsole.Draw((x - camera.X, consoleY + y - camera.Y), levelCells[this[x, y]]);
                }
            }
        }
        public void DrawRestore()
        {
            for (int i = restorePos.Count - 1; i >= 0; i--)
            {
                Point2I pos = restorePos[i];
                //if (camera.X < pos.x && pos.x < camera.X + camera.Width &&
                //   camera.Y < pos.y && pos.y < camera.Y + camera.Height)
                {
                    levelCells[this[pos]].DrawCell(pos.x, pos.y);
                }
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
