using Microsoft.Xna.Framework;

namespace Wildlands.UI
{
    public class Inventory : UIElement
    {
        public Inventory(UIAnchorX anchorX, UIAnchorY anchorY, float offsetX, float offsetY, float width, float height) :
            base(anchorX, anchorY, offsetX, offsetY, width, height) { }

        public override void Draw(Game1 game)
        {
            Drawing.DrawRect(game, Bounds, Color.White);
        }
    }
}
