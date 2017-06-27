using System;
using System.Collections.Generic;
using Assets.Scripts.Balls;

namespace Assets.Scripts.Core.Events
{
    public class BallGridMatchArgs : IGameEvent
    {
        public List<IBallController> BallPath { get; private set; }

        public BallGridMatchArgs(List<IBallController> ballPath)
        {
            BallPath = ballPath;
        }
    }
}