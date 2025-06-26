using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace ConsoleGames
{
    internal class Camera
    {
        private int x;
        private int y;
        private int width;
        private int height;
        public Camera() : this(0, 0, 0, 0) { }
        public Camera(int width, int height) : this(0, 0, width, height) { }
        public Camera(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }

        public void Center(Point2I pos, int maxWidth, int maxHeight)
        {
            x = Math.Max(0, Math.Min(pos.x - width / 2, maxWidth - width));
            y = Math.Max(0, Math.Min(pos.y - height / 2, maxHeight - height));
        }
    }
}
