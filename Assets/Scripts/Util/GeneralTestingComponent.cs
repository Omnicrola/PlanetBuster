using Assets.Scripts.Balls;
using Assets.Scripts.Balls.Launcher;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Util
{
    [ExecuteInEditMode]
    public class GeneralTestingComponent : UnityBehavior
    {
        private GridPosition _gridPosition;

        public GridPosition GridPosition
        {
            get { return _gridPosition; }
            set
            {
                _gridPosition = value;
                transform.position = value.ToWorldPosition();
            }
        }

        protected override void Start()
        {
        }


        protected override void Update()
        {
        }
    }
}