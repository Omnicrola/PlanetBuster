using System;
using Assets.Scripts.Models;

namespace Assets.Scripts.Extensions
{
    public static class PlanetBusterExtensions
    {
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
    }
}