using System;
using System.IO;
using System.Linq;
using Assets.Scripts;

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
            var filenames = Directory.GetFiles(_pathToLevels).Where(level => level.Contains("level-")).ToList();
            Console.WriteLine(filenames);
        }
    }
}