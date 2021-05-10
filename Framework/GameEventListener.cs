﻿using System.Collections;
using UnityEngine.Events;
using UnityEngine;

namespace ArcaneRecursion
{
    public class GameEventListener : MonoBehaviour
    {
        public GameEvent GameEvent;
        public UnityEvent Response;

        private void OnEnable()
        {
            GameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            GameEvent.UnregisterListener(this);
        }

        public void OnEventRaised()
        {
            Response.Invoke();
        }
    }
}