using System;

namespace Assets.Scripts.Core.Events
{
    public class GameOverEventArgs : IGameEvent
    {
        public GameOverCondition Condition { get; private set; }

        public GameOverEventArgs(GameOverCondition condition)
        {
            Condition = condition;
        }
    }

    public enum GameOverCondition
    {
        Win, LossByTime, LossByDropHeight
    }
}