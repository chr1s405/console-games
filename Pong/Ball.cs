using Utils;

namespace Pong
{
    internal class Ball
    {
        Point2D pos;
        Point2D velocity;
        Point2D Pos { get => pos; }
        Point2D Velocity { get => Velocity; }
        public Ball(Level level)
        {
            pos = ((int)level.Width / 2, (int)level.Height / 2);
            velocity = (Math.Cos(Utils.Utils.AngleToRad(30)), Math.Sin(Utils.Utils.AngleToRad(30)));
        }
        public void Move(Level level)
        {
            pos += velocity;
            if (pos.x < 0 || pos.x > level.Width - 1)
            {
                velocity.x *= -1;
                pos.x += velocity.x;
            }
            if (pos.y < 0 || pos.y > level.Height - 1)
            {
                velocity.y *= -1;
                pos.y += velocity.y;
            }
        }
        public void Draw()
        {
            MyConsole.SetCell(pos, "  ", ConsoleColor.White);
        }
    }
}
