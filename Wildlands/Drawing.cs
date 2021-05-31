using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Wildlands
{
    public static class Drawing
    {
        // Grid and pixel size
        public const int Grid = 32;
        public const int PixelsPerGrid = 16;
        public const int Pixel = Grid / PixelsPerGrid;

        // Grid dimensions
        public const int GridWidth = 16;
        public const int GridHeight = 16;

        // Screen size
        public const int ScreenWidth = GridWidth * Grid;
        public const int ScreenHeight = GridHeight * Grid;

        private static Texture2D blankTexture;

        public static void InitializeGraphics(Game1 game)
        {
            // Set screen size
            game.Graphics.PreferredBackBufferHeight = ScreenHeight;
            game.Graphics.PreferredBackBufferWidth = ScreenWidth;
            game.Graphics.ApplyChanges();

            // Initialize blank texture
            blankTexture = new Texture2D(game.GraphicsDevice, 1, 1);
            blankTexture.SetData(new Color[] { Color.White });
        }

        // Draws given rect of given color to sprite batch
        public static void DrawRect(Game1 game, Rectangle rect, Color color)
        {
            game.SpriteBatch.Draw(blankTexture, rect, color);
        }

        // Draws given sprite to sprite batch
        public static void DrawSprite(Game1 game, Texture2D texture, Rectangle rect, int tilesetIndex, float layer)
        {
            // Get sprite width and height in source texture
            int spriteWidth = rect.Width / Pixel;
            int spriteHeight = rect.Height / Pixel;

            // Calculate sprite source rect
            int spritesPerRow = texture.Width / spriteWidth;
            int tilesetX = tilesetIndex % spritesPerRow * spriteWidth;
            int tilesetY = tilesetIndex / spritesPerRow * spriteHeight;
            Rectangle sourceRect = new Rectangle(tilesetX, tilesetY, spriteWidth, spriteHeight);

            // Draw sprite to sprite batch
            game.SpriteBatch.Draw(texture, rect, sourceRect, Color.White, 0, Vector2.Zero, SpriteEffects.None, layer);
        }
    }
}
