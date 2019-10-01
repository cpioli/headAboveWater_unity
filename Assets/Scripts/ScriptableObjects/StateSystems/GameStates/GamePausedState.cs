using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using cpioli.Events;

namespace cpioli.States
{
    [CreateAssetMenu (menuName = "StateSystem/Game/Paused", order = 2)]
    public class GamePausedState : GameState
    {
        public GameEvent PauseEvent;
        public GameEvent ResumeEvent;

        public GameState InPlayState;

        public override void OnStateEnter(GameManager gm)
        {
            Debug.Log("Entering Paused State");
            base.OnStateEnter(gm);
            PauseEvent.Raise();
        }

        public override void OnStateExit()
        {
            Debug.Log("Exiting Paused State!");
            base.OnStateExit();
            ResumeEvent.Raise();
        }

        public override void Act()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                gm.ChangeGameState(InPlayState);
                return;
            }
        }
    }

}
