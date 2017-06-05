using System.Collections.Generic;
using Assets.Scripts.Models;

namespace Assets.Scripts.Core.Levels
{
    public class LevelManager : ILevelManager
    {
        public LevelManager()
        {
        }

        public List<LevelSummary> GetAll()
        {
            return new List<LevelSummary>
            {
                new LevelSummary("1")
                {
                    IsLocked = false,
                },
                new LevelSummary("2")
                {
                    IsLocked = false,
                },
                new LevelSummary("3")
                {
                    IsLocked = false,
                },
                new LevelSummary("4")
                {
                    IsLocked = false,
                },
                new LevelSummary("5")
                {
                    IsLocked = false,
                },
                new LevelSummary("6")
                {
                    IsLocked = false,
                },
                new LevelSummary("7")
                {
                    IsLocked = false,
                },
            };
        }
    }
}