using Microsoft.Xna.Framework;

namespace Wildlands.UI
{
    public class Inventory : UIElement
    {
        // Slot data
        private const int SlotCols = 10;
        private const int SlotRows = 3;
        private const int SlotCount = SlotCols * SlotRows;

        // Item data
        private readonly ItemCount[] slots = new ItemCount[SlotCount];

        public Inventory() : base(UIAnchorX.Center, UIAnchorY.Center, 0, 0, Drawing.Grid * SlotCols, Drawing.Grid * SlotRows) { }

        public override void Draw(Game1 game)
        {
            // Draw background
            Drawing.DrawRect(game, Bounds, Color.White);

            // For each item slot
            for (int x = 0; x < SlotCols; x++)
            {
                for (int y = 0; y < SlotRows; y++)
                {
                    // Get item at slot
                    int i = x + (y * SlotCols);
                    ItemCount itemCount = slots[i];

                    // Skip item if empty
                    if (itemCount.IsEmpty) continue;

                    // Get slot rect
                    Vector2 offset = new Vector2(x, y) * Drawing.Grid;
                    Vector2 slotPosition = position + offset;
                    Rectangle slotRect = new Rectangle(slotPosition.ToPoint(), new Point(Drawing.Grid));

                    // Draw item to rect
                    Drawing.DrawSprite(game, Drawing.ItemsTileset, slotRect, (int)itemCount.Item, Layers.UI);
                }
            }
        }

        // Attempts to add item to inventory and returns whether successful
        public bool AddItem(Item item, int count) => AddItem(new ItemCount(item, count));
        public bool AddItem(ItemCount itemCount)
        {
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
