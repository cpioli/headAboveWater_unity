using UnityEngine;
using cpioli.Events;

namespace cpioli.States
{
    public abstract class GameState : ScriptableObject
    {
        private GameManager gm;
        public GameEventListenerList listeners;

        public abstract void Act();
        public virtual void OnStateEnter(GameManager gm)
        {
            if(this.gm == null) this.gm = gm;
        }
        public virtual void OnStateExit()
        {

        }
    }
}


