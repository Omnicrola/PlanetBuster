using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Assets.Scripts;
using Assets.Scripts.Models;

namespace Assets.Editor
{
    public class LevelMetaDataGenerator
    {
        private readonly string _pathToLevels;

        public LevelMetaDataGenerator(string pathToLevels)
        {
            _pathToLevels = pathToLevels;
        }

        public void Regenerate()
        {
            var levelMetadata = CreateMetadata();


            var metadataFile = _pathToLevels + GameConstants.Levels.MetadataFile;
            using (var fileStream = File.Open(metadataFile, FileMode.Create))
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, levelMetadata);
            }
        }

        private LevelMetadata CreateMetadata()
        {
            var levelMetadata = new LevelMetadata();
            foreach (var levelFile in GetAllLevelFiles())
            {
                levelMetadata.Levels.Add(new LevelMetaResource
                {
                    OrdinalNumber = levelFile.OrdinalNumber,
                    LevelName = levelFile.LevelName,
                    ResourceName = ExportUtil.ConstructFilename(levelFile.OrdinalNumber)
                });
            }
            return levelMetadata;
        }

        private IEnumerable<LevelSummary> GetAllLevelFiles()
        {
            return Directory
                .GetFiles(_pathToLevels)
                .Select(ReadSummary);
        }

        private LevelSummary ReadSummary(string filepath)
        {
            using (var fileStream = File.Open(filepath, FileMode.Open))
            {
                var binaryFormatter = new BinaryFormatter();
                return binaryFormatter.Deserialize(fileStream) as LevelSummary;
            }
        }
    }
}