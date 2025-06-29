using Terraria;
using Microsoft.Xna.Framework;
namespace PenguinPartyPort.Common.Systems;

public class Schematic
{
    public int Width { get; }
    public int Height { get; }
    public TileInfo[,] Tiles { get; }
    public string Name { get; }

    public Schematic(string name)
    {
        Name = name;
        string fileName = name + ".json";
        Tiles = ParseFile(fileName);
        Width = Tiles.GetLength(0);
        Height = Tiles.GetLength(1);
    }

    public void PasteSchematic(Point position)
    {
        for (int i = 0; i < Width; i++)
            for (int j = 0; j < Height; j++)
                if (Tiles[i, j].HasTile)
                    WorldGen.PlaceTile(position.X + i, position.Y + j, Tiles[i, j].TileID, forced: true);
    }

    public static TileInfo[,] ParseFile(string fileName)
    {
        SchematicFile.Load(fileName);
        return SchematicFile.file.TileInfos;
    }
}