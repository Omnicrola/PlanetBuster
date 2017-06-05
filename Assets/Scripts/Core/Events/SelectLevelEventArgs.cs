using Assets.Scripts.Models;

namespace Assets.Scripts.Core.Events
{
    public class SelectLevelEventArgs : IGameEvent
    {
        public LevelSummary LevelSummary { get; private set; }

        public SelectLevelEventArgs(LevelSummary model)
        {
            LevelSummary = model;
        }
    }
}