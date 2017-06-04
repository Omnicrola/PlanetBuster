﻿using System;
using Assets.Scripts.Balls;

namespace Assets.Scripts.Core.Events
{
    public class BallDestroyEventArgs : IGameEvent
    {
        public IBallController BallController { get; private set; }

        public BallDestroyEventArgs(IBallController ballController)
        {
            BallController = ballController;
        }
    }
}