namespace Wildlands.Items
{
    public class Inventory
    {
        // Slot data
        public const int SlotCols = 10;
        public const int SlotRows = 4;
        public const int SlotCount = SlotCols * SlotRows;

        // Item data
        private ItemCount[] slots = new ItemCount[SlotCount];

        public Inventory() { }

        public void OnSave(Game1 game)
        {
            slots = game.SaveData.inventoryData.slots;
        }

        public void OnLoad(Game1 game)
        {
            game.SaveData.inventoryData.slots = slots;
        }

        public ItemCount GetSlot(int i) => slots[i];

        // Attempts to add item to inventory and returns whether successful
        public bool AddItem(Item item, int count) => AddItem(new ItemCount(item, count));
        public bool AddItem(ItemCount itemCount)
        {
            // If empty item, return
            if (itemCount.IsEmpty) return false;

            // Check for stackable slot
            for (int i = 0; i < SlotCount; i++)
            {
                if (slots[i].Item == itemCount.Item)
                {
                    slots[i].Count += itemCount.Count;
                    return true;
                }
            }

            // Check for empty slot
            for (int i = 0; i < SlotCount; i++)
            {
                if (slots[i].IsEmpty)
                {
                    slots[i] = itemCount;
                    return true;
                }
            }

            // Return false if adding unsuccessful
            return false;
        }
    }
}
