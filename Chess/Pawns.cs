using System.Data;
using System.Reflection.Metadata.Ecma335;
using Utils;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Chess
{
    public abstract class ChessPiece
    {
        public int Owner;
        public string Character;
        public ChessPiece(int owner, string character)
        {
            Owner = owner;
            Character = character;
        }
        public abstract List<Point2I> GetValidMoves(ChessPiece[,] board, Point2I pos);
    }
    public class Pawn : ChessPiece
    {
        bool isFirstMove = true;
        public bool IsFirstMove { get => isFirstMove; set => isFirstMove = value; }
        public Pawn(int owner) : base(owner, "i") { }
        public override List<Point2I> GetValidMoves(ChessPiece[,] board, Point2I pos)
        {
            List<Point2I> moves = new List<Point2I>();
            Point2I move;
            move = (pos.x, Owner == 0 ? pos.y - 1 : pos.y + 1);
            if (ChessBoard.IsOnBoard(move))
            {
                if (board[move.x, move.y] is null)
                {
                    moves.Add(move);
                    move = (pos.x, Owner == 0 ? pos.y - 2 : pos.y + 2);
                    if (isFirstMove)
                    {
                        if (ChessBoard.IsOnBoard(move))
                            if (board[move.x, move.y] is null)
                            {
                                moves.Add(move);
                            }

                    }
                }
            }
            move = (pos.x - 1, Owner == 0 ? pos.y - 1 : pos.y + 1);
            if (ChessBoard.IsOnBoard(move))
            {
                if (board[move.x, move.y] is not null && board[move.x, move.y].Owner != Owner)
                {
                    moves.Add(move);
                }
            }
            move = (pos.x + 1, Owner == 0 ? pos.y - 1 : pos.y + 1);
            if (ChessBoard.IsOnBoard(move))
            {
                if (board[move.x, move.y] is not null && board[move.x, move.y].Owner != Owner)
                {
                    moves.Add(move);
                }
            }
            return moves;
        }
        public int Transform()
        {
            Console.WriteLine("Transform Your Pawn into: ");
            Console.WriteLine("1. Queen");
            Console.WriteLine("2. Bisshop");
            Console.WriteLine("3. Knight");
            Console.WriteLine("4. Rook");
            Console.Write("option: ");
            try
            {
                return Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                return 5;
            }
        }
    }
    public class King : ChessPiece
    {
        public King(int owner) : base(owner, "M") { }

        public override List<Point2I> GetValidMoves(ChessPiece[,] board, Point2I pos)
        {
            List<Point2I> moves = new List<Point2I>();
            List<Point2I> tryMoves = new List<Point2I> { (0, 1), (1, 1), (1, 0), (1, -1), (0, -1), (-1, -1), (-1, 0), (-1, 1) };
            foreach (var tryMove in tryMoves)
            {
                Point2I move = pos + tryMove;
                if (ChessBoard.IsOnBoard(move))
                {
                    if (board[move.x, move.y] is null || (board[move.x, move.y] is not null && board[move.x, move.y].Owner != Owner))
                    {
                        moves.Add(move);
                    }
                }
            }
            return moves;
        }
    }
    public class Queen : ChessPiece
    {
        public Queen(int owner) : base(owner, "W") { }

        public override List<Point2I> GetValidMoves(ChessPiece[,] board, Point2I pos)
        {
            List<Point2I> moves = new List<Point2I>();
            List<Point2I> tryMoves = new List<Point2I> { (0, 1), (1, 1), (1, 0), (1, -1), (0, -1), (-1, -1), (-1, 0), (-1, 1) };
            foreach (var tryMove in tryMoves)
            {
                for (int i = 1; i < 8; i++)
                {
                    Point2I move = pos + tryMove * i;
                    if (ChessBoard.IsOnBoard(move))
                    {
                        if (board[move.x, move.y] is null)
                        {
                            moves.Add(move);
                            continue;
                        }
                        if (board[move.x, move.y] is not null && board[move.x, move.y].Owner != Owner)
                        {
                            moves.Add(move);
                        }
                    }
                    break;
                }
            }
            return moves;
        }
    }
    public class Rook : ChessPiece
    {
        public Rook(int owner) : base(owner, "T") { }

        public override List<Point2I> GetValidMoves(ChessPiece[,] board, Point2I pos)
        {
            List<Point2I> moves = new List<Point2I>();
            List<Point2I> tryMoves = new List<Point2I> { (0, 1), (1, 0), (0, -1), (-1, 0) };
            foreach (var tryMove in tryMoves)
            {
                for (int i = 1; i < 8; i++)
                {
                    Point2I move = pos + tryMove * i;
                    if (ChessBoard.IsOnBoard(move))
                    {
                        if (board[move.x, move.y] is null)
                        {
                            moves.Add(move);
                            continue;
                        }
                        if (board[move.x, move.y] is not null && board[move.x, move.y].Owner != Owner)
                        {
                            moves.Add(move);
                        }
                    }
                    break;
                }
            }
            return moves;
        }
    }
    public class Bisshop : ChessPiece
    {
        public Bisshop(int owner) : base(owner, "/") { }

        public override List<Point2I> GetValidMoves(ChessPiece[,] board, Point2I pos)
        {
            List<Point2I> moves = new List<Point2I>();
            List<Point2I> tryMoves = new List<Point2I> { (1, 1), (1, -1), (-1, -1), (-1, 1) };
            foreach (var tryMove in tryMoves)
            {
                for (int i = 1; i < 8; i++)
                {
                    Point2I move = pos + tryMove * i;
                    if (ChessBoard.IsOnBoard(move))
                    {
                        if (board[move.x, move.y] is null)
                        {
                            moves.Add(move);
                            continue;
                        }
                        if (board[move.x, move.y] is not null && board[move.x, move.y].Owner != Owner)
                        {
                            moves.Add(move);
                        }
                    }
                    break;
                }
            }
            return moves;
        }
    }
    public class Knight : ChessPiece
    {
        public Knight(int owner) : base(owner, "F") { }

        public override List<Point2I> GetValidMoves(ChessPiece[,] board, Point2I pos)
        {
            List<Point2I> moves = new List<Point2I>();
            List<Point2I> tryMoves = new List<Point2I> { (2, 1), (2, -1), (-2, 1), (-2, -1), (1, 2), (-1, 2), (1, -2), (-1, -2) };
            foreach (var tryMove in tryMoves)
            {
                Point2I move = pos + tryMove;
                if (ChessBoard.IsOnBoard(move))
                {
                    if (board[move.x, move.y] is null || (board[move.x, move.y] is not null && board[move.x, move.y].Owner != Owner))
                    {
                        moves.Add(move);
                    }
                }
            }
            return moves;
        }
    }
}
