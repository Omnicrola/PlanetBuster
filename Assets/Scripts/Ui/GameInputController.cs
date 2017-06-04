using System.Linq;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Ui
{
    public class GameInputController : UnityBehavior
    {
        private Camera _camera;
        private bool _isPressed;

        protected override void Start()
        {
            _camera = GetComponent<Camera>();
        }

        protected override void Update()
        {
            var gameEventBus = GameManager.Instance.EventBus;
            if (IsPressed())
            {
                gameEventBus.Broadcast(new GameInputEventArgs(InputEventType.Press, Input.mousePosition));
            }
            if (IsReleased())
            {
                gameEventBus.Broadcast(new GameInputEventArgs(InputEventType.Release, Input.mousePosition));
            }
        }

        private bool IsReleased()
        {
            var wasReleased = TouchWasReleased() || Input.GetMouseButtonUp(0);
            bool isNotOverUiObject = !EventSystem.current.IsPointerOverGameObject();
            return wasReleased && isNotOverUiObject;
        }

        private bool IsPressed()
        {
            var isPressed = AreAnyTouches() || Input.GetMouseButtonDown(0);
            bool isNotOverUiObject = !EventSystem.current.IsPointerOverGameObject();
            return isPressed && isNotOverUiObject;
        }

        private bool AreAnyTouches()
        {
            return Input.touches.Any(t => t.phase != TouchPhase.Canceled && t.phase != TouchPhase.Ended);
        }


        private bool TouchWasReleased()
        {
            return Input.touches.Any(t => t.phase == TouchPhase.Ended);
        }
    }
}