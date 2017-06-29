using System;
using Assets.Scripts.Balls;

namespace Assets.Scripts.Core.Events
{
    public class BallDestroyByGiantLaserEventArgs : IGameEvent
    {
        public IBallController BallController { get; private set; }

        public BallDestroyByGiantLaserEventArgs(IBallController ballController)
        {
            BallController = ballController;
        }
    }
}