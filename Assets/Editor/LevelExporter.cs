using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Assets.Scripts;
using Assets.Scripts.LevelEditor;

namespace Assets.Editor
{
    public class LevelExporter
    {

        private readonly IGridEditorSettings _gridEditorSettings;

        public LevelExporter(IGridEditorSettings gridEditorSettings)
        {
            _gridEditorSettings = gridEditorSettings;
        }

        public bool Export()
        {
            var levelData = _gridEditorSettings.GetExportData();
            string filename = GameConstants.Levels.ResourcePath + "level-" + levelData.OrdinalNumber + ".bin";


            using (var fileStream = File.Open(filename, FileMode.Create))
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, levelData);
            }
            return true;
        }

    }


}