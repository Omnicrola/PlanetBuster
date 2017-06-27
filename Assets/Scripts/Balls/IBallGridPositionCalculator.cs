using Assets.Scripts.Models;

namespace Assets.Scripts.Balls
{
    public interface IBallGridPositionCalculator
    {
        GridPosition FindGridPosition(IBallController ballInGrid, BallMagnitude magnitude, float angleOfImpact);
    }
}