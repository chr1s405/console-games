using Utils;

namespace Snake
{
    internal class Snake
    {
        private List<Point2I> body = new List<Point2I>();
        private Dir direction;
        public List<Point2I> Body { get => body; }
        public Dir Direction { get => direction; set => direction = value; }
        public Snake()
        {
            Body.Add((2, 2));
            Body.Add((1, 2));
            direction = Dir.right;
        }
        public void Move(Level level, Point2I coin)
        {
            Body.Insert(0, Body[0] + direction);
            if (!(Body[0] == coin))
            {
                Body.RemoveAt(Body.Count - 1);
            }
            if (Body[0].x == 0) { Body[0] = (level.Width - 2, Body[0].y); }
            if (Body[0].y == 0) { Body[0] = (Body[0].x, level.Height - 2); }
            if (Body[0].x == level.Width - 1) { Body[0] = (1, Body[0].y); }
            if (Body[0].y == level.Height - 1) { Body[0] = (Body[0].x, 1); }
        }
        public bool IsDead()
        {
            for(int i = 1; i < Body.Count; i++)
            {
                if (Body[0] == Body[i])
                {
                    return true;
                }
            }
            return false;
        }
        public void Draw()
        {
            for (int i = 1; i < Body.Count; i++)
            {
                MyConsole.SetCell(Body[i], "[]", ConsoleColor.DarkGreen);
            }
            MyConsole.SetCell(Body[0], "[]", ConsoleColor.Green);

        }
    }
}
