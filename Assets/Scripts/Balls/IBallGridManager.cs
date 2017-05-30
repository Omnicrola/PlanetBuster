using UnityEngine;

namespace Assets.Scripts.Balls
{
    public interface IBallGridManager
    {
        IBallController GenerateBall();
        GameObject GenerateBall(int type);
        int GetNextBallType();
        Sprite GetBallSpriteOfType(int type);
        void StartNewLevel();
    }
}