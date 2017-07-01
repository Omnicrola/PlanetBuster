using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Assets.Scripts.LevelEditor;
using Assets.Scripts.Models;

namespace Assets.Editor
{
    public class LevelImporter
    {
        private readonly IGridEditorSettings _gridEditorSettings;

        public LevelImporter(IGridEditorSettings gridEditorSettings)
        {
            _gridEditorSettings = gridEditorSettings;
        }

        public bool Import(string filename)
        {
            var levelSummary = LoadLevelSummary(filename);
            if (levelSummary == null)
            {
                return false;
            }
            _gridEditorSettings.SetLevelData(levelSummary);

            return true;
        }


        private static LevelSummary LoadLevelSummary(string filename)
        {
            using (var fileStream = File.Open(filename, FileMode.Open))
            {
                var binaryFormatter = new BinaryFormatter();
                var fileData = binaryFormatter.Deserialize(fileStream);
                return fileData as LevelSummary;
            }
            return null;
        }
    }
}