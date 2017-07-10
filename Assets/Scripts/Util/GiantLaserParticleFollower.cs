using Assets.Scripts.Effects;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Util
{
    public class GiantLaserParticleFollower : UnityBehavior
    {
        public GameObject Target;
        public GiantLaserButtonEffectController EffectController;
        public bool TargetIsInUi = false;
        public Camera MainCamera;
        public bool FollowRotation = true;
        public bool FollowPosition = true;

        private ParticleSystem _particleSystem;

        protected override void Start()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        protected override void Update()
        {
            if (EffectController.IsActive)
            {
                _particleSystem.Play();
            }
            else
            {
                _particleSystem.Stop();
            }
            if (FollowRotation)
            {
                gameObject.transform.rotation = Target.transform.rotation;
            }
            if (FollowPosition)
            {
                if (TargetIsInUi)
                {
                    var uiPosition = Target.transform.position;
                    var newPosition = MainCamera.ScreenToWorldPoint(uiPosition);
                    gameObject.transform.position = new Vector3(newPosition.x, newPosition.y, 0);
                }
                else
                {
                    gameObject.transform.position = Target.transform.position;
                }

            }

        }
    }
}