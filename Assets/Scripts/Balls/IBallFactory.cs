using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Balls
{
    public interface IBallFactory
    {
        Vector3 GetWorldPositionFromGrid(GridPosition gridPosition);
        GameObject GenerateBall(BallType type);
        void Recycle(GameObject gameObject);
        GridPosition GetGridPositionFromWorldPosition(Vector2 worldPosition);
    }
}