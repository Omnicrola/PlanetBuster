﻿using System;

namespace Assets.Scripts.Balls
{
    public class BallCollisionEventArgs : EventArgs
    {
        public IBallController IncomingBall { get; private set; }
        public IBallController BallInGrid { get; private set; }
        public float AngleOfImpact { get; private set; }

        public BallCollisionEventArgs(IBallController incomingBall, IBallController ballInGrid, float angleOfImpact)
        {
            IncomingBall = incomingBall;
            BallInGrid = ballInGrid;
            AngleOfImpact = angleOfImpact;
        }
    }
}