using Newtonsoft.Json;
using System.IO;
using Terraria.ModLoader;

namespace PenguinPartyPort.Common.Systems
{

    public class SchematicFile
    {
        public static SchematicFile file { get; set; } = new();

        [JsonProperty("Tile IDs")]
        public ushort[,] TileIDs { get; set; }

        public static void Load(string fileName)
        {
            string filePath = FindFilePath(fileName);
            string json = File.ReadAllText(filePath);
            file = JsonConvert.DeserializeObject<SchematicFile>(json);
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
        }
    }
}
