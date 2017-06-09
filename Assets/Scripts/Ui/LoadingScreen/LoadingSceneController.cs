using Assets.Scripts.Core;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Ui.LoadingScreen
{
    public class LoadingSceneController : UnityBehavior
    {
        public float TransitionDelay = 1f;
        private float _startedWaiting;
        private bool _isDone = false;

        protected override void Start()
        {
            _startedWaiting = Time.time;
        }

        protected override void Update()
        {
            if (Time.time - _startedWaiting >= TransitionDelay)
            {
                var nextScene = GameManager.Instance.TransitionToScene;
                if (nextScene != null)
                {
                    GameManager.Instance.TransitionToScene = null;
                    SceneManager.LoadScene(nextScene);
                }
                else
                {
                    if (!_isDone)
                    {
                        _isDone = true;
                        Logging.Instance.Log(LogLevel.Warning, "Loading scene has no target scene to transition to!");
                    }
                }
            }
        }
    }
}