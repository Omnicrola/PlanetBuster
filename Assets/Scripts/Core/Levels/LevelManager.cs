using System.Collections.Generic;
using Assets.Scripts.Models;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Core.Levels
{
    public class LevelManager : ILevelManager
    {
        private LevelDataReader _levelDataReader;

        public LevelManager()
        {
            _levelDataReader = new LevelDataReader();
        }

        public List<LevelSummary> GetAll()
        {
            return _levelDataReader.ReadAllLevels();
        }


    }
}