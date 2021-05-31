using Microsoft.Xna.Framework;
using System;

namespace Wildlands
{
    public class Camera
    {
        public Matrix Transform { get; private set; }
        public Vector2 Position { get; private set; }

        // Screen midpoints
        private const int MidWidth = Drawing.ScreenWidth / 2;
        private const int MidHeight = Drawing.ScreenHeight / 2;

        private readonly Matrix Offset = Matrix.CreateTranslation(MidWidth, MidHeight, 0);

        // Scene endpoints
        private const int MaxWidth = Drawing.SceneWidth - MidWidth;
        private const int MaxHeight = Drawing.SceneHeight - MidHeight;

        public Camera() { }

        public void Update(Game1 game)
        {
            // Get player position and size
            Vector2 playerPosition = game.Player.Position;
            Vector2 playerSize = game.Player.Size;

            // Get camera position
            int cameraX = (int)(playerPosition.X + (playerSize.X / 2));
            int cameraY = (int)(playerPosition.Y + (playerSize.Y / 2));

            // Clamp camera position within scene bounds
            cameraX = Math.Clamp(cameraX, MidWidth, MaxWidth);
            cameraY = Math.Clamp(cameraY, MidHeight, MaxHeight);

            // Set camera position
            Position = new Vector2(cameraX - MidWidth, cameraY - MidHeight);

            // Flip camera x and y
            cameraX *= -1;
            cameraY *= -1;

            // Update transform matrix
            Matrix target = Matrix.CreateTranslation(cameraX, cameraY, 0);
            Transform = target * Offset;
        }
    }
}
