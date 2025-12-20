using Utils;

namespace Tests
{
    public class InventoryTest : Game
    {
        Utils.Inventory inventory;
        public override void Initialize()
        {
            inventory = new Utils.Inventory(25);
            MyConsole.Init();
        }
        public override void Draw()
        {
            inventory.Draw();

            MyConsole.Draw();
        }
    }
}
