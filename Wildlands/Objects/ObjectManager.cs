using System.Collections.Generic;

namespace Wildlands.Objects
{
    public class ObjectManager
    {
        private List<GameObject> gameObjects;

        public ObjectManager() { }

        public void AddObject(GameObject obj) => gameObjects.Add(obj);

        public void Update(Game1 game, float delta)
        {
            // Update each GameObject
            foreach (GameObject obj in gameObjects) obj.Update(game, delta);
        }

        public void Draw(Game1 game)
        {
            // Draw each GameObject
            foreach (GameObject obj in gameObjects) obj.Draw(game);
        }
    }
}
