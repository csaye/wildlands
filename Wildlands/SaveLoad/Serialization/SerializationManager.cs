using System;
using System.IO;
using System.Xml.Serialization;

namespace Wildlands.SaveLoad
{
    public static class SerializationManager
    {
        private const string ProjectName = "Wildlands";

        private static string DataPath => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public static void Save(string fileName, SaveData obj)
        {
            // Verify folder structure and get file path
            VerifyFolderStructure();
            string path = Path.Combine(DataPath, ProjectName, "Saves", $"{fileName}.xml");

            // Write save data to file
            TextWriter writer = new StreamWriter(path);
            XmlSerializer xml = new XmlSerializer(typeof(SaveData));
            xml.Serialize(writer, obj);
            writer.Close();
        }

        public static SaveData Load(string fileName)
        {
            // Get path and return new save if no file exists
            string path = Path.Combine(DataPath, ProjectName, "Saves", $"{fileName}.xml");
            if (!File.Exists(path)) return new SaveData();

            // Read save data from file
            TextReader reader = new StreamReader(path);
            XmlSerializer xml = new XmlSerializer(typeof(SaveData));
            return (SaveData)xml.Deserialize(reader);
        }

        // Verifies that all necessary folders are created
        private static void VerifyFolderStructure()
        {
            string projectFolder = Path.Combine(DataPath, ProjectName);
            if (!Directory.Exists(projectFolder)) Directory.CreateDirectory(projectFolder);
            string savesFolder = Path.Combine(projectFolder, "Saves");
            if (!Directory.Exists(savesFolder)) Directory.CreateDirectory(savesFolder);
        }
    }
}
