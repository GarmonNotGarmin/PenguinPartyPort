using Newtonsoft.Json;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria.ModLoader;
using Terraria;
using Terraria.ModLoader.Core;
using System;
using Terraria.ID;

namespace PenguinPartyPort.Common.Systems;

public class TileInfo
{

    public ushort TileID { get; set; }
    public bool HasTile { get; set; }

    public TileInfo(ushort tileID, bool hasTile = true)
    {
        TileID = tileID;
        HasTile = hasTile;
    }
}

public class SchematicFile
{
    private readonly static string folderPath = Path.Combine(ModLoader.ModPath, "PenguinPartyPort", "Schematics");

    public static SchematicFile file { get; set; } = new();

    [JsonProperty("Tile IDs")]
    public TileInfo[,] TileInfos { get; set; }

    public static void Save(string name, Point position, int width, int height)
    {
        TileInfo[,] tileInfos = new TileInfo[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Point pos = new(position.X + i, position.Y + j);
                if (Main.tile[pos.X, pos.Y].HasTile)
                    tileInfos[i, j] = new(Main.tile[pos.X, pos.Y].TileType);
                else
                    tileInfos[i, j] = new(0, false);
            }
        }

        SchematicFile file = new() 
        {
            TileInfos = tileInfos
        };

        WriteFile(file, name);
    }

    public static void Save(string name, Point point1, Point point2)
    {
        Point topLeft = new(Math.Min(point1.X, point2.X), Math.Min(point1.Y, point2.Y));
        Point bottomRight = new(Math.Max(point1.X, point2.X), Math.Max(point1.Y, point2.Y));

        int width = bottomRight.X - topLeft.X + 1;
        int height = bottomRight.Y - topLeft.Y + 1;
        Save(name, point1, width, height);
    }

    private static void WriteFile(SchematicFile file, string fileName)
    {
        Directory.CreateDirectory(folderPath);
        string json = JsonConvert.SerializeObject(file);
        string filePath = Path.Combine(folderPath, $"{fileName}.json");
        File.WriteAllText(filePath, json);
    }

    public static void Load(string fileName)
    {
        string filePath = FindFilePath(fileName);
        string json = File.ReadAllText(filePath);
        file = JsonConvert.DeserializeObject<SchematicFile>(json);
    }

    private static string FindFilePath(string fileName)
    {
        string filePath = Path.Combine(folderPath, fileName);
        if (File.Exists(filePath))
        {
            return filePath;
        }
        else
        {
            throw new FileNotFoundException();
        }
    }
}