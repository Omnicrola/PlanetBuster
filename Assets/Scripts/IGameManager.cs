using System;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts
{
    public interface IGameManager
    {
        void WhenTargetIsActive(GameObject gameObject, Action action);
        GameObject GenerateBall(int gridX, int gridY);
    }
}