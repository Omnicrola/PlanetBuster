using System;

namespace Assets.Scripts.Core.Events
{
    public class ScoreChangedEventArgs : EventArgs
    {
        public ScoreChangedEventArgs(int newScore)
        {
            NewScore = newScore;
        }

        public int NewScore { get; private set; }
    }
}