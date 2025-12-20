
using Utils;

namespace Pong
{
    public class Game : Utils.Game
    {
        Level level;
        Ball ball;
        public override void Initialize()
        {
            level = new Level(20, 20);
            ball = new Ball(level);
        }
        public override void Update()
        {
            ball.Move(level);
        }
        public override void Draw()
        {
            level.Draw();
            ball.Draw();

            MyConsole.Draw();
        }
    }
}
