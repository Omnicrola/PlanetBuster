using System;
using Assets.Scripts.Balls;

namespace Assets.Scripts.Core.Events
{
    public class BallGridMatchArgs : IGameEvent
    {
        public IBallPath BallPath { get; private set; }

        public BallGridMatchArgs(IBallPath ballPath)
        {
            BallPath = ballPath;
        }
    }
}