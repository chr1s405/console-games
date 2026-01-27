using System.Security.AccessControl;
using Utils;
using static Chess.Game;

namespace Chess
{
    public class ChessBoard
    {
        ChessPiece[,] board = new ChessPiece[8, 8];
        ChessPiece activePiece = null;
        Point2I activePos = (-1,-1);
        bool isGameOver = false;
        public int Width { get => board.GetLength(0); }
        public int Height { get => board.GetLength(1); }
        public ChessPiece[,] Board { get => board; }
        public bool HasActivePiece { get => activePiece is not null; }
        public ChessPiece ActivePiece { get => activePiece; }
        public Point2I ActivePos { get => activePos; }
        public bool IsGameOver { get => isGameOver; }
        public ChessBoard()
        {
            List<ChessPiece> whites = new List<ChessPiece> {
                (new Rook(0)),
                (new Knight(0)),
                (new Bisshop(0)),
                (new Queen(0)),
                (new King(0)),
                (new Bisshop(0)),
                (new Knight(0)),
                (new Rook(0)),
            };

            List<ChessPiece> blacks = new List<ChessPiece> {
                (new Rook(1)),
                (new Knight(1)),
                (new Bisshop(1)),
                (new Queen(1)),
                (new King(1)),
                (new Bisshop(1)),
                (new Knight(1)),
                (new Rook(1)),
            };
            for (int i = 0; i < 8; i++)
            {
                board[i, 0] = blacks[i];
                board[i, 1] = (new Pawn(1));
                board[i, 6] = (new Pawn(0));
                board[i, 7] = whites[i];
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
                    activePos = pos;
                    return true;
                }
            }
            return false;
        }
        public bool PlacePiece(string chessPos)
        {
            Point2I pos = GetPosFromBoard(chessPos);
            if (activePiece.GetValidMoves(board, activePos).Contains(pos))
            {
                ChessPiece chessPiece = board[pos.x, pos.y];

                board[activePos.x, activePos.y] = null;
                board[pos.x, pos.y] = activePiece;
                if(activePiece.GetType() == typeof(Pawn))
                {
                    if (((Pawn)activePiece).IsFirstMove)
                    {
                        ((Pawn)activePiece).IsFirstMove = false;
                    }
                    if (pos.y == (activePiece.Owner == 0 ? 0 : 7))
                    {
                        int choice = ((Pawn)activePiece).Transform();
                        ChessPiece newPiece;
                        switch (choice)
                        {
                            case 1: newPiece = new Queen(activePiece.Owner); break;
                            case 2: newPiece = new Bisshop(activePiece.Owner); break;
                            case 3: newPiece = new Knight(activePiece.Owner); break;
                            case 4: newPiece = new Rook(activePiece.Owner); break;
                            default: newPiece = board[pos.x, pos.y]; break;
                        }
                        board[pos.x, pos.y] = newPiece;
                        MyConsole.Clear();
                        MyConsole.Draw();
                    }
                }
                activePiece = null;
                if (chessPiece is not null && chessPiece.GetType() == typeof(King))
                {
                    isGameOver = true;
                }
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
                MyConsole.SetBackground((activePos) + (1, 1), ConsoleColor.DarkYellow);
                List<Point2I> moves = activePiece.GetValidMoves(board, activePos);
                foreach (Point2I move in moves)
                {
                    if (board[move.x, move.y] is null)
                    {
                        MyConsole.SetForeground(move + (1, 1), "x", activePiece.Owner == 0 ? ConsoleColor.DarkYellow : ConsoleColor.DarkYellow);
                    }
                    else
                    {
                        MyConsole.SetForeground(move + (1, 1), board[move.x, move.y].Character, ConsoleColor.Red);
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
                x = Convert.ToInt32("abcdefgh".IndexOf(pos.Substring(0, 1).ToLower()));
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
