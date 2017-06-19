using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Balls
{
    public interface IBallFactory
    {
        IBallController GenerateBall(int gridX, int gridY);
        IBallController GenerateBall(BallLevelData ballData);
        Vector3 GetGridPosition(int gridX, int gridY);
        GameObject GenerateBall(int type);
        void Recycle(GameObject gameObject);
        Sprite GetBallSpriteOfType(int type);
    }
}