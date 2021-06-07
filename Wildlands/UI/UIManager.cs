using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Wildlands.UI
{
    public class UIManager
    {
        public Slots Slots { get; private set; } = new Slots();
        public Hotbar Hotbar { get; private set; } = new Hotbar();
        public EnergyBar EnergyBar { get; private set; } = new EnergyBar();

        public bool MenuOpen { get; private set; }

        private readonly List<UIElement> uiElements = new List<UIElement>();

        public UIManager()
        {
            uiElements.Add(Slots);
            uiElements.Add(Hotbar);
            uiElements.Add(EnergyBar);
        }

        public void Update(Game1 game)
        {
            // Toggle menu open
            if (game.IsKeyPressed(Keys.E)) MenuOpen = !MenuOpen;
        }

        public void Draw(Game1 game)
        {
            // Draw UI elements
            foreach (UIElement elem in uiElements) elem.Draw(game);
        }

        public void OnSave()
        {
            // Save all UI elements
            foreach (UIElement elem in uiElements) elem.OnSave();
        }

        public void OnLoad()
        {
            // Load all UI elements
            foreach (UIElement elem in uiElements) elem.OnLoad();
        }

        public void ProcessClick(Game1 game, Point mousePosition) { }

        public void UpdatePositioning()
        {
            // Update all UI element positions
            foreach (UIElement elem in uiElements) elem.UpdatePosition();
        }
    }
}
