using System;
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
                    BallData = BuildLevel(1, 0.9f, 0.25f)
                },
                new LevelSummary("2")
                {
                    IsLocked = false,
                    BallData = BuildLevel(2, 0.9f, 0.25f)
                },
                new LevelSummary("3")
                {
                    IsLocked = false,
                    BallData = BuildLevel(3, 0.9f, 0.25f)
                },
                new LevelSummary("4")
                {
                    IsLocked = false,
                    BallData = BuildLevel(4, 0.9f, 0.25f)
                },
                new LevelSummary("5")
                {
                    IsLocked = false,
                    BallData = BuildLevel(5,0.9f, 0.25f)
                },
                new LevelSummary("6")
                {
                    IsLocked = false,
                    BallData = BuildLevel(6, 0.9f, 0.25f)
                },
                new LevelSummary("7")
                {
                    IsLocked = false,
                    BallData = BuildLevel(7, 0.9f, 0.25f)
                },
            };
        }

        private static List<BallLevelData> BuildLevel(int seed, float densityPercentage, float powerGemDensity)
        {
            var random = new Random(seed);
            var ballLevelData = new List<BallLevelData>();
            var maxWidth = 11;
            var height = 20;
            for (int x = 0; x < maxWidth; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var occupySlot = random.NextDouble() < densityPercentage;
                    if (occupySlot)
                    {
                        var ballType = random.Next(4);
                        var hasPowerGem = random.NextDouble() < powerGemDensity;
                        ballLevelData.Add(new BallLevelData(x, y, ballType, hasPowerGem));
                    }
                }
            }
            return ballLevelData;
        }
    }
}