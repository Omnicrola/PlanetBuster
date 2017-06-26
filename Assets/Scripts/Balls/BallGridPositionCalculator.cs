using Assets.Scripts.Extensions;
using Assets.Scripts.Models;

namespace Assets.Scripts.Balls
{
    public class BallGridPositionCalculator : IBallGridPositionCalculator
    {
        const float NORTH_EAST = 45f;
        const float SOUTH_EAST = 135f;
        const float SOUTH_WEST = 225f;
        const float NORTH_WEST = 315f;

        public GridPosition FindGridPosition(BallModel ballInGrid, float angleOfImpact)
        {
            int offsetX = 0;
            int offsetY = 0;

            var magnitudeScale = ballInGrid.Magnitude.GetScale();

            if (angleOfImpact >= NORTH_EAST && angleOfImpact < SOUTH_EAST)
            {
                // Right side
                offsetX = (int)(1 * magnitudeScale);
            }
            else if (angleOfImpact >= SOUTH_EAST && angleOfImpact < SOUTH_WEST)
            {
                // Bottom side
                offsetY = (int)(1 * magnitudeScale);
            }
            else if (angleOfImpact >= SOUTH_WEST && angleOfImpact < NORTH_WEST)
            {
                // Left side
                offsetX = -1;
            }
            return new GridPosition(ballInGrid.GridX + offsetX, ballInGrid.GridY + offsetY);
        }
    }
}