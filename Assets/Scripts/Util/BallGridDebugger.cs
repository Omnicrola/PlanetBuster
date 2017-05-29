using Assets.Scripts.Balls;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Util
{
    public class BallGridDebugger : UnityBehavior
    {
        private Camera _camera;

        private IBallController _currentController;

        protected override void Start()
        {
            _camera = GetComponent<Camera>();
        }

        protected override void Update()
        {
            var raycastHit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (raycastHit.collider != null)
            {
                var newController = raycastHit.transform.gameObject.GetComponent<IBallController>();
                if (newController != null)
                {
                    if (_currentController != newController)
                    {
                        if (_currentController != null) RemoveHighlights(_currentController.Model);
                        SetHighlights(newController);
                        _currentController = newController;
                    }


                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                var hasModel = _currentController.Model != null;
                var gridLocation = hasModel ? _currentController.Model.GridX + ", " + _currentController.Model.GridY : "(n/a)";
                Debug.Log("ball at : " + gridLocation + " has model : " + hasModel);
            }

        }

        private void SetHighlights(IBallController controller)
        {
            if (controller.Model == null)
            {
                SetObjectColor(controller.gameObject, Color.red);
            }
            else
            {
                var model = controller.Model;
                if (model.North == null && model.South == null && model.East == null && model.West == null)
                {
                    SetObjectColor(controller.gameObject, Color.yellow);
                }
                SetHighlight(model.North);
                SetHighlight(model.South);
                SetHighlight(model.East);
                SetHighlight(model.West);
            }
        }

        private void RemoveHighlights(BallModel model)
        {
            if (model != null)
            {
                RemoveHightlight(model.North);
                RemoveHightlight(model.South);
                RemoveHightlight(model.East);
                RemoveHightlight(model.West);
            }
        }

        private void SetHighlight(IBallController ballToHightlight)
        {
            if (ballToHightlight != null)
            {
                SetObjectColor(ballToHightlight.gameObject, Color.green);
            }
        }

        private void RemoveHightlight(IBallController ball)
        {
            if (ball != null)
            {
                SetObjectColor(ball.gameObject, Color.white);
            }
        }

        private void SetObjectColor(GameObject objToSet, Color color)
        {
            objToSet.GetComponent<Renderer>().material.color = color;
        }
    }
}