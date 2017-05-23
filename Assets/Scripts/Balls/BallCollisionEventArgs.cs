using System;

namespace Assets.Scripts.Balls
{
    public class BallCollisionEventArgs : EventArgs
    {
        public BallController IncomingBall { get; private set; }
        public BallController BallInGrid { get; private set; }
        public float AngleOfImpact { get; private set; }

        public BallCollisionEventArgs(BallController incomingBall, BallController ballInGrid, float angleOfImpact)
        {
            IncomingBall = incomingBall;
            BallInGrid = ballInGrid;
            AngleOfImpact = angleOfImpact;
        }
    }
}