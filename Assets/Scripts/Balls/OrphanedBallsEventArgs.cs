using System;
using System.Collections.Generic;

namespace Assets.Scripts.Balls
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