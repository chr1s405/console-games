using Utils;
using static Chess.Game;

namespace Chess
{
    public class ChessBoard
    {
        Pawn[,] board = new Pawn[8, 8];
        public int Width { get => board.GetLength(0); }
        public int Height { get => board.GetLength(1); }
        public Pawn[,] Board { get => board; }
        public ChessBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                board[i, 1] = (new FootSoldier(1, (i, 1)));
                board[i, 6] = (new FootSoldier(0, (i, 6)));
            }
            board[4, 0] = (new King(1, (4, 0)));
            board[3, 0] = (new Queen(1, (3, 0)));
            board[0, 0] = (new Tower(1, (0, 0)));
            board[7, 0] = (new Tower(1, (7, 0)));
            board[1, 0] = (new Horse(1, (1, 0)));
            board[6, 0] = (new Horse(1, (6, 0)));
            board[2, 0] = (new Bisshop(1, (2, 0)));
            board[5, 0] = (new Bisshop(1, (5, 0)));

            board[4, 7] = (new King(0, (4, 7)));
            board[3, 7] = (new Queen(0, (3, 7)));
            board[0, 7] = (new Tower(0, (0, 7)));
            board[7, 7] = (new Tower(0, (7, 7)));
            board[1, 7] = (new Horse(0, (1, 7)));
            board[6, 7] = (new Horse(0, (6, 7)));
            board[2, 7] = (new Bisshop(0, (2, 7)));
            board[5, 7] = (new Bisshop(0, (5, 7)));
        }
        public void Draw()
        {
            for (int i = 1; i < 9; i++)
            {
                MyConsole.SetCell((0, i), i.ToString());
                MyConsole.SetCell((9, i), i.ToString());
                string abc = " abcdefgh ";
                MyConsole.SetCell((i, 0), abc.Substring(i, 1));
                MyConsole.SetCell((i, 9), abc.Substring(i, 1));
            }
            for (int row = 1; row < board.GetLength(0) + 1; row++)
            {
                for (int col = 1; col < board.GetLength(1) + 1; col++)
                {
                    ConsoleColor color;
                    if ((col + row) % 2 == 0)
                    {
                        color = ConsoleColor.Gray;
                    }
                    else
                    {
                        color = ConsoleColor.DarkGray;
                    }
                    MyConsole.SetCell((col, row), "  ", color, color);
                    if (row - 1 < Height && col - 1 < Width)
                    {
                        Pawn pawn = board[col - 1, row - 1];
                        if (pawn is not null)
                        {
                            MyConsole.SetForeGround((col, row), pawn.Character, pawn.Owner == 0 ? ConsoleColor.White : ConsoleColor.Black);
                        }
                    }
                }
            }
        }
    }
}
