using Microsoft.Xna.Framework.Input;

namespace Wildlands.UI
{
    public class UIManager
    {
        public Inventory Inventory { get; private set; }

        public bool MenuActive { get; private set; }

        public UIManager()
        {
            Inventory = new Inventory(UIAnchorX.Center, UIAnchorY.Bottom, 0, -Drawing.Grid, Drawing.Grid * 10, Drawing.Grid);
        }

        public void Update(Game1 game)
        {
            // Toggle menu active
            if (game.IsKeyPressed(Keys.E)) MenuActive = !MenuActive;
        }

        public void Draw(Game1 game)
        {
            // If menu active
            if (MenuActive)
            {
                Inventory.Draw(game); // Draw inventory
            }
        }
    }
}
