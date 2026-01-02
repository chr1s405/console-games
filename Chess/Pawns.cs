using System.Data;
using Utils;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Chess
{
    public abstract class ChessPiece
    {
        public int Owner;
        public Point2I Pos;
        public string Character;
        public ChessPiece(int owner, Point2I pos, string character)
        {
            Owner = owner;
            Pos = pos;
            Character = character;
        }
        public abstract List<Point2I> GetValidMoves(ChessPiece[,] board);
        public virtual void Move(Point2I pos)
        {
            Pos = pos;
        }
    }
    public class Pawn : ChessPiece
    {
        bool isFirstMove = true;
        public Pawn(int owner, Point2I pos) : base(owner, pos, "i") { }
        public override void Move(Point2I pos)
        {
            base.Move(pos);
            if (isFirstMove)
                isFirstMove = false;

        }
        public override List<Point2I> GetValidMoves(ChessPiece[,] board)
        {
            List<Point2I> moves = new List<Point2I>();
            Point2I move;
            move = (Pos.x, Owner == 0 ? Pos.y - 1 : Pos.y + 1);
            if (ChessBoard.IsOnBoard(move))
            {
                if (board[move.x, move.y] is null)
                {
                    moves.Add(move);
                    move = (Pos.x, Owner == 0 ? Pos.y - 2 : Pos.y + 2);
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
            move = (Pos.x - 1, Owner == 0 ? Pos.y - 1 : Pos.y + 1);
            if (ChessBoard.IsOnBoard(move))
            {
                if (board[move.x, move.y] is not null && board[move.x, move.y].Owner != Owner)
                {
                    moves.Add(move);
                }
            }
            move = (Pos.x + 1, Owner == 0 ? Pos.y - 1 : Pos.y + 1);
            if (ChessBoard.IsOnBoard(move))
            {
                if (board[move.x, move.y] is not null && board[move.x, move.y].Owner != Owner)
                {
                    moves.Add(move);
                }
            }
            return moves;
        }
    }
    public class King : ChessPiece
    {
        public King(int owner, Point2I pos) : base(owner, pos, "M") { }

        public override List<Point2I> GetValidMoves(ChessPiece[,] board)
        {
            List<Point2I> moves = new List<Point2I>();
            List<Point2I> tryMoves = new List<Point2I> { (0, 1), (1, 1), (1, 0), (1, -1), (0, -1), (-1, -1), (-1, 0), (-1, 1) };
            foreach (var tryMove in tryMoves)
            {
                Point2I move = Pos + tryMove;
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
        public Queen(int owner, Point2I pos) : base(owner, pos, "W") { }

        public override List<Point2I> GetValidMoves(ChessPiece[,] board)
        {
            List<Point2I> moves = new List<Point2I>();
            List<Point2I> tryMoves = new List<Point2I> { (0, 1), (1, 1), (1, 0), (1, -1), (0, -1), (-1, -1), (-1, 0), (-1, 1) };
            foreach (var tryMove in tryMoves)
            {
                for (int i = 1; i < 8; i++)
                {
                    Point2I move = Pos + tryMove * i;
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
    public class Tower : ChessPiece
    {
        public Tower(int owner, Point2I pos) : base(owner, pos, "T") { }

        public override List<Point2I> GetValidMoves(ChessPiece[,] board)
        {
            List<Point2I> moves = new List<Point2I>();
            List<Point2I> tryMoves = new List<Point2I> { (0, 1), (1, 0), (0, -1), (-1, 0) };
            foreach (var tryMove in tryMoves)
            {
                for (int i = 1; i < 8; i++)
                {
                    Point2I move = Pos + tryMove * i;
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
        public Bisshop(int owner, Point2I pos) : base(owner, pos, "/") { }

        public override List<Point2I> GetValidMoves(ChessPiece[,] board)
        {
            List<Point2I> moves = new List<Point2I>();
            List<Point2I> tryMoves = new List<Point2I> { (1, 1), (1, -1), (-1, -1), (-1, 1) };
            foreach (var tryMove in tryMoves)
            {
                for (int i = 1; i < 8; i++)
                {
                    Point2I move = Pos + tryMove * i;
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
    public class Horse : ChessPiece
    {
        public Horse(int owner, Point2I pos) : base(owner, pos, "F") { }

        public override List<Point2I> GetValidMoves(ChessPiece[,] board)
        {
            List<Point2I> moves = new List<Point2I>();
            List<Point2I> tryMoves = new List<Point2I> { (2, 1), (2, -1), (-2, 1), (-2, -1), (1, 2), (-1, 2), (1, -2), (-1, -2) };
            foreach (var tryMove in tryMoves)
            {
                Point2I move = Pos + tryMove;
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
