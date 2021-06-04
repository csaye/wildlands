using System.IO;
using System.Xml.Serialization;

namespace Wildlands.SaveLoad
{
    public static class SerializationManager
    {
        public static SaveData Load(string path)
        {
            TextReader reader = new StreamReader(path);
            XmlSerializer xml = new XmlSerializer(typeof(SaveData));
            return (SaveData)xml.Deserialize(reader);
        }

        public static void Save(string path, SaveData obj)
        {
            TextWriter writer = new StreamWriter(path);
            XmlSerializer xml = new XmlSerializer(typeof(SaveData));
            xml.Serialize(writer, obj);
            writer.Close();
        }
    }
}
