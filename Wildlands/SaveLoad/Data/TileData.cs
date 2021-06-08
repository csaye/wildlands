using System.IO;
using System.Reflection;
using System.Text.Json;
using Wildlands.Tiles;

namespace Wildlands.SaveLoad
{
    public class TileData
    {
        public Tile[] tiles;

        public TileData()
        {
            // Read default tile data from file
            string currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string path = Path.Combine(currentDirectory, "Content", "TileData.json");
            string text = File.ReadAllText(path);
            tiles = JsonSerializer.Deserialize<Tile[]>(text);
        }
    }
}
