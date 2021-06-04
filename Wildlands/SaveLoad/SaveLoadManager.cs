namespace Wildlands.SaveLoad
{
    public static class SaveLoadManager
    {
        // Saves current game data to file
        public static void Save()
        {
            SerializationManager.Save("./Save", SaveData.Current);
        }

        // Loads game data from file
        public static void Load()
        {
            SerializationManager.Load("./Save");
        }
    }
}
