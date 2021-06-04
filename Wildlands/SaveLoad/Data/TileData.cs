using System.IO;
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
            string text = File.ReadAllText("Content/TileData.json");
            tiles = JsonSerializer.Deserialize<Tile[]>(text);
        }
    }
}
