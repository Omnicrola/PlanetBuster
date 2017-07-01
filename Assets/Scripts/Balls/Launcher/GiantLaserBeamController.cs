using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Ui;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Balls.Launcher
{
    public class GiantLaserBeamController : UnityBehavior
    {
        public GameObject BeamAnchor;
        public GameObject BeamSprite;
        public float Radius = 1.0f;
        public float DamagePerSecond = 1.0f;
        private bool _isPoweredUp;

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
            var raycastHit2D = Physics2D.CircleCast(gameObject.transform.position, Radius, gameObject.transform.up);
            if (raycastHit2D.collider != null)
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

                var distance = raycastHit2D.distance;
                BeamAnchor.transform.localScale = new Vector3(1, distance, 1);
            }
            else
            {
                BeamAnchor.transform.localScale = new Vector3(1, 20, 1);
            }
        }
    }
}