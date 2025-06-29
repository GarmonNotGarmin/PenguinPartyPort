using PenguinPartyPort.Content.Items.Tools;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria;
using System;

namespace PenguinPartyPort.Common.Systems;

public class Pointer : ModPlayer
{
    public Point Point1 = new(-1, -1);
    public Point Point2 = new(-1, -1);

    public void UpdatePointer(Point point1, Point point2)
    {
        Point1 = point1;
        Point2 = point2;
    }

    public override void Initialize()
    {
        Point1 = new(-1, -1);
        Point2 = new(-1, -1);
    }
}
