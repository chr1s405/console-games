using System.Numerics;
using System.Transactions;
using Utils;

namespace Chess
{
    public class Game : Utils.Game
    {
        ChessBoard board;
        int turn = 0;
        string command = "";
        public override void Initialize()
        {
            board = new ChessBoard();

            MyConsole.Init(board.Width, board.Height);
            Draw();
        }
        public override void Update()
        {
            command = Console.ReadLine() ?? "";
            if (!board.HasActivePiece)
            {
                board.TakePiece(command, turn);
            }
            else
            {
                if (board.PlacePiece(command))
                {
                    turn = (turn + 1) % 2;
                }
            }
            if (board.IsGameOver)
            {
                GameOver = true;
            }
        }
        public override void Draw()
        {
            board.Draw();

            MyConsole.Draw();
            Console.WriteLine();

            if (!GameOver)
            {
                Console.WriteLine(turn == 0 ? "White's turn" : "Black's turn" + ":");
                MyConsole.Write("input: ");
                if (board.HasActivePiece)
                {
                    Console.Write(ChessBoard.GetChessPos(board.ActivePiece.Pos) + " -> ");
                }
            }
            else
            {
                MyConsole.WriteLine((turn == 0 ? "Black" : "White") + " won");
                MyConsole.Write();
            }
        }

    }
}
