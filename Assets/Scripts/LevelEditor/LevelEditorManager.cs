using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.LevelEditor
{
    public class LevelEditorManager : UnityBehavior
    {
        public GameObject Ceiling;
        public GameObject Camera;

        public PalleteSelection CurrentPalletSelection { get; set; }
        private EditableBallGrid _editableBallGrid;

        protected override void Start()
        {
            var simpleObjectPool = GetComponent<SimpleObjectPool>();
            _editableBallGrid = new EditableBallGrid(simpleObjectPool, Ceiling);
        }

        public void PlaceBall()
        {
            if (CurrentPalletSelection == null)
            {
                DeleteBall();
            }
            else
            {
                var gridPosition = GetCurrentGridPosition();
                _editableBallGrid.SetBall(gridPosition, CurrentPalletSelection);
            }
        }

        private Vector2 GetCurrentGridPosition()
        {
            return GetComponent<GridPositionDetector>().GetGridPosition();
        }

        public void DeleteBall()
        {
            var gridPosition = GetCurrentGridPosition();
            _editableBallGrid.Remove(gridPosition);
        }
    }
}