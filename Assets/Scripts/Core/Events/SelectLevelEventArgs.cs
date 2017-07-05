using Assets.Scripts.Core.Levels;
using Assets.Scripts.Models;

namespace Assets.Scripts.Core.Events
{
    public class SelectLevelEventArgs : IGameEvent
    {
        public ILevelDataController Level { get; private set; }

        public SelectLevelEventArgs(ILevelDataController model)
        {
            Level = model;
        }
    }
}