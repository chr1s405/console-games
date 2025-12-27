using System.Numerics;
using System.Transactions;
using Utils;

namespace Chess
{
    public class Game : Utils.Game
    {
        ChessBoard board;
        int turn = 0;
        string command;
        List<ChessPiece> army1;
        List<ChessPiece> army2;
        public override void Initialize()
        {
            board = new ChessBoard();

            MyConsole.Init(board.Width, board.Height);
            Draw();
        }
        public override void Update()
        {
            command = Console.ReadLine();
            Console.WriteLine(ChessBoard.GetPosFromBoard(command).ToString());
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
        }
        public override void Draw()
        {
            board.Draw();

            MyConsole.Draw();
            Console.WriteLine(turn == 0 ? "Player 1's turn" : "Player 2's turn" + ":");
            if (board.HasActivePiece)
            {
                Console.Write(ChessBoard.GetChessPos(board.ActivePiece.Pos) + " -> ");
            }
        }

    }
}
