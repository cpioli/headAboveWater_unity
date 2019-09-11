using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace cpioli.Events
{

    [System.Serializable]
    public class GameEventListenerObj
    {
        public string name = "";
        [Tooltip("Event to register with.")]
        public GameEvent Event;
        [Tooltip("Response to invoke when Event is raised.")]
        public UnityEvent Response;

        /*private void OnEnable()
        {
            try
            {
                Event.RegisterListener(this);
            }
            catch (NullReferenceException e)
            {
                Debug.LogError("NullReferenceException:Event missing!  GameObject: " + this.name);
            }
        }

        public void OnDisable()
        {
            Event.UnregisterListener(this);
        }*/

        public void OnEventRaised()
        {
            Response.Invoke();
        }
    }
}

