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

        public List<LevelSummary> GetAll()
        {
            return new List<LevelSummary>
            {
                new LevelSummary(1,"1")
                {
                    IsLocked = false,
                    BallData = BuildLargeLevel()
                },
                new LevelSummary(2,"2")
                {
                    IsLocked = false,
                    BallData = BuildLevel(2, 0.9f, 0.25f)
                },
                new LevelSummary(3,"3")
                {
                    IsLocked = false,
                    BallData = BuildLevel(3, 0.9f, 0.25f)
                },
                new LevelSummary(4,"4")
                {
                    IsLocked = false,
                    BallData = BuildLevel(4, 0.9f, 0.25f)
                },
                new LevelSummary(5,"5")
                {
                    IsLocked = false,
                    BallData = BuildLevel(5, 0.9f, 0.25f)
                },
                new LevelSummary(6,"6")
                {
                    IsLocked = false,
                    BallData = BuildLevel(6, 0.9f, 0.25f)
                },
                new LevelSummary(7,"7")
                {
                    IsLocked = false,
                    BallData = BuildLevel(7, 0.9f, 0.25f)
                },
            };
        }

        private List<BallLevelData> BuildLargeLevel()
        {
            var ballData = new List<BallLevelData>();
            var random = new Random(234);

            for (int x = 0; x < 11; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    if ((x < 5 || x > 6) || (y < 3 || y > 4))
                    {
                        ballData.Add(new BallLevelData
                        {
                            XPos = x,
                            YPos = y,
                            HasPowerGem = true,
                            BallType = (BallType)random.Next(4),
                            Magnitude = BallMagnitude.Standard
                        });
                    }
                }
            }
            ballData.Add(new BallLevelData
            {
                XPos = 5,
                YPos = 3,
                BallType = BallType.Blue,
                Magnitude = BallMagnitude.Medium
            });
            return ballData;
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
                        BallType ballType = (BallType)random.Next(4);
                        var hasPowerGem = random.NextDouble() < powerGemDensity;
                        ballLevelData.Add(new BallLevelData
                        {
                            XPos = x,
                            YPos = y,
                            BallType = ballType,
                            HasPowerGem = hasPowerGem,
                            Magnitude = BallMagnitude.Standard
                        });
                    }
                }
            }
            return ballLevelData;
        }
    }
}