using Assets.Scripts.Models;

namespace Assets.Scripts.Balls
{
    public interface IBallGridPositionCalculator
    {
        GridPosition FindGridPosition(BallModel ballInGrid, float angleOfImpact);
    }
}