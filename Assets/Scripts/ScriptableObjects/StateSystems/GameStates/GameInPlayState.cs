using UnityEngine;
using cpioli.Events;

namespace cpioli.States
{
    public class GameInPlayState : GameState
    {
        public GameEvent LevelBeginEvent;

        public GameState PausedState;

        public override void OnStateEnter(GameManager gm)
        {
            base.OnStateEnter(gm);
            LevelBeginEvent.Raise();
            Debug.Log("Beginning Level");
        }

        public override void OnStateExit()
        {
            Debug.Log("Exiting GameInPlayState");
            base.OnStateExit();
        }

        public override void Act()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                //gm.ChangeGameState(PausedState);
            } 
        }
    }
}

