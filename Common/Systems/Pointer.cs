using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace PenguinPartyPort.Common.Systems;

public class Pointer : ModPlayer
{
    public Point Point1 = new(-1, -1);
    public Point Point2 = new(-1, -1);
    public Point MousePoint;

    public void UpdatePointer(Point point1, Point point2)
    {
        Point1 = point1;
        Point2 = point2;
    }

    public bool CheckAndSwapPoints()
    {
        bool isSwapped = false;
        if (Point1.X > Point2.X)
        {
            (Point1.X, Point2.X) = (Point2.X, Point1.X);
            isSwapped = true;
        }
        if (Point1.Y > Point2.Y)
        {
            (Point1.Y, Point2.Y) = (Point2.Y, Point1.Y);
            isSwapped = true;
        }
        return isSwapped;
    }

    public override void Initialize()
    {
        Point1 = new(-1, -1);
        Point2 = new(-1, -1);
    }
}
