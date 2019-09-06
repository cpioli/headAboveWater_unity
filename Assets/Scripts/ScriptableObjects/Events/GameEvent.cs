using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cpioli.Events
{
    [CreateAssetMenu]
    public class GameEvent : ScriptableObject
    {
        ///<summary>
        /// The list of listeners that this event will notify if raised
        /// </summary>
        private readonly List<GameEventListenerObj> eventListenerObjs = new List<GameEventListenerObj>();

        public void Raise()
        {
            for(int i = eventListenerObjs.Count - 1; i >= 0; i--)
            {
                eventListenerObjs[i].OnEventRaised();
            }
        }

        public void RegisterListener(GameEventListenerObj listener)
        {
            if (!eventListenerObjs.Contains(listener))
            {
                eventListenerObjs.Add(listener);
            }
        }

        public void UnregisterListener(GameEventListenerObj listener)
        {
            if (eventListenerObjs.Contains(listener))
            {
                eventListenerObjs.Remove(listener);
            }
        }
    }

}
