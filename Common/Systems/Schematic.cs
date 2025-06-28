using Terraria;

namespace PenguinPartyPort.Common.Systems;

public class Schematic
{
    public int Width { get; }
    public int Height { get; }
    public Tile[,] Tiles { get; }
    public string Name { get; }

    public Schematic(string name)
    {
        Name = name;
        string fileName = name + ".json";
        ushort [,] tileIDs = ParseFile(fileName);
        Width = tileIDs.GetLength(0);
        Height = tileIDs.GetLength(1);
        Tiles = new Tile[Width, Height];

        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                Tiles[i, j] = new Tile();
                Tiles[i, j].HasTile = true;
                Tiles[i, j].TileType = tileIDs[i, j];
            }
        }
    }

    public static ushort[,] ParseFile(string fileName)
    {
        SchematicFile.Load(fileName);
        return SchematicFile.file.TileIDs;
    }
}
