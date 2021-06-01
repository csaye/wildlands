﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Wildlands.Objects
{
    public class Player : GameObject
    {
        private Vector2 movementDirection;

        private const float Speed = 100; // Player movement speed

        public Player(float x, float y, int width, int height) : base(x, y, width, height) { }

        public override void Update(Game1 game, float delta)
        {
            // Reset movement direction
            movementDirection = Vector2.Zero;

            // Get movement direction
            if (game.IsKeyDown(Keys.W)) movementDirection.Y -= 1; // Up
            if (game.IsKeyDown(Keys.S)) movementDirection.Y += 1; // Down
            if (game.IsKeyDown(Keys.D)) movementDirection.X += 1; // Right
            if (game.IsKeyDown(Keys.A)) movementDirection.X -= 1; // Left
            if (movementDirection.Length() > 1) movementDirection.Normalize(); // Normalize

            // Move player
            Vector2 newPosition = position + movementDirection * delta * Speed;
            position = game.ObjectManager.NearestEmptyPosition(this, newPosition);
        }

        public override void Draw(Game1 game)
        {
            Drawing.DrawRect(game, Bounds, Color.Blue);
        }
    }
}
