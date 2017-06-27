using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Balls;
using Assets.Scripts.Models;

namespace Assets.Scripts.Extensions
{
    public static class PlanetBusterExtensions
    {
        public static float GetScale(this BallMagnitude magnitude)
        {
            switch (magnitude)
            {
                case BallMagnitude.Standard:
                    return 1f;
                    break;
                case BallMagnitude.Medium:
                    return 2f;
                    break;
                case BallMagnitude.Large:
                    return 3f;
                    break;
                case BallMagnitude.Huge:
                    return 4f;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("magnitude", magnitude, null);
            }
        }

        public static bool None<T>(this IEnumerable<T> list)
        {
            return !list.Any();
        }

        public static float GetHitpoints(this BallMagnitude magnitude)
        {
            switch (magnitude)
            {
                case BallMagnitude.Standard:
                    return GameConstants.BallHitpoints.Standard;
                    break;
                case BallMagnitude.Medium:
                    return GameConstants.BallHitpoints.Medium;
                    break;
                case BallMagnitude.Large:
                    return GameConstants.BallHitpoints.Large;
                    break;
                case BallMagnitude.Huge:
                    return GameConstants.BallHitpoints.Huge;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("magnitude", magnitude, null);
            }
        }

        public static IBallController GetFromPosition(this IBallController[,] ballArray, GridPosition gridPosition)
        {
            var isInWidthBounds = gridPosition.X >= 0 && gridPosition.X < ballArray.GetLength(0);
            var isInHeightBounds = gridPosition.Y >= 0 && gridPosition.Y < ballArray.GetLength(1);
            if (isInWidthBounds && isInHeightBounds)
            {
                return ballArray[gridPosition.X, gridPosition.Y];
            }
            return null;
        }
    }
}