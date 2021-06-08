namespace Wildlands.SaveLoad
{
    public static class SaveLoadManager
    {
        // Saves current game data to file
        public static void Save(Game1 game)
        {
            // Retrieve save data
            game.OnSave();

            // Serialize save data
            SerializationManager.Save("Save", game.SaveData);
        }

        // Loads game data from file
        public static void Load(Game1 game)
        {
            // Retrieve save data
            game.SaveData = SerializationManager.Load("Save");

            // Load save data
            game.OnLoad();
        }
    }
}
