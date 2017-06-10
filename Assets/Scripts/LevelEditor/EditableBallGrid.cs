using System.Collections.Generic;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.LevelEditor
{
    public class EditableBallGrid
    {
        private readonly SimpleObjectPool _objectPool;
        private readonly GameObject _ceiling;
        private readonly Dictionary<Vector2, GameObject> _activeBalls;

        public EditableBallGrid(SimpleObjectPool objectPool, GameObject ceiling)
        {
            _objectPool = objectPool;
            _ceiling = ceiling;
            _activeBalls = new Dictionary<Vector2, GameObject>();
        }

        public void SetBall(Vector2 gridPosition, PalleteSelection currentPalletSelection)
        {
            GameObject ballToSet = null;
            if (_activeBalls.ContainsKey(gridPosition))
            {
                ballToSet = _activeBalls[gridPosition];
            }
            else
            {
                ballToSet = _objectPool.GetObjectFromPool();
                _activeBalls[gridPosition] = ballToSet;
            }
            ballToSet.transform.SetParent(_ceiling.transform, false);
            var ballPlacementController = ballToSet.GetComponent<BallPlacementController>();
            ballPlacementController.PalleteSelection = currentPalletSelection;
            ballToSet.transform.position = gridPosition;

        }

        public void Remove(Vector2 gridPosition)
        {
            if (_activeBalls.ContainsKey(gridPosition))
            {
                _objectPool.ReturnObjectToPool(_activeBalls[gridPosition]);
                _activeBalls.Remove(gridPosition);
            }
        }
    }
}