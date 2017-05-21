using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts
{
    public class BallController : DirtyBehavior<IBall>
    {
        private SpriteRenderer _spriteRenderer;

        protected override void Start()
        {
            base.Start();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected override void DirtyUpdate()
        {
            if (_spriteRenderer != null && Model != null)
            {
                _spriteRenderer.sprite = Resources.Load<Sprite>(Model.IconName);
            }
        }
    }

    public interface IBall
    {
        string IconName { get; }
    }
}
