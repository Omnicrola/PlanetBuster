using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    public class EffectsManager : UnityBehavior
    {
        private SimpleObjectPool _simpleObjectPool;

        void Start()
        {
            GameManager.Instance.EventBus.BallMatchFound += OnMatchFound;
            _simpleObjectPool = GetComponent<SimpleObjectPool>();
        }

        private void OnMatchFound(object sender, BallGridMatchArgs e)
        {
            foreach (var ball in e.BallPath)
            {

                var ballPosition = ball.Position;
                var newExplosion = _simpleObjectPool.GetObjectFromPool();
                newExplosion.transform.SetParent(transform);
                newExplosion.transform.position = ballPosition;
                newExplosion.GetComponent<BallDestroyEffect>().PlayEffect();
            }
        }

        protected override void OnDestroy()
        {
            GameManager.Instance.EventBus.BallMatchFound -= OnMatchFound;
        }
    }
}
