using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGames
{
    abstract class Game
    {
        private bool gameOver;
        public bool GameOver { get => gameOver; set => gameOver = value; }

        virtual public void Initialize() { }
        virtual public void Update() { }
        virtual public void Draw() { }
    }
}
