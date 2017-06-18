using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Ui
{
    public class MatchToUiObject : UnityBehavior
    {
        public Camera camera;
        public GameObject TargetUiObject;

        protected override void Start()
        {
        }

        protected override void Update()
        {
            transform.position = camera.ScreenToWorldPoint(TargetUiObject.transform.position);
        }
    }
}