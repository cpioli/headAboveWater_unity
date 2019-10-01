using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using cpioli.Events;

namespace cpioli.States
{
    public class GamePausedState : GameState
    {
        public GameEvent PauseEvent;
        public GameEvent ResumeEvent;

        public GameState InPlayState;

        public override void OnStateEnter(GameManager gm)
        {
            base.OnStateEnter(gm);
            PauseEvent.Raise();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            ResumeEvent.Raise();
        }

        public override void Act()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                //gm.ChangeGameState(InPlayState);
                return;
            }
        }
    }

}
