namespace Wildlands.Items
{
    public class Inventory
    {
        // Slot data
        public const int SlotCols = 10;
        public const int SlotRows = 4;
        public const int SlotCount = SlotCols * SlotRows;

        // Item data
        public ItemCount[] Slots { get; private set; } = new ItemCount[SlotCount];
        public ItemCount CarrierSlot { get; set; } = new ItemCount();

        public Inventory() { }

        public void OnSave(Game1 game)
        {
            Slots = game.SaveData.inventoryData.slots;
        }

        public void OnLoad(Game1 game)
        {
            game.SaveData.inventoryData.slots = Slots;
        }

        // Attempts to add item to inventory and returns whether successful
        public bool AddItem(Item item, int count) => AddItem(new ItemCount(item, count));
        public bool AddItem(ItemCount itemCount)
        {
            // If empty item, return
            if (itemCount.IsEmpty) return false;

            // Check for stackable slot
            for (int i = 0; i < SlotCount; i++)
            {
                if (Slots[i].Item == itemCount.Item)
                {
                    Slots[i].Count += itemCount.Count;
                    return true;
                }
            }

            // Check for empty slot
            for (int i = 0; i < SlotCount; i++)
            {
                if (Slots[i].IsEmpty)
                {
                    Slots[i] = itemCount;
                    return true;
                }
            }

            // Return false if adding unsuccessful
            return false;
        }
    }
}
