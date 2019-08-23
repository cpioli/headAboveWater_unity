using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using cpioli.Events;

namespace cpioli.Events
{
    public class GameEventListenerList : MonoBehaviour
    {
        public GameEventListenerObj[] listeners;


        private void OnEnable()
        {
            for (int i = listeners.Length - 1; i >= 0; i--)
            {
                GameEventListenerObj gelo = listeners[i];
                try
                {
                    print("Enabling Listener " + gelo.name);
                    gelo.Event.RegisterListener(gelo);
                }
                catch (NullReferenceException e)
                {
                    Debug.LogError("NullReferenceException: Event missing! GameObject: " + gelo.name);
                }
            }
        }

        private void OnDisable()
        {
            for (int i = listeners.Length - 1; i >= 0; i--)
            {
                GameEventListenerObj gelo = listeners[i];
                if (gelo.Event == null) continue;
                gelo.Event.UnregisterListener(gelo);
            }
        }
    }
}

