namespace Utils
{
    public struct LevelCell
    {
        private int cellId;
        private string cellString;
        private ConsoleColor bgColor;
        private ConsoleColor fgColor;
        public string CellString { get => cellString; }
        public ConsoleColor BgColor { get => bgColor; }
        public ConsoleColor FgColor { get => fgColor; }
        public LevelCell(int id, string cellString, ConsoleColor bgColor = ConsoleColor.Black, ConsoleColor fgColor = ConsoleColor.White)
        {
            this.cellId = id;
            this.cellString = cellString;
            this.bgColor = bgColor;
            this.fgColor = fgColor;
        }
    }
}