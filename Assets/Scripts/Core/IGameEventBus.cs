using System;
using Assets.Scripts.Balls.Launcher;
using Assets.Scripts.Core.Events;

namespace Assets.Scripts.Core
{
    public interface IGameEventBus
    {
        void Broadcast(EventArgs eventArgs);
        void Subscribe<T>(Action<T> eventHandlerOne) where T : EventArgs;
        void Unsubscribe<T>(Action<T> eventHandlerOne) where T : EventArgs;
    }
}