using Assets.Scripts.Core;
using Assets.Scripts.Util;

namespace Assets.Scripts
{
    public class LevelStarter : UnityBehavior
    {
        protected override void Start()
        {
            GameManager.Instance.StartNewLevel();
        }
    }
}
