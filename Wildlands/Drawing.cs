using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Wildlands
{
    public static class Drawing
    {
        // Grid and pixel size
        public const int Grid = 32;
        private const int PixelsPerGrid = 16;
        public const int Pixel = Grid / PixelsPerGrid;

        // Screen size
        private const int DefaultScreenWidth = 1280;
        private const int DefaultScreenHeight = 720;
        public static int ScreenWidth { get; private set; } = DefaultScreenWidth;
        public static int ScreenHeight { get; private set; } = DefaultScreenHeight;
        public static void SetScreenWidth(int newWidth) => ScreenWidth = newWidth;
        public static void SetScreenHeight(int newHeight) => ScreenHeight = newHeight;

        // Scene size
        public const int GridWidth = 256;
        public const int GridHeight = 256;
        public const int SceneWidth = GridWidth * Grid;
        public const int SceneHeight = GridHeight * Grid;

        // Content
        public static Texture2D TilesTileset { get; private set; }
        public static Texture2D ItemsTileset { get; private set; }
        private static SpriteFont arialFont;

        private static Texture2D blankTexture;

        public static void InitializeGraphics(Game1 game)
        {
            // Initialize screen size
            game.Graphics.PreferredBackBufferWidth = DefaultScreenWidth;
            game.Graphics.PreferredBackBufferHeight = DefaultScreenHeight;
            game.Graphics.ApplyChanges();

            // Initialize blank texture
            blankTexture = new Texture2D(game.GraphicsDevice, 1, 1);
            blankTexture.SetData(new Color[] { Color.White });
        }

        public static void LoadContent(Game1 game)
        {
            // Load tilesets
            TilesTileset = game.Content.Load<Texture2D>("Tilesets/Tiles");
            ItemsTileset = game.Content.Load<Texture2D>("Tilesets/Items");

            // Load spritefonts
            arialFont = game.Content.Load<SpriteFont>("Fonts/Arial");
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

        // Draws given text to sprite batch
        public static void DrawText(Game1 game, string text, Vector2 position, Color color)
        {
            game.SpriteBatch.DrawString(arialFont, text, position, color);
        }
    }
}
