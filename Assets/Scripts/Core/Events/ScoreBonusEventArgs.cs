using System;

namespace Assets.Scripts.Core.Events
{
    public class ScoreBonusEventArgs : IGameEvent
    {
        public int Size { get; private set; }
        public int BonusScore { get; private set; }

        public ScoreBonusEventArgs(int size, int bonusScore)
        {
            Size = size;
            BonusScore = bonusScore;
        }
    }
}