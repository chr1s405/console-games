namespace Utils
{
    public struct LevelCell
    {
        private int cellId;
        private string cellString;
        //private ConsoleColor cellColor;
        //private ConsoleColor fontColor;
        public string CellString { get => cellString; }
        //public ConsoleColor BgColor { get => cellColor; }
        //public ConsoleColor FgColor { get => fontColor; }
        public LevelCell(int id, string cellString)
        {
            this.cellId = id;
            this.cellString = cellString;
            //this.cellColor = bgColor;
            //this.fontColor = fontColor;
        }
    }
}