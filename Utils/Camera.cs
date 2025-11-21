namespace Utils;
static public class Camera
{
    static public int X = 0;
    static public int Y = 0;
    static public int Width = 0;
    static public int Height = 0;
    static public Point2I Pos { get => (X, Y); set { X = value.x; Y = value.y; } }
    static public void Init(int width, int height)
    {
        X = 0; 
        Y = 0; 
        Width = width; 
        Height = height;
    }
    static public void Center(Point2I pos, int maxWidth, int maxHeight)
    {
        X = Math.Max(0, Math.Min(pos.x - Width / 2, maxWidth - Width));
        Y = Math.Max(0, Math.Min(pos.y - Height / 2, maxHeight - Height));
    }
}
