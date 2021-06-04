using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Wildlands.Objects
{
    public class ObjectManager
    {
        private readonly List<GameObject> gameObjects = new List<GameObject>();

        public ObjectManager() { }

        public void AddObject(GameObject obj) => gameObjects.Add(obj);

        public void Update(Game1 game, float delta)
        {
            // Update each object
            foreach (GameObject obj in gameObjects) obj.Update(game, delta);
        }

        public void Draw(Game1 game)
        {
            // Draw each object
            foreach (GameObject obj in gameObjects) obj.Draw(game);
        }

        public void ProcessClick(Game1 game, Point mousePosition)
        {

        }

        // Processes the intersection between two bounds and returns a corrected new bounds position
        private Vector2 ProcessIntersection(Rectangle newBounds, Rectangle objBounds, Vector2 newPosition)
        {
            // If object bounds intersect
            if (newBounds.Intersects(objBounds))
            {
                // Get object centers
                Point newCenter = newBounds.Center;
                Point objCenter = objBounds.Center;

                // Get horizontal and vertical displacements
                float displacementX = Math.Abs(newCenter.X - objCenter.X);
                float displacementY = Math.Abs(newCenter.Y - objCenter.Y);

                // If greater horizontal displacement
                if (displacementX > displacementY)
                {
                    // If right of object
                    if (newCenter.X > objCenter.X) newPosition.X = objBounds.Right;
                    // If left of object
                    else newPosition.X = objBounds.Left - newBounds.Width;
                }
                // If greater vertical displacement
                else
                {
                    // If below object
                    if (newCenter.Y > objCenter.Y) newPosition.Y = objBounds.Bottom;
                    // If above object
                    else newPosition.Y = objBounds.Top - newBounds.Height;
                }
            }

            // Return corrected new position
            return newPosition;
        }

        // Finds the nearest empty position for given object moving to given position
        public Vector2 NearestEmptyPosition(GameObject movingObj, Vector2 newPosition)
        {
            // Get new bounds
            Rectangle newBounds = movingObj.Bounds;
            newBounds.X = (int)newPosition.X;
            newBounds.Y = (int)newPosition.Y;

            // For each object
            foreach (GameObject obj in gameObjects)
            {
                // Skip self
                if (obj == movingObj) continue;

                // Process intersection between objects
                newPosition = ProcessIntersection(newBounds, obj.Bounds, newPosition);

                // Update new bounds
                newBounds.X = (int)newPosition.X;
                newBounds.Y = (int)newPosition.Y;
            }

            // Return corrected new position
            return newPosition;
        }
    }
}
