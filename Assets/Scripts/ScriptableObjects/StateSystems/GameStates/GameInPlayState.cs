using UnityEngine;
using UnityEngine.Events;
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

        public override void OnStateExit(GameManager gm)
        {
            Debug.Log("Exiting GameInPlayState");
            base.OnStateExit(gm);
        }

        public override void Act(GameManager gm)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                //gm.ChangeGameState(PausedState);
            } 
        }
    }
}

