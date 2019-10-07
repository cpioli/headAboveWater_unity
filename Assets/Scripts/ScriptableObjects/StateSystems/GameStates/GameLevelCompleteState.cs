using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cpioli.Events;

namespace cpioli.States
{
    [CreateAssetMenu (menuName = "StateSystem/Game/LevelComplete", order = 4)]
    public class GameLevelCompleteState : GameState
    {
        public GameEvent LevelCompleteEvent;
        public GameState LevelBeginState;

        public GameEventListenerObj RestartListener;

        public override void OnStateEnter(GameManager gm)
        {
            base.OnStateEnter(gm);
            Debug.Log("Entering the LevelCompleteState");
            LevelCompleteEvent.Raise();
            RestartListener.Event.RegisterListener(RestartListener);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            Debug.Log("Exiting the LevelCompleteState");
            RestartListener.Event.UnregisterListener(RestartListener);
        }

        public override void Act()
        {

        }

        public void ResponseToRestartEvent()
        {
            Debug.Log("Responding to Restart Event!");
            gm.ChangeGameState(LevelBeginState);
        }
    }
}

