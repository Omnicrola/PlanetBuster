using System;

namespace Assets.Scripts.Core.Events
{
    public class ScoreChangedEventArgs : IGameEvent
    {
        public ScoreChangedEventArgs(int newScore)
        {
            NewScore = newScore;
        }

        public int NewScore { get; private set; }
    }
}