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
        private Vector2 position;
        private Vector2 size;

        public Rectangle Bounds => new Rectangle(position.ToPoint(), size.ToPoint());

        public UIElement(UIAnchorX anchorX, UIAnchorY anchorY, float offsetX, float offsetY, float width, float height)
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
                case UIAnchorX.Center: posX = (screenWidth / 2) - (width / 2); break;
                // Right anchor
                case UIAnchorX.Right: posX = screenWidth - width; break;
            }

            // Calculate y position from anchor
            float posY = 0;
            switch (anchorY)
            {
                // Top anchor
                case UIAnchorY.Top: posY = 0; break;
                // Center anchor
                case UIAnchorY.Center: posY = (screenHeight / 2) - (height / 2); break;
                // Bottom anchor
                case UIAnchorY.Bottom: posY = screenHeight - height; break;
            }

            position = new Vector2(posX + offsetX, posY + offsetY); // Set element position
            size = new Vector2(width, height); // Set element size
        }
    }
}
