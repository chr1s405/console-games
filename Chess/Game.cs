using System.Numerics;
using Utils;

namespace Chess
{
    public class Game : Utils.Game
    {
        ChessBoard board;
        int turn = 0;
        string command;
        List<Pawn> army1;
        List<Pawn> army2;
        public override void Initialize()
        {
            board = new ChessBoard();

            MyConsole.Init(board.Width, board.Height);
            Draw();
        }
        public override void Update()
        {
            command = Console.ReadLine();
            turn = (turn + 1) % 2;
        }
        public override void Draw()
        {
            board.Draw();
            MyConsole.Draw();
            Console.WriteLine(turn == 0 ? "Player 1's turn" : "Player 2's turn" + ":");
        }
        public abstract class Pawn
        {
            public int Owner;
            public Point2I Pos;
            public string Character;
            public Pawn(int owner, Point2I pos, string character)
            {
                Owner = owner;
                Pos = pos;
                Character = character;
            }
            //public void Draw()
            //{
            //    MyConsole.SetForeGround(Pos + (1,1), Character, Owner == 0 ? ConsoleColor.White : ConsoleColor.Black);
            //}
        }
        public class FootSoldier : Pawn
        {
            public FootSoldier(int owner, Point2I pos) : base(owner, pos, "i") { }
        }
        public class King : Pawn
        {
            public King(int owner, Point2I pos) : base(owner, pos, "M") { }
        }
        public class Queen : Pawn
        {
            public Queen(int owner, Point2I pos) : base(owner, pos, "W") { }
        }
        public class Tower : Pawn
        {
            public Tower(int owner, Point2I pos) : base(owner, pos, "T") { }
        }
        public class Horse : Pawn
        {
            public Horse(int owner, Point2I pos) : base(owner, pos, "F") { }
        }
        public class Bisshop : Pawn
        {
            public Bisshop(int owner, Point2I pos) : base(owner, pos, "/") { }
        }
    }
}
