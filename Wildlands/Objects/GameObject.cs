using Microsoft.Xna.Framework;

namespace Wildlands.Objects
{
    public abstract class GameObject
    {
        protected Vector2 position;
        protected Vector2 size;

        public Rectangle Bounds => new Rectangle(position.ToPoint(), size.ToPoint());

        public Vector2 Position => position;
        public Vector2 Size => size;

        public GameObject(float x, float y, int width, int height)
        {
            position = new Vector2(x, y);
            size = new Vector2(width, height);
        }

        public virtual void Update(Game1 game, float delta) { }
        public abstract void Draw(Game1 game);

        public virtual void OnSave(Game1 game) { }
        public virtual void OnLoad(Game1 game) { }
    }
}
