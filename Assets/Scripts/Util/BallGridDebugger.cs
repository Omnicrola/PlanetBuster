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
                        _currentController = newController;
                    }
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("ball at : " + _currentController.GridPosition);
            }
        }
    }
}