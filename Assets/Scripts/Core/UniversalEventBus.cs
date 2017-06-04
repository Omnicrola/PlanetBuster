using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Assets.Scripts.Core
{
    public class UniversalEventBus
    {
        private readonly Dictionary<Type, List<EventSubscriberInvoker>> _subscribers =
            new Dictionary<Type, List<EventSubscriberInvoker>>();

        public void Broadcast(object source, EventArgs eventArgs)
        {
            var eventType = eventArgs.GetType();
            if (_subscribers.ContainsKey(eventType))
            {
                var subscribers = _subscribers[eventType];
                foreach (var subscriber in subscribers)
                {
                    subscriber.Send(source, eventArgs);
                }
            }
            else
            {
                Console.WriteLine("There are no subscribers for event of type : " + eventType.Name);
            }
        }

        public void Subscribe<T>(object subscriber) where T : EventArgs
        {
            var eventType = typeof(T);
            if (!_subscribers.ContainsKey(eventType))
            {
                _subscribers[eventType] = new List<EventSubscriberInvoker>();
            }
            _subscribers[eventType].Add(new EventSubscriberInvoker(subscriber, typeof(T)));
        }

        public void Unsubscribe<T>(object subscriber) where T : EventArgs
        {
            var eventType = typeof(T);
            _subscribers[eventType].RemoveAll(s => s.Subscriber == subscriber);
        }

        private class EventSubscriberInvoker
        {
            public object Subscriber { get; private set; }
            private readonly List<MethodInfo> _handlerMethods;


            public EventSubscriberInvoker(object subscriber, Type eventType)
            {
                Subscriber = subscriber;
                _handlerMethods = FindHandlerMethods(subscriber, eventType);
                if (!_handlerMethods.Any())
                {
                    throw new ArgumentException(
                        string.Format("Class '{0}' has no methods that can accept an event of type '{1}'",
                            subscriber.GetType().Name, eventType.Name));
                }
            }

            public void Send(object source, EventArgs eventArgs)
            {
                foreach (var handlerMethod in _handlerMethods)
                {
                    handlerMethod.Invoke(Subscriber, new object[] { source, eventArgs });
                }
            }

            private static List<MethodInfo> FindHandlerMethods(object subscriber, Type eventArgType)
            {
                var allMethods = new List<MethodInfo>();
                RecursivelyGetAllMethods(allMethods, subscriber.GetType());

                return allMethods.Where(m => HasEventParameter(m, eventArgType)).ToList();
            }

            private static void RecursivelyGetAllMethods(List<MethodInfo> methods, Type type)
            {
                methods.AddRange(
                    type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public |
                                    BindingFlags.FlattenHierarchy));
                if (typeof(object) != type.BaseType)
                {
                    RecursivelyGetAllMethods(methods, type.BaseType);
                }
            }

            private static bool HasEventParameter(MethodInfo methodInfo, Type eventArgType)
            {
                var parameterInfos = methodInfo.GetParameters();
                if (parameterInfos.Length < 2)
                {
                    return false;
                }
                return parameterInfos[1].ParameterType == eventArgType;
            }
        }
    }
}