using System.Numerics;
using Utils;

namespace Chess
{
    public class Game : Utils.Game
    {
        Board board;
        int turn = 0;
        string command;
        List<Pawn> army1;
        List<Pawn> army2;
        public override void Initialize()
        {
            board = new Board();
            army1 = InitializePawns(0);
            army2 = InitializePawns(1);

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
            foreach (var pawn in army1)
            {
                pawn.Draw();
            }
            foreach (var pawn in army2)
            {
                pawn.Draw();
            }
            MyConsole.Draw();
            Console.WriteLine(turn == 0 ? "Player 1's turn" : "Player 2's turn" + ":");
        }
        public List<Pawn> InitializePawns(int owner)
        {
            List<Pawn> army = new List<Pawn>();
            for (int i = 0; i < 8; i++)
            {
                army.Add(new FootSoldier(owner, owner == 0 ? (i, 1) : (i, 6)));
            }
            army.Add(new King(owner, owner == 0 ? (4, 0) : (4, 7)));
            army.Add(new Queen(owner, owner == 0 ? (3, 0) : (3, 7)));
            army.Add(new Tower(owner, owner == 0 ? (0, 0) : (0, 7)));
            army.Add(new Tower(owner, owner == 0 ? (7, 0) : (7, 7)));
            army.Add(new Horse(owner, owner == 0 ? (1, 0) : (1, 7)));
            army.Add(new Horse(owner, owner == 0 ? (6, 0) : (6, 7)));
            army.Add(new Bisshop(owner, owner == 0 ? (2, 0) : (2, 7)));
            army.Add(new Bisshop(owner, owner == 0 ? (5, 0) : (5, 7)));

            return army;
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
            public void Draw()
            {
                MyConsole.SetForeGround(Pos + (1,1), Character, Owner == 0 ? ConsoleColor.White : ConsoleColor.Black);
            }
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
