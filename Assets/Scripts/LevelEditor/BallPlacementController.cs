using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.LevelEditor
{
    public class BallPlacementController : UnityBehavior
    {
        private bool _isDirty;
        private PalleteSelection _palleteSelection;

        public PalleteSelection PalleteSelection
        {
            get { return _palleteSelection; }
            set
            {
                _palleteSelection = value;
                _isDirty = true;
            }
        }

        protected override void Start()
        {
            _isDirty = true;
        }

        protected override void Update()
        {
            if (_isDirty && _palleteSelection != null)
            {
                _isDirty = false;
                GetComponent<SpriteRenderer>().sprite = _palleteSelection.Sprite;
            }
        }
    }
}