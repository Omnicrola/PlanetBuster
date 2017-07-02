using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Balls
{
    public interface IBallFactory
    {
        IBallController GenerateBall(GridPosition gridPosition);
        IBallController GenerateBall(BallLevelData ballData);
        Vector3 GetWorldPositionFromGrid(GridPosition gridPosition);
        GameObject GenerateBall(BallType type);
        void Recycle(GameObject gameObject);
        GridPosition GetGridPositionFromWorldPosition(Vector2 worldPosition);
    }
}