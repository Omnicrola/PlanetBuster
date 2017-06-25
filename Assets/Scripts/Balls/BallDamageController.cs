using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Balls
{
    public class BallDamageController : UnityBehavior
    {
        public Sprite DamageSprite1;
        public Sprite DamageSprite2;
        public Sprite DamageSprite3;

        public float Threshold1 = 0.25f;
        public float Threshold2 = 0.50f;
        public float Threshold3 = 0.75f;

        private SpriteRenderer _spriteRenderer;

        public float PercentDamaged { get; set; }

        protected override void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected override void Update()
        {
            if (PercentDamaged < Threshold1)
            {
                _spriteRenderer.sprite = null;
            }
            else if (PercentDamaged >= Threshold1 && PercentDamaged < Threshold2)
            {
                _spriteRenderer.sprite = DamageSprite1;
            }
            else if (PercentDamaged >= Threshold2 && PercentDamaged < Threshold3)
            {
                _spriteRenderer.sprite = DamageSprite2;
            }
            else if (PercentDamaged >= Threshold3)
            {
                _spriteRenderer.sprite = DamageSprite3;
            }
        }
    }
}