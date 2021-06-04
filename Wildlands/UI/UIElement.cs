using Microsoft.Xna.Framework;

namespace Wildlands.UI
{
    public enum UIAnchorX
    {
        Left,
        Center,
        Right
    }

    public enum UIAnchorY
    {
        Top,
        Center,
        Bottom
    }

    public abstract class UIElement
    {
        private UIAnchorX anchorX;
        private UIAnchorY anchorY;
        private Vector2 offset;
        private Vector2 size;
        protected Vector2 position;

        public Rectangle Bounds => new Rectangle(position.ToPoint(), size.ToPoint());

        public UIElement(UIAnchorX anchorX, UIAnchorY anchorY, float offsetX, float offsetY, float width, float height)
        {
            this.anchorX = anchorX; // Set anchor x
            this.anchorY = anchorY; // Set anchor y
            size = new Vector2(width, height); // Set element size
            offset = new Vector2(offsetX, offsetY); // Set element position offset

            // Update element position
            UpdatePosition();
        }

        public abstract void Draw(Game1 game);

        public virtual void OnSave() { }
        public virtual void OnLoad() { }

        // Updates element position based on screen size
        public void UpdatePosition()
        {
            // Get screen width and height
            int screenWidth = Drawing.ScreenWidth;
            int screenHeight = Drawing.ScreenHeight;

            // Calculate x position from anchor
            float posX = 0;
            switch (anchorX)
            {
                // Left anchor
                case UIAnchorX.Left: posX = 0; break;
                // Center anchor
                case UIAnchorX.Center: posX = (screenWidth / 2) - (size.X / 2); break;
                // Right anchor
                case UIAnchorX.Right: posX = screenWidth - size.X; break;
            }

            // Calculate y position from anchor
            float posY = 0;
            switch (anchorY)
            {
                // Top anchor
                case UIAnchorY.Top: posY = 0; break;
                // Center anchor
                case UIAnchorY.Center: posY = (screenHeight / 2) - (size.Y / 2); break;
                // Bottom anchor
                case UIAnchorY.Bottom: posY = screenHeight - size.Y; break;
            }

            position = new Vector2(posX, posY) + offset; // Set element position
        }
    }
}
