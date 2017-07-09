using Assets.Scripts.Balls.Launcher;
using Assets.Scripts.Effects;
using Assets.Scripts.Ui;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Util
{
    public class PowerbarCollisionTargetController : UnityBehavior, IParticleTargetProvider
    {
        public Camera Camera;
        public ProgressBarController ProgressBar;
        public RectTransform TargetRect;
        public GiantLaserController GiantLaserController;

        protected override void Start()
        {
        }

        protected override void Update()
        {
            var position = TargetRect.transform.position;
            var startingX = position.x - (TargetRect.rect.width / 2);
            var startingY = position.y + (TargetRect.rect.height / 2);

            var percentFilled = ProgressBar.Level;
            var adjustedWidth = startingX + (TargetRect.rect.width * percentFilled);

            var offsetPosition = new Vector2(adjustedWidth, startingY);
            var targetPosition = Camera.ScreenToWorldPoint(offsetPosition);
            gameObject.transform.position = new Vector2(targetPosition.x, targetPosition.y);
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == Tags.PowerParticles)
            {
                GiantLaserController.AddPowerCharge();
            }
        }

        public Vector3 TargetPosition { get { return transform.position; } }
    }
}