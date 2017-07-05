using System.Collections.Generic;
using Assets.Scripts.Models;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Core.Levels
{
    public class LevelManager : ILevelManager
    {
        public LevelManager()
        {
        }

        public List<ILevelDataController> GetAll()
        {
            return new List<ILevelDataController>
            {

            };
        }

    }
}