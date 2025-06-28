using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;

namespace PenguinPartyPort.Common.Systems;

public class Schematic
{
    [JsonProperty("Name")]
    public string Name { get; }

    public Schematic(string name)
    {
        Name = name;
    }

    public static Tile[]? ParseFile(string fileName)
    {
        string filePath = FindFilePath(fileName);
        using (var stream = File.OpenRead(filePath))
        {
            
        }

        return null;
    }

    private static string FindFilePath(string fileName)
    {
        string filePath = "PenguinPartyPort/Schematics/" + fileName;
        if (ModContent.FileExists(filePath))
        {
            return filePath;
        }
        else
        {
            throw new FileNotFoundException();
        }
        return "";
    }
}
