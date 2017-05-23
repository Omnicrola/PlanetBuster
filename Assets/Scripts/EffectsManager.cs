using Assets.Scripts.Balls;
using UnityEngine;

namespace Assets.Scripts
{
    public class EffectsManager : UnityBehavior
    {
        public GameObject ExplosionPrefab;

        void Start()
        {
            GameManager.Instance.MatchFound += OnMatchFound;
        }

        private void OnMatchFound(object sender, BallGridMatchArgs e)
        {
            foreach (var ball in e.BallPath)
            {
                var ballPosition = ball.transform.position;
                var newExplosion = Instantiate(ExplosionPrefab);
                newExplosion.transform.SetParent(transform);
                newExplosion.transform.position = ballPosition;
            }
        }

        protected override void OnDestroy()
        {
            GameManager.Instance.MatchFound -= OnMatchFound;
        }
    }
}
