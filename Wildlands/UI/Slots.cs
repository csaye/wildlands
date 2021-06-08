using Microsoft.Xna.Framework;
using System;
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
                for (int y = 1; y < SlotRows; y++)
                {
                    // Get item at slot
                    int i = x + (y * SlotCols);
                    ItemCount itemCount = game.Inventory.GetSlot(i);

                    // Skip item if empty
                    if (itemCount.IsEmpty) continue;

                    // Get slot rect
                    Vector2 offset = new Vector2(x, y) * Drawing.Grid;
                    Vector2 slotPosition = position + offset;
                    Rectangle slotRect = new Rectangle(slotPosition.ToPoint(), new Point(Drawing.Grid));

                    // Draw item to rect
                    Drawing.DrawSprite(game, Drawing.ItemsTileset, slotRect, (int)itemCount.Item, Layers.UI);
                    if (itemCount.Count > 1) Drawing.DrawText(game, itemCount.Count.ToString(), slotPosition, Color.Black);
                }
            }
        }

        public override void OnLeftClick(Game1 game, Point mousePosition)
        {
            Point click = mousePosition - position.ToPoint();
            int clickX = click.X / Drawing.Grid;
            int clickY = click.Y / Drawing.Grid;
            int slot = clickX + ((clickY + 1) * SlotCols);
            Console.WriteLine($"Clicked slot {slot}");
        }
    }
}
