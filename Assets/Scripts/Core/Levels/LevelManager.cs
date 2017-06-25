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
                new LevelSummary("1")
                {
                    IsLocked = false,
                    BallData = BuildLargeLevel()
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
                    BallData = BuildLevel(5, 0.9f, 0.25f)
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

        private List<BallLevelData> BuildLargeLevel()
        {
            var ballData = new List<BallLevelData>();
            for (int x = 0; x < 11; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    if ((x < 5 || x > 6) || (y < 2 || y > 3))
                    {
                        ballData.Add(new BallLevelData
                        {
                            XPos = x,
                            YPos = y,
                            HasPowerGem = true,
                            BallType = 1,
                            Magnitude = BallMagnitude.Standard
                        });
                    }
                }
            }
            ballData.Add(new BallLevelData
            {
                XPos = 5,
                YPos = 2,
                BallType = 3,
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
                        var ballType = random.Next(4);
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