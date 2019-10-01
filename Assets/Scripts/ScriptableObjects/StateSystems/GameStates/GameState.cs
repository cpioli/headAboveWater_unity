using UnityEngine;

namespace cpioli.States
{
    public abstract class GameState : ScriptableObject
    {
        public abstract void Act(GameManager gm);
        public virtual void OnStateEnter(GameManager gm)
        {

        }
        public virtual void OnStateExit(GameManager gm)
        {

        }
    }
}


