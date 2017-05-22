using System;

namespace Assets.Scripts.Balls
{
    public class BallCollisionEventArgs : EventArgs
    {
        public BallController OtherObject { get; set; }
        public BallController ThisObject { get; set; }

        public BallCollisionEventArgs(BallController otherObject, BallController thisObject)
        {
            OtherObject = otherObject;
            ThisObject = thisObject;
        }
    }
}