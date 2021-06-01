using Microsoft.Xna.Framework;
using System;

namespace Wildlands
{
    public class Camera
    {
        public Matrix Transform { get; private set; }
        public Vector2 Position { get; private set; }

        public Camera() { }

        public void Update(Game1 game)
        {
            // Get screen midpoints
            int midWidth = Drawing.ScreenWidth / 2;
            int midHeight = Drawing.ScreenHeight / 2;

            // Get scene endpoints
            int maxWidth = Drawing.SceneWidth - midWidth;
            int maxHeight = Drawing.SceneHeight - midHeight;

            // Get player position and size
            Vector2 playerPosition = game.Player.Position;
            Vector2 playerSize = game.Player.Size;

            // Get camera target x
            int cameraX;
            if (midWidth < maxWidth)
            {
                // Set to player position clamped within scene bounds
                int playerX = (int)(playerPosition.X + (playerSize.X / 2));
                cameraX = Math.Clamp(playerX, midWidth, maxWidth);
            }
            // If camera view exceeds scene, set to center
            else cameraX = Drawing.SceneWidth / 2;

            // Get camera target y
            int cameraY;
            if (midHeight < maxHeight)
            {
                // Set to player position clamped within scene bounds
                int playerY = (int)(playerPosition.Y + (playerSize.Y / 2));
                cameraY = Math.Clamp(playerY, midHeight, maxHeight);
            }
            // If camera view exceeds scene, set to center
            else cameraY = Drawing.SceneHeight / 2;

            // Set camera position
            Position = new Vector2(cameraX - midWidth, cameraY - midHeight);

            // Update transform matrix
            Matrix target = Matrix.CreateTranslation(-cameraX, -cameraY, 0);
            Matrix offset = Matrix.CreateTranslation(midWidth, midHeight, 0);
            Transform = target * offset;
        }
    }
}
