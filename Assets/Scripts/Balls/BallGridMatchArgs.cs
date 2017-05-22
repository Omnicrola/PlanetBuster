using System;

namespace Assets.Scripts.Balls
{
    public class BallGridMatchArgs : EventArgs
    {
        public IBallPath BallPath { get; private set; }

        public BallGridMatchArgs(IBallPath ballPath)
        {
            BallPath = ballPath;
        }
    }
}