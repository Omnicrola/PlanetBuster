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
        public GameObject ValidInputArea;

        private Camera _camera;
        private bool _isPressed;

        protected override void Start()
        {
            _camera = GetComponent<Camera>();
        }

        protected override void Update()
        {
            if (!GameManager.Instance.Pause)
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
        }

        private bool IsReleased()
        {
            var isMouseValid = IsMousePositionValid() && Input.GetMouseButtonUp(0);
            var wasReleased = WasAnyValidTouchReleased() || isMouseValid;
            return wasReleased;
        }

        private bool IsPressed()
        {
            var isMouseValid = IsMousePositionValid() && Input.GetMouseButtonDown(0);
            var isPressed = AreAnyTouchesValid() || isMouseValid;

            return isPressed;
        }

        private bool IsMousePositionValid()
        {
            var inputRectangle = GetInputRectangle();
            return inputRectangle.Contains(Input.mousePosition);
        }


        private bool AreAnyTouchesValid()
        {
            var validInputRectangle = GetInputRectangle();
            var validTouches = Input.touches
                .Where(t => t.phase != TouchPhase.Canceled)
                .Where(t => t.phase != TouchPhase.Ended)
                .Any(t => validInputRectangle.Contains(t.position));
            return validTouches;
        }

        private Rect GetInputRectangle()
        {
            var inputRectangle = ValidInputArea.GetComponent<RectTransform>().rect;

            Vector2 position = ValidInputArea.transform.position;
            var offsetPosition = inputRectangle.position + position;
            return new Rect(offsetPosition, new Vector2(inputRectangle.width, inputRectangle.height));
        }


        private bool WasAnyValidTouchReleased()
        {
            var inputRectangle = GetInputRectangle();
            return Input.touches
                .Where(t => t.phase == TouchPhase.Ended)
                .Any(t => inputRectangle.Contains(t.position));
        }
    }
}