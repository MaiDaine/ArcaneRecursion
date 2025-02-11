﻿using System.Collections.Generic;
using UnityEngine;

namespace ArcaneRecursion
{
    [CreateAssetMenu(menuName = "ArcaneRecursion/Events/GameEvent")]
    public class GameEvent : ScriptableObject
    {
        protected readonly List<GameEventListener> _eventListeners = new List<GameEventListener>();

        public virtual void Raise()
        {
            for (int i = _eventListeners.Count - 1; i >= 0; i--)
                _eventListeners[i].OnEventRaised();
        }

        public void RegisterListener(GameEventListener listener)
        {
            if (!_eventListeners.Contains(listener))
                _eventListeners.Add(listener);
        }

        public void UnregisterListener(GameEventListener listener)
        {
            if (_eventListeners.Contains(listener))
                _eventListeners.Remove(listener);
        }
    }
}