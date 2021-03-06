using Microsoft.Xna.Framework;
using System;

namespace Wildlands.Tiles
{
    public class TileManager
    {
        private readonly Tile[,] tiles;

        private const int GridWidth = Drawing.GridWidth;
        private const int GridHeight = Drawing.GridHeight;

        private const int Grid = Drawing.Grid;

        public TileManager()
        {
            tiles = new Tile[GridWidth, GridHeight];
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
                for (int y = minY; y <= maxY; y++)
                {
                    // Skip if position out of range
                    if (x < 0 || x > GridWidth - 1 || y < 0 || y > GridHeight - 1) continue;

                    // Get tile and rect at position
                    Tile tile = tiles[x, y];
                    Rectangle tileRect = new Rectangle(x * Grid, y * Grid, Grid, Grid);

                    // Skip tile if empty
                    if (tile == Tile.None) continue;

                    // Draw tile
                    Drawing.DrawSprite(game, Drawing.TilesTileset, tileRect, (int)tile, Layers.Tiles);
                }
            }
        }

        public void OnSave(Game1 game)
        {
            // Save tile data to file
            Tile[] tiles1D = new Tile[GridWidth * GridHeight];
            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    int i = x + (y * GridWidth);
                    tiles1D[i] = tiles[x, y];
                }
            }

            game.SaveData.tileData.tiles = tiles1D;
        }

        public void OnLoad(Game1 game)
        {
            // Load tile data from file
            Tile[] tiles1D = game.SaveData.tileData.tiles;
            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    int i = x + (y * GridWidth);
                    tiles[x, y] = tiles1D[i];
                }
            }
        }
    }
}
