using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Balls
{
    public interface IBallFactory
    {
        IBallController GenerateBall(GridPosition gridPosition);
        IBallController GenerateBall(BallLevelData ballData);
        Vector3 GetWorldPositionFromGrid(GridPosition gridPosition);
        GameObject GenerateBall(int type);
        void Recycle(GameObject gameObject);
        Sprite GetBallSpriteOfType(int type);
        GridPosition GetGridPositionFromWorldPosition(Vector2 worldPosition);
    }
}