using System;
using System.Collections.Generic;
using Assets.Scripts.Balls;

namespace Assets.Scripts.Core.Events
{
    public class OrphanedBallsEventArgs : EventArgs
    {
        public List<IBallController> OrphanedBalls { get; private set; }

        public OrphanedBallsEventArgs(List<IBallController> orphanedBalls)
        {
            OrphanedBalls = orphanedBalls;
        }
    }
}