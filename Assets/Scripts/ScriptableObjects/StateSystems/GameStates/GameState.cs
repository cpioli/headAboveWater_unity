using UnityEngine;
using cpioli.Events;

namespace cpioli.States
{
    public abstract class GameState : ScriptableObject
    {
        protected GameManager gm;

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


