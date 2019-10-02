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

        public override void OnStateEnter(GameManager gm)
        {
            base.OnStateEnter(gm);
            Debug.Log("Entering the LevelCompleteState");
            LevelCompleteEvent.Raise();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            Debug.Log("Exiting the LevelCompleteState");
        }

        public override void Act()
        {

        }
    }
}

