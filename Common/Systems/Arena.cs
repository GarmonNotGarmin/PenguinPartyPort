using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PenguinPartyPort.Common.Systems;

public class Arena
{
    public Point Position { get; set; }
    public int Width { get; }
    public int Height { get; }

    public Arena(Point position, int width, int height)
    {
        Position = position;
        Width = width;
        Height = height;
    }


}