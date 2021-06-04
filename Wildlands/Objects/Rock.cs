using Microsoft.Xna.Framework;

namespace Wildlands.Objects
{
    public class Rock : GameObject
    {
        public Rock(float x, float y, int width, int height) : base(x, y, width, height) { }

        public override void Draw(Game1 game)
        {
            Drawing.DrawRect(game, Bounds, Color.Gray);
        }
    }
}
