using Utils;

namespace Chess
{
    public class Game: Utils.Game
    {
        Board board;
        public override void Initialize()
        {
            board = new Board();
            MyConsole.Init(board.Width, board.Height);
        }
        public override void Update()
        {
            
        }
        public override void Draw()
        {
            board.Draw();
            MyConsole.Draw();
        }
    }
}
