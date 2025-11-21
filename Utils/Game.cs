namespace Utils
{
    public abstract class Game
    {
        public bool GameOver { get; set; }
        public string Message { get; set; }

        virtual public void Initialize() { }
        virtual public void Update() { }
        virtual public void Draw() { }
    }
}
