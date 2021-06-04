using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Wildlands.UI
{
    public class UIManager
    {
        public Inventory Inventory { get; private set; } = new Inventory();
        public EnergyBar EnergyBar { get; private set; } = new EnergyBar();

        public bool MenuOpen { get; private set; }

        private readonly List<UIElement> baseUI = new List<UIElement>();
        private readonly List<UIElement> menuUI = new List<UIElement>();

        public UIManager()
        {
            // Base UI
            baseUI.Add(EnergyBar);

            // Menu UI
            menuUI.Add(Inventory);
        }

        public void Update(Game1 game)
        {
            // Toggle menu open
            if (game.IsKeyPressed(Keys.E)) MenuOpen = !MenuOpen;
        }

        public void Draw(Game1 game)
        {
            // Draw all base UI elements
            foreach (UIElement elem in baseUI) elem.Draw(game);

            // If menu open
            if (MenuOpen)
            {
                // Draw all menu UI elements
                foreach (UIElement elem in menuUI) elem.Draw(game);
            }
        }

        public void OnSave()
        {
            // Save all UI elements
            foreach (UIElement elem in baseUI) elem.OnSave();
            foreach (UIElement elem in menuUI) elem.OnSave();
        }

        public void OnLoad()
        {
            // Load all UI elements
            foreach (UIElement elem in baseUI) elem.OnLoad();
            foreach (UIElement elem in menuUI) elem.OnLoad();
        }

        public void ProcessClick(Game1 game, Point mousePosition) { }

        public void UpdatePositioning()
        {
            // Update all UI element positions
            foreach (UIElement elem in baseUI) elem.UpdatePosition();
            foreach (UIElement elem in menuUI) elem.UpdatePosition();
        }
    }
}
