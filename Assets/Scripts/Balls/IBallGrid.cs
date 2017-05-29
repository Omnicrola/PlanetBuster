using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Balls
{
    public interface IBallGrid
    {
        int Size { get; }
        int[] TypesLeftActive { get; }
        int ActiveBalls { get; }
        void Append(IBallController newBall, int gridX, int gridY);
        void Initialize(List<IBallController> newBalls);
        void Clear();
        void Remove(GameObject gameObject);
    }
}