using Utils;

namespace Tests
{
    public class InventoryTest : Game
    {
        Utils.Inventory inventory;
        public override void Initialize()
        {
            inventory = new Utils.Inventory(25);
        }
        public override void Update()
        {
            if (Console.KeyAvailable)
            {
                if(Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    GameOver = true;
                }
            }
        }
        public override void Draw()
        {
            inventory.Draw();

            MyConsole.Draw();
            Console.WriteLine("Press any key to stop");
        }
    }
}
