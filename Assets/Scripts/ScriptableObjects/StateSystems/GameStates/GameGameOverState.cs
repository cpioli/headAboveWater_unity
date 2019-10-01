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

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        public override void Act()
        {
            //do nothing
        }
    }
}

