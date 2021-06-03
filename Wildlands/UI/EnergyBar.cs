using Microsoft.Xna.Framework;
using System;

namespace Wildlands.UI
{
    public class EnergyBar : UIElement
    {
        // Energy data
        private const int MaxEnergy = 100;
        public int Energy { get; private set; } = MaxEnergy;

        public EnergyBar() : base(UIAnchorX.Right, UIAnchorY.Top, -8, 8, MaxEnergy, 16) { }

        public override void Draw(Game1 game)
        {
            // Draw background
            Drawing.DrawRect(game, Bounds, Color.White);

            // Draw energy bar
            Rectangle energyRect = new Rectangle(position.ToPoint(), new Point(Energy, 16));
            Drawing.DrawRect(game, energyRect, Color.Orange);
        }

        // Attempts to use given amount of energy and returns whether successful
        public bool UseEnergy(int amount)
        {
            if (Energy < amount) return false;
            Energy -= amount;
            return true;
        }

        // Attempts to give amount of energy and returns whether successful
        public bool GiveEnergy(int amount)
        {
            if (Energy == MaxEnergy) return false;
            Energy = Math.Min(Energy + amount, MaxEnergy);
            return true;
        }
    }
}
