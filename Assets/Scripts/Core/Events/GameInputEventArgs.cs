using UnityEngine;

namespace Assets.Scripts.Core.Events
{
    public class GameInputEventArgs : IGameEvent
    {
        public InputEventType EventType { get; private set; }
        public Vector3 MousePosition { get; private set; }

        public GameInputEventArgs(InputEventType eventType, Vector3 mousePosition)
        {
            EventType = eventType;
            MousePosition = mousePosition;
        }
    }

    public enum InputEventType
    {
        Press, Release
    }
}