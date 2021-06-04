using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using Wildlands.SaveLoad;

namespace Wildlands.Objects
{
    public class Player : GameObject
    {
        private Vector2 movementDirection;

        private const float Speed = 100; // Player movement speed

        public Player(float x, float y, int width, int height) : base(x, y, width, height) { }

        public override void Update(Game1 game, float delta)
        {
            Move(game, delta);
        }

        public override void Draw(Game1 game)
        {
            Drawing.DrawRect(game, Bounds, Color.Blue);
        }

        public override void OnSave()
        {
            // Save player position
            SaveData.Current.playerData.xPos = position.X;
            SaveData.Current.playerData.yPos = position.Y;
        }

        public override void OnLoad()
        {
            // Load player position
            float xPos = SaveData.Current.playerData.xPos;
            float yPos = SaveData.Current.playerData.yPos;
            position = new Vector2(xPos, yPos);
        }

        private void Move(Game1 game, float delta)
        {
            // Reset movement direction
            movementDirection = Vector2.Zero;

            // Get movement direction
            if (game.IsKeyDown(Keys.W)) movementDirection.Y -= 1; // Up
            if (game.IsKeyDown(Keys.S)) movementDirection.Y += 1; // Down
            if (game.IsKeyDown(Keys.D)) movementDirection.X += 1; // Right
            if (game.IsKeyDown(Keys.A)) movementDirection.X -= 1; // Left
            if (movementDirection.Length() > 1) movementDirection.Normalize(); // Normalize

            // Get new position and clamp within scene bounds
            Vector2 newPosition = position + movementDirection * delta * Speed;
            newPosition.X = Math.Clamp(newPosition.X, 0, Drawing.SceneWidth - size.X);
            newPosition.Y = Math.Clamp(newPosition.Y, 0, Drawing.SceneHeight - size.Y);

            // Move player
            position = game.ObjectManager.NearestEmptyPosition(this, newPosition);
        }
    }
}
