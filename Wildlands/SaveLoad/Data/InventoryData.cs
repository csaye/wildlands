using Wildlands.Items;

namespace Wildlands.SaveLoad
{
    public class InventoryData
    {
        public ItemCount[] slots = new ItemCount[Inventory.SlotCount];

        public InventoryData() { }
    }
}
