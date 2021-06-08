namespace Wildlands.SaveLoad
{
    public class SaveData
    {
        public SaveData() { } 

        public string saveName = "default";

        public PlayerData playerData = new PlayerData();
        public InventoryData inventoryData = new InventoryData();
        public TileData tileData = new TileData();
    }
}
