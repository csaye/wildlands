using Microsoft.Xna.Framework;
using Wildlands.Items;

namespace Wildlands.UI
{
    public class Hotbar : UIElement
    {
        const int SlotCols = Inventory.SlotCols;

        public Hotbar() : base(UIAnchorX.Center, UIAnchorY.Bottom, 0, -Drawing.Grid, Drawing.Grid * SlotCols, Drawing.Grid) { }

        public override void Draw(Game1 game)
        {
            Drawing.DrawRect(game, Bounds, Color.White);

            for (int x = 0; x < SlotCols; x++)
            {
                // Get item at slot
                int slotIndex = x;
                ItemCount slot = game.Inventory.Slots[slotIndex];

                // Skip item if empty
                if (slot.IsEmpty) continue;

                // Get slot rect
                Vector2 offset = new Vector2(x, 0) * Drawing.Grid;
                Vector2 slotPosition = position + offset;
                Rectangle slotRect = new Rectangle(slotPosition.ToPoint(), new Point(Drawing.Grid));

                // Draw item to rect
                Drawing.DrawSprite(game, Drawing.ItemsTileset, slotRect, (int)slot.Item, Layers.UI);
                if (slot.Count > 1) Drawing.DrawText(game, slot.Count.ToString(), slotPosition, Color.Black);
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
            int slotIndex = click.X / Drawing.Grid;
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
