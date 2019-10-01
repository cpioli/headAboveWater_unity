using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cpioli.Events;

namespace cpioli.States
{
    public class GameGameOverState : GameState
    {
        public GameEvent GameOverEvent;

        public override void OnStateEnter(GameManager gm)
        {
            base.OnStateEnter(gm);
            GameOverEvent.Raise();
        }

        public override void OnStateExit(GameManager gm)
        {
            base.OnStateExit(gm);
        }

        public override void Act(GameManager gm)
        {
            //do nothing
        }
    }
}

