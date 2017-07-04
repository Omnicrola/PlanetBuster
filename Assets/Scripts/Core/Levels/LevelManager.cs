using System.Collections.Generic;
using Assets.Scripts.Models;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Core.Levels
{
    public class LevelManager : ILevelManager
    {
        private LevelDataReader _levelDataReader;
        private LevelMetadataReader _levelMetadataReader;

        public LevelManager()
        {
            _levelDataReader = new LevelDataReader();
            _levelMetadataReader = new LevelMetadataReader(Application.persistentDataPath);
        }

        public LevelMetadata GetAll()
        {
            return _levelMetadataReader.Read();
        }


    }
}