using System;
using UnityEngine;

namespace Assets.Scripts
{
    public interface IGameManager
    {
        void WhenTargetIsActive(GameObject gameObject, Action action);
    }
}