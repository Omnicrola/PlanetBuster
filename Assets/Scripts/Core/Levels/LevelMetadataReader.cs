using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Assets.Scripts.Models;

namespace Assets.Scripts.Core.Levels
{
    public class LevelMetadataReader
    {
        private readonly string _baseFilepath;

        public LevelMetadataReader(string baseFilepath)
        {
            _baseFilepath = baseFilepath;
        }

        public LevelMetadata Read()
        {
            var metadataFile = _baseFilepath + GameConstants.Levels.MetadataFile;
            using (var filestream = File.Open(metadataFile, FileMode.Open))
            {
                var binaryFormatter = new BinaryFormatter();
                return binaryFormatter.Deserialize(filestream) as LevelMetadata;
            }
        }
    }
}