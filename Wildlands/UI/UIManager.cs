using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Wildlands.UI
{
    public class UIManager
    {
        public Inventory Inventory { get; private set; }

        public bool MenuActive { get; private set; }

        private List<UIElement> uiElements = new List<UIElement>();

        public UIManager()
        {
            Inventory = new Inventory(UIAnchorX.Center, UIAnchorY.Bottom, 0, -Drawing.Grid, Drawing.Grid * 10, Drawing.Grid);
            uiElements.Add(Inventory);
        }

        public void UpdatePositioning()
        {
            // Update all UI element positions
            foreach (UIElement elem in uiElements) elem.UpdatePosition();
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
                // Draw all UI elements
                foreach (UIElement elem in uiElements) elem.Draw(game);
            }
        }
    }
}
