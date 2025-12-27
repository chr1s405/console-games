using Utils;
using static Chess.Game;

namespace Chess
{
    public class ChessBoard
    {
        ChessPiece[,] board = new ChessPiece[8, 8];
        ChessPiece activePiece = null;
        public int Width { get => board.GetLength(0); }
        public int Height { get => board.GetLength(1); }
        public ChessPiece[,] Board { get => board; }
        public bool HasActivePiece { get => activePiece is not null; }
        public ChessPiece ActivePiece { get => activePiece; }
        public ChessBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                board[i, 1] = (new Pawn(1, (i, 1)));
                board[i, 6] = (new Pawn(0, (i, 6)));
            }
            List<ChessPiece> pieces = new List<ChessPiece> {
                (new King(1, (4, 0))),
                (new Queen(1, (3, 0))),
                (new Tower(1, (0, 0))),
                (new Tower(1, (7, 0))),
                (new Horse(1, (1, 0))),
                (new Horse(1, (6, 0))),
                (new Bisshop(1, (2, 0))),
                (new Bisshop(1, (5, 0))),

                (new King(0, (4, 7))),
                (new Queen(0, (3, 7))),
                (new Tower(0, (0, 7))),
                (new Tower(0, (7, 7))),
                (new Horse(0, (1, 7))),
                (new Horse(0, (6, 7))),
                (new Bisshop(0, (2, 7))),
                (new Bisshop(0, (5, 7))),
            };
            foreach (ChessPiece piece in pieces)
            {
                board[piece.Pos.x, piece.Pos.y] = piece;
            }
        }
        public bool TakePiece(string chessPos, int owner)
        {
            Point2I pos = GetPosFromBoard(chessPos);
            if (IsOnBoard(pos))
            {
                ChessPiece piece = board[pos.x, pos.y];
                if (piece is not null && piece.Owner == owner)
                {
                    activePiece = piece;
                    return true;
                }
            }
            return false;
        }
        public bool PlacePiece(string chessPos)
        {
            Point2I pos = GetPosFromBoard(chessPos);
            if (activePiece.GetValidMoves(board).Contains(pos))
            {
                board[activePiece.Pos.x, activePiece.Pos.y] = null;
                board[pos.x, pos.y] = activePiece;
                board[pos.x, pos.y].Pos = pos;
                activePiece = null;
                return true;
            }
            activePiece = null;
            return false;
        }
        public void Draw()
        {
            for (int i = 1; i < 9; i++)
            {
                MyConsole.SetCell((0, i), (9 - i).ToString());
                MyConsole.SetCell((9, i), (9 - i).ToString());
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
                        ChessPiece piece = board[col - 1, row - 1];
                        if (piece is not null)
                        {
                            MyConsole.SetForeground((col, row), piece.Character, piece.Owner == 0 ? ConsoleColor.White : ConsoleColor.Black);
                        }
                    }
                }
            }
            if (activePiece is not null)
            {
                //MyConsole.SetForeground((activePiece.Pos) + (1, 1), activePiece.Character, ConsoleColor.DarkYellow);
                MyConsole.SetBackground((activePiece.Pos) + (1, 1), ConsoleColor.DarkYellow);
                List<Point2I> moves = activePiece.GetValidMoves(board);
                foreach (Point2I move in moves)
                {
                    if (board[move.x, move.y] is null)
                    {
                        //MyConsole.SetForeground(move + (1, 1), "x", activePiece.Owner == 0 ? ConsoleColor.White : ConsoleColor.Black);
                        MyConsole.SetForeground(move + (1, 1), "x", activePiece.Owner == 0 ? ConsoleColor.DarkYellow : ConsoleColor.DarkYellow);
                    }
                    else
                    {
                        MyConsole.SetForeground(move + (1, 1), board[move.x, move.y].Character, ConsoleColor.Red);
                        //MyConsole.SetBackground(move + (1, 1), ConsoleColor.Red);
                    }
                }
            }
        }
        public static string GetChessPos(Point2I pos)
        {
            return $"{"abcdefgh".Substring(pos.x, 1)}{8 - pos.y}";
        }
        public static Point2I GetPosFromBoard(string pos)
        {
            int x = 0;
            int y = 0;
            try
            {
                x = Convert.ToInt32("abcdefg".IndexOf(pos.Substring(0, 1).ToLower()));
                y = 8 - Convert.ToInt32(pos.Substring(1, 1));
            }
            catch (Exception e) { return (-1, -1); }
            return (x, y);
        }
        public static bool IsOnBoard(Point2I pos)
        {
            return (pos.x >= 0 && pos.x < 8) && (pos.y >= 0 && pos.y < 8);
        }
    }

}
