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

            for (int i = 0; i < SlotCols; i++)
            {
                // Get item at slot
                ItemCount itemCount = game.Inventory.GetSlot(i);

                // Skip item if empty
                if (itemCount.IsEmpty) continue;

                // Get slot rect
                Vector2 offset = new Vector2(i, 0) * Drawing.Grid;
                Vector2 slotPosition = position + offset;
                Rectangle slotRect = new Rectangle(slotPosition.ToPoint(), new Point(Drawing.Grid));

                // Draw item to rect
                Drawing.DrawSprite(game, Drawing.ItemsTileset, slotRect, (int)itemCount.Item, Layers.UI);
                if (itemCount.Count > 1) Drawing.DrawText(game, itemCount.Count.ToString(), slotPosition, Color.Black);
            }
        }
    }
}
