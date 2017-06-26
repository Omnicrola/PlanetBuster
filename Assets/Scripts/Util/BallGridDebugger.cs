using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Balls;
using Assets.Scripts.Core;
using Assets.Scripts.Models;
using UnityEngine;
using Assets.Scripts.Extensions;
using Assets.Scripts.Ui;

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
                        if (_currentController != null) RemoveHighlights(_currentController);
                        SetHighlights(newController);
                        SendDebugText(newController);
                        _currentController = newController;
                    }
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                var hasModel = _currentController.Model != null;
                var gridLocation = hasModel
                    ? _currentController.Model.GridX + ", " + _currentController.Model.GridY
                    : "(n/a)";
                Debug.Log("ball at : " + gridLocation + " has model : " + hasModel);
            }
        }

        private void SendDebugText(IBallController newController)
        {
            var north = newController.North.Any() ? newController.North.Count.ToString() : "None";
            var south = newController.South.Any() ? newController.South.Count.ToString() : "None";
            var east = newController.East.Any() ? newController.East.Count.ToString() : "None";
            var west = newController.West.Any() ? newController.West.Count.ToString() : "None";
            //var north = newController.North.Any() ? newController.North.Select(c => "(" + c.Model.GridX + "," + c.Model.GridY + ")").Aggregate((s, e) => " ") : "None";
            //            var south = newController.South.Any() ? newController.South.Select(c => "(" + c.Model.GridX + "," + c.Model.GridY + ")").Aggregate((s, e) => " ") : "None";
            //            var east = newController.East.Any() ? newController.East.Select(c => "(" + c.Model.GridX + "," + c.Model.GridY + ")").Aggregate((s, e) => " ") : "None";
            //            var west = newController.West.Any() ? newController.West.Select(c => "(" + c.Model.GridX + "," + c.Model.GridY + ")").Aggregate((s, e) => " ") : "None";

            var text = " N:" + north + "\nS:" + south + "\nE:" + east + "\nW:" + west;
            GameManager.Instance.EventBus.Broadcast(new DebugEventArgs("Neighbors", text));
        }

        private void SetHighlights(IBallController controller)
        {
            if (controller.Model == null)
            {
                SetObjectColor(controller.gameObject, Color.red);
            }
            else
            {
                if (controller.AllNeighbors.None())
                {
                    SetObjectColor(controller.gameObject, Color.yellow);
                }
                SetHighlight(controller.North);
                SetHighlight(controller.South);
                SetHighlight(controller.East);
                SetHighlight(controller.West);
            }
        }

        private void RemoveHighlights(IBallController ballController)
        {
            RemoveHightlight(ballController.North);
            RemoveHightlight(ballController.South);
            RemoveHightlight(ballController.East);
            RemoveHightlight(ballController.West);
        }

        private void SetHighlight(List<IBallController> ballsToHightlight)
        {
            foreach (var ballController in ballsToHightlight)
            {
                SetObjectColor(ballController.gameObject, Color.green);
            }
        }

        private void RemoveHightlight(List<IBallController> ballsToReset)
        {
            foreach (var ballController in ballsToReset)
            {
                SetObjectColor(ballController.gameObject, Color.white);
            }
        }

        private void SetObjectColor(GameObject objToSet, Color color)
        {
            objToSet.GetComponent<BallController>().BallSprite.GetComponent<Renderer>().material.color = color;
        }
    }
}