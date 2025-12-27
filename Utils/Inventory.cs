
namespace Utils
{
    public class Inventory
    {
        int capacity;
        List<Item> items;
        int width;
        int height;
        public Inventory(int capacity)
        {
            items = new List<Item>();
            this.capacity = capacity;
            width = 5;
            height = (int)Math.Ceiling(capacity / (float)width);
        }

        public void AddItem(Item item)
        {
            items.Add(item);
        }
        public bool RemoveItem(Item item)
        {
            return items.Remove(item);
        }
        public void Draw()
        {

            int cellWidth = 3;
            int cellHeight = 3;

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    if (width * row + col < capacity)
                    {
                        DrawCell(col, row, cellWidth, cellHeight);
                        Item item = items.ElementAtOrDefault(row * width + col);
                        if (item != null)
                        {
                            MyConsole.SetCell((col * cellWidth + 1, row * cellHeight + 1), item.Icon);
                        }
                    }
                }
            }
        }
        public void DrawCell(int left, int top, int width, int height, bool isLocked = false)
        {
            //MyConsole.SetCell((left * width, top * height), "+");
            //MyConsole.SetCell((left * width + width, top * height), "+");
            //MyConsole.SetCell((left * width, top * height + height), "+");
            //MyConsole.SetCell((left * width + width, top * height + height), "+");
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    if (col > 0 && col < width && (row == 0 || row == height-1 ))
                    {
                        MyConsole.SetCell((left * width + col, top * height), "-");
                        MyConsole.SetCell((left * width + col, top * height + height), "-");
                    }
                    if( row > 0 && row < height && (col == 0 || col == width - 1))
                    {
                        MyConsole.SetCell((left * width, top * height + row), "|");
                        MyConsole.SetCell((left * width + width, top * height + row), "|");
                    }

                }
            }
        }
    }
}
