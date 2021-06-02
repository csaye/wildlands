using Microsoft.Xna.Framework;
using System;

namespace Wildlands.Tiles
{
    public class TileManager
    {
        private Tile[,] tiles;

        private const int TilesWidth = Drawing.SceneGridWidth;
        private const int TilesHeight = Drawing.SceneGridHeight;

        private const int Grid = Drawing.Grid;

        public TileManager()
        {
            tiles = new Tile[TilesWidth, TilesHeight];

            // Initialize tiles
            for (int x = 0; x < TilesWidth; x++)
            {
                for (int y = 0; y < TilesHeight; y++)
                {
                    tiles[x, y] = Tile.Grass;
                }
            }
        }

        public void Draw(Game1 game)
        {
            // Get camera position in tile space
            Vector2 camPosition = game.Camera.Position / Grid;

            // Calculate minimum and maximum visible positions
            int minX = (int)camPosition.X;
            int minY = (int)camPosition.Y;
            int maxX = minX + (int)Math.Ceiling((float)Drawing.ScreenWidth / Grid);
            int maxY = minY + (int)Math.Ceiling((float)Drawing.ScreenHeight / Grid);

            // For each viewable position
            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY + 1; y++)
                {
                    // Skip if position out of range
                    if (x < 0 || x > TilesWidth - 1 || y < 0 || y > TilesHeight - 1) continue;

                    // Get tile and rect at position
                    Tile tile = tiles[x, y];
                    Rectangle rect = new Rectangle(x * Grid, y * Grid, Grid, Grid);

                    // Draw tile
                    switch (tile)
                    {
                        case Tile.Empty: break;
                        case Tile.Grass: Drawing.DrawRect(game, rect, Color.Green); break;
                    }
                }
            }
        }
    }
}
