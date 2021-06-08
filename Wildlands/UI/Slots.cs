using Microsoft.Xna.Framework;
using Wildlands.Items;

namespace Wildlands.UI
{
    public class Slots : UIElement
    {
        private const int SlotCols = Inventory.SlotCols;
        private const int SlotRows = Inventory.SlotRows;

        public Slots() : base(UIAnchorX.Center, UIAnchorY.Center, 0, 0, Drawing.Grid * SlotCols, Drawing.Grid * (SlotRows - 1)) { }

        public override void Draw(Game1 game)
        {
            // If menu closed, return
            if (!game.UIManager.MenuOpen) return;

            // Draw background
            Drawing.DrawRect(game, Bounds, Color.White);

            // For each item slot
            for (int x = 0; x < SlotCols; x++)
            {
                for (int y = 0; y < SlotRows - 1; y++)
                {
                    // Get item at slot
                    int slotIndex = x + ((y + 1) * SlotCols);
                    ItemCount slot = game.Inventory.Slots[slotIndex];

                    // Skip item if empty
                    if (slot.IsEmpty) continue;

                    // Get slot rect
                    Vector2 offset = new Vector2(x, y) * Drawing.Grid;
                    Vector2 slotPosition = position + offset;
                    Rectangle slotRect = new Rectangle(slotPosition.ToPoint(), new Point(Drawing.Grid));

                    // Draw item to rect
                    Drawing.DrawSprite(game, Drawing.ItemsTileset, slotRect, (int)slot.Item, Layers.UI);
                    if (slot.Count > 1) Drawing.DrawText(game, slot.Count.ToString(), slotPosition, Color.Black);
                }
            }
        }

        public override void OnLeftClick(Game1 game, Point mousePosition)
        {
            // Get inventory slots
            Inventory inventory = game.Inventory;
            ItemCount carrierSlot = inventory.CarrierSlot;
            ItemCount[] slots = inventory.Slots;

            // Get clicked slot
            Point click = mousePosition - position.ToPoint();
            int clickX = click.X / Drawing.Grid;
            int clickY = click.Y / Drawing.Grid;
            int slotIndex = clickX + ((clickY + 1) * SlotCols);
            ItemCount clickedSlot = slots[slotIndex];

            // If picking up items
            if (carrierSlot.IsEmpty)
            {
                // If not empty, transfer items to carrier
                if (!clickedSlot.IsEmpty)
                {
                    inventory.CarrierSlot = clickedSlot;
                    slots[slotIndex] = new ItemCount();
                }
            }
            // If dropping off items
            else
            {
                // If slot empty, deposit carrier items
                if (clickedSlot.IsEmpty)
                {
                    slots[slotIndex] = carrierSlot;
                    inventory.CarrierSlot = new ItemCount();
                }
                // If carrier items stackable
                else if (clickedSlot.Item == carrierSlot.Item)
                {
                    slots[slotIndex].Count += carrierSlot.Count;
                    inventory.CarrierSlot = new ItemCount();
                }
                // If not stackable, swap carrier and clicked
                else
                {
                    ItemCount swapSlot = carrierSlot;
                    inventory.CarrierSlot = clickedSlot;
                    slots[slotIndex] = swapSlot;
                }
            }
        }
    }
}
