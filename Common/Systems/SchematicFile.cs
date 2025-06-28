using Newtonsoft.Json;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria.ModLoader;
using Terraria;

namespace PenguinPartyPort.Common.Systems;

public class SchematicFile
{
    private readonly static string folderPath = "PenguinPartyPort/Schematics/";

    public static SchematicFile file { get; set; } = new();

    [JsonProperty("Tile IDs")]
    public ushort[,] TileIDs { get; set; }

    public static void Save(string name, Vector2 position, int width, int height)
    {
        ushort[,] tileIDs = new ushort[width, height];

        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
                tileIDs[i, j] = Main.tile[(int)position.X + i, (int)position.Y + j].TileType;

        SchematicFile file = new() 
        {
            TileIDs = tileIDs
        };

        string json = JsonConvert.SerializeObject(file, Formatting.Indented);
        string filePath = folderPath + name + ".json";
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
        string filePath = folderPath + fileName;
        if (ModContent.FileExists(filePath))
        {
            return filePath;
        }
        else
        {
            throw new FileNotFoundException();
        }
    }
}
