using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Balls
{
    public class NextBallController : UnityBehavior
    {
        public float ScaleTime = 0.5f;
        public float Size = 1f;

        private SpriteRenderer _spriteRenderer;

        protected override void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetNextBall(Sprite ballSprite)
        {
            _spriteRenderer.sprite = ballSprite;
            ScaleTime = 0.5f;
            gameObject.transform.localScale = Vector3.zero;
            iTween.ScaleTo(gameObject, new Vector3(Size, Size, Size), ScaleTime);
        }
    }
}