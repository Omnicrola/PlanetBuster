using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Assets.Scripts.Core
{
    public class UniversalEventBus : IGameEventBus
    {
        private readonly Dictionary<Type, List<SubscriberInvoker>> _eventHandlers =
            new Dictionary<Type, List<SubscriberInvoker>>();

        public void Broadcast(EventArgs eventArgs)
        {
            var eventType = eventArgs.GetType();
            if (_eventHandlers.ContainsKey(eventType))
            {
                var subscribers = _eventHandlers[eventType];
                foreach (var subscriber in subscribers)
                {
                    subscriber.Invoke(eventArgs);
                }
            }
            else
            {
                Console.WriteLine("There are no subscribers for event of type : " + eventType.Name);
            }
        }

        public void Subscribe<T>(Action<T> eventHandlerOne) where T : EventArgs
        {
            if (!_eventHandlers.ContainsKey(typeof(T)))
            {
                _eventHandlers[typeof(T)] = new List<SubscriberInvoker>();
            }
            _eventHandlers[typeof(T)].Add(new SubscriberInvoker(eventHandlerOne.Target, eventHandlerOne.Method));
        }

        public void Unsubscribe<T>(Action<T> eventHandlerOne) where T : EventArgs
        {

            _eventHandlers[typeof(T)].RemoveAll(si => si.Match(eventHandlerOne.Target, eventHandlerOne.Method));
        }

        private class SubscriberInvoker
        {
            private readonly object _target;
            private readonly MethodInfo _method;

            public SubscriberInvoker(object target, MethodInfo method)
            {
                _target = target;
                _method = method;
            }

            public bool Match(object target, MethodInfo method)
            {
                return method == _method && target == _target;
            }

            public void Invoke(object eventArgs)
            {
                _method.Invoke(_target, new object[] { eventArgs });
            }
        }
    }
}