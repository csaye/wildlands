using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Wildlands.Objects
{
    public class ObjectManager
    {
        private const int Grid = Drawing.Grid;

        private const int GridWidth = Drawing.GridWidth;
        private const int GridHeight = Drawing.GridHeight;

        private readonly List<GameObject> dynamicObjects = new List<GameObject>();
        private readonly GameObject[,] staticObjects = new GameObject[GridWidth, GridHeight];

        public ObjectManager() { }

        public void AddDynamicObject(GameObject obj) => dynamicObjects.Add(obj);
        public void AddStaticObject(GameObject gameObject)
        {
            int posX = (int)(gameObject.Position.X / Grid);
            int posY = (int)(gameObject.Position.Y / Grid);
            staticObjects[posX, posY] = gameObject;
        }

        public void Update(Game1 game, float delta)
        {
            // Update each dynamic object
            foreach (GameObject obj in dynamicObjects) obj.Update(game, delta);
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

            // For each dynamic object
            foreach (GameObject obj in dynamicObjects)
            {
                // If object position within camera bounds, draw object
                Vector2 pos = obj.Position / Grid;
                if (pos.X >= minX && pos.X <= maxX && pos.Y >= minY && pos.Y <= maxY) obj.Draw(game);
            }

            // For each viewable position
            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    // Skip if position out of range
                    if (x < 0 || x > GridWidth - 1 || y < 0 || y > GridHeight - 1) continue;

                    // Get static object at position and skip if null
                    GameObject obj = staticObjects[x, y];
                    if (obj == null) continue;

                    // Draw object
                    obj.Draw(game);
                }
            }
        }

        public void OnSave()
        {
            // Save all objects
            foreach (GameObject obj in dynamicObjects) obj.OnSave();
            foreach (GameObject obj in staticObjects)
            {
                if (obj != null) obj.OnSave();
            }
        }

        public void OnLoad()
        {
            // Load all objects
            foreach (GameObject obj in dynamicObjects) obj.OnLoad();
        }

        public void ProcessClick(Game1 game, Point mousePosition) { }

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

            // Calculate new bounds on grid
            int leftBound = newBounds.Left / Drawing.Grid;
            int rightBound = newBounds.Right / Drawing.Grid;
            int topBound = newBounds.Top / Drawing.Grid;
            int bottomBound = newBounds.Bottom / Drawing.Grid;

            // For each static object in new bound
            for (int x = leftBound; x <= rightBound; x++)
            {
                for (int y = topBound; y <= bottomBound; y++)
                {
                    // Skip position if out of bounds
                    if (x < 0 || x > GridWidth - 1 || y < 0 || y > GridHeight - 1) continue;

                    // Get object at position and skip if null
                    GameObject obj = staticObjects[x, y];
                    if (obj == null) continue;

                    // Process intersection between objects
                    newPosition = ProcessIntersection(newBounds, obj.Bounds, newPosition);

                    // Update new bounds
                    newBounds.X = (int)newPosition.X;
                    newBounds.Y = (int)newPosition.Y;
                }
            }

            // For each dynamic object
            foreach (GameObject obj in dynamicObjects)
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
