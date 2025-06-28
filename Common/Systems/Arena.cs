using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PenguinPartyPort.Common.Systems;

public class Arena
{
    public Vector2 Position { get; set; }
    public int Width { get; }
    public int Height { get; }

    public Arena(Vector2 position, int width, int height)
    {
        Position = position;
        Width = width;
        Height = height;
    }


}