using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Ui;
using Assets.Scripts.Util;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Balls.Launcher
{
    public class GiantLaserBeamController : UnityBehavior
    {
        public GameObject BeamAnchor;
        public GameObject BeamSprite;
        public float ScaleAdjustment = 1f;

        public GameObject LaserImpactFx;
        public GameObject LaserImpactSphere;
        public float Radius = 1.0f;
        public float DamagePerSecond = 1.0f;
        public LayerMask LayerMask;


        private bool _isPoweredUp = true;

        private Random _random = new Random();

        protected override void Start()
        {
        }

        public void PowerUp()
        {
            SetBeamWidth(0);
            AnimateBeamWidth(0, Radius, "SetIsPoweredUp", .125f);
        }

        public void PowerDown()
        {
            AnimateBeamWidth(Radius, 0, "SetIsPoweredDown", .25f);
        }

        private void AnimateBeamWidth(float startWidth, float endWidth, string onComplete, float time)
        {
            Hashtable positionHash = iTween.Hash(
                "from", startWidth,
                "to", endWidth,
                "time", time,
                "easetype", iTween.EaseType.easeInQuart,
                "onupdate", "SetBeamWidth",
                "oncomplete", onComplete);
            iTween.ValueTo(gameObject, positionHash);
        }


        private void SetBeamWidth(float newWidth)
        {
            BeamSprite.transform.localScale = new Vector3(newWidth, 1, 1);
            float percentageOfmaxWidth = newWidth / Radius;
            BeamSprite.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, percentageOfmaxWidth);
        }

        private void SetIsPoweredUp()
        {
            _isPoweredUp = true;
        }

        private void SetIsPoweredDown()
        {
            _isPoweredUp = true;
            gameObject.SetActive(false);
        }

        protected override void Update()
        {
            if (_isPoweredUp)
            {
                CircleCastForTarget();
            }
        }

        private void CircleCastForTarget()
        {
            var distance = 100;
            var raycastHit2D = Physics2D.CircleCast(gameObject.transform.position, Radius, gameObject.transform.up, distance, LayerMask.value);
            if (raycastHit2D.collider != null)
            {
                DealDamageToTargetBall(raycastHit2D);
                AdjustImpactFx(raycastHit2D);
            }
            else
            {
                BeamAnchor.transform.localScale = new Vector3(1, 20, 1);
                LaserImpactFx.transform.position = new Vector3(0, 20, 0);
            }
        }

        private void AdjustImpactFx(RaycastHit2D raycastHit2D)
        {
            var verticalScale = raycastHit2D.distance + 1;

            BeamAnchor.transform.localScale = new Vector3(1, verticalScale * ScaleAdjustment, 1);
            var impactPosition = new Vector3(raycastHit2D.centroid.x, raycastHit2D.point.y);
            LaserImpactFx.transform.localPosition = new Vector3(0, verticalScale * .25f + .25f);
            var angleBetween = Vector3.Angle(impactPosition, raycastHit2D.transform.position);
            LaserImpactFx.transform.rotation = Quaternion.AngleAxis(angleBetween, Vector3.forward);

            float scale = (float)(_random.NextDouble() * 0.1 + .21);
            LaserImpactSphere.transform.localScale = new Vector3(scale, scale, scale);
        }

        private void DealDamageToTargetBall(RaycastHit2D raycastHit2D)
        {
            var ballController = raycastHit2D.collider.gameObject.GetComponent<IBallController>();
            if (ballController != null)
            {
                ballController.Hitpoints -= DamagePerSecond * Time.deltaTime;
                if (ballController.Hitpoints <= 0)
                {
                    GameManager.Instance.EventBus.Broadcast(
                        new BallDestroyByGiantLaserEventArgs(ballController));
                }
            }
        }
    }
}