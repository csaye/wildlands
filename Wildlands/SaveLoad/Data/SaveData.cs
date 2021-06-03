namespace Wildlands.SaveLoad
{
    public class SaveData
    {
        private static SaveData _current;
        public static SaveData Current
        {
            get
            {
                if (_current == null) _current = new SaveData();
                return _current;
            }
            set
            {
                if (value != null) _current = value;
            }
        }

        public SaveData() { } 

        public string saveName = "default";
    }
}
