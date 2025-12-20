namespace Utils
{
    public enum Dir { none, up, right, down, left }

    public class Utils
    {
        static public double AngleToRad(double angle)
        {
            return angle * Math.PI / 180;

        }
    }
}
