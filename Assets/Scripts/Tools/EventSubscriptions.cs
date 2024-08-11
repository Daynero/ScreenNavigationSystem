using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tools
{
    public class EventSubscriptions
    {
        private readonly List<(Button button, UnityAction action)> _buttonSubscriptions = new();
        private readonly Dictionary<object, List<Delegate>> _subscriptions = new();

        public void AddSubscription<T>(ref EventHandler<T> eventHandler, EventHandler<T> action)
            where T : EventArgs
        {
            if (!_subscriptions.ContainsKey(eventHandler))
            {
                _subscriptions[eventHandler] = new List<Delegate>();
            }

            eventHandler += action;
            _subscriptions[eventHandler].Add(action);
        }

        public void AddSubscription(Button button, UnityAction action)
        {
            _buttonSubscriptions.Add((button, action));
            button.onClick.AddListener(action);
        }

        public void ClearSubscriptions()
        {
            ClearButtonSubscriptions();
            ClearActionSubscriptions();
        }

        private void ClearButtonSubscriptions()
        {
            foreach (var (button, _) in _buttonSubscriptions)
            {
                button.onClick.RemoveAllListeners();
            }
            _buttonSubscriptions.Clear();
        }
        
        private void ClearActionSubscriptions()
        {
            foreach (var kvp in _subscriptions)
            {
                foreach (var action in kvp.Value)
                {
                    var eventHandler = kvp.Key as EventHandler;
                    eventHandler -= action as EventHandler;
                }
            }

            _subscriptions.Clear();
        }
    }
}