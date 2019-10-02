using UnityEngine;
using cpioli.Events;

namespace cpioli.States
{
    [CreateAssetMenu (menuName = "StateSystem/Game/InPlay", order = 1)]
    public class GameInPlayState : GameState
    {
        private PlayerPlatformController ppc;

        public PlayerMovementState ledgeHangingState;
        public GameEvent LevelBeginEvent;
        public GameEventListenerObj SwimmerDiesListener;
        public GameState PausedState;
        public GameState GameOverState;
        public GameState LevelCompleteState;

        public override void OnStateEnter(GameManager gm)
        {
            base.OnStateEnter(gm);
            SwimmerDiesListener.Event.RegisterListener(SwimmerDiesListener);
            ppc = gm.Swimmer.GetComponent<PlayerPlatformController>();

            Debug.Log("Beginning Level");
        }

        public override void OnStateExit()
        {
            Debug.Log("Exiting GameInPlayState");
            SwimmerDiesListener.Event.UnregisterListener(SwimmerDiesListener);
            base.OnStateExit();
        }

        public override void Act()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                gm.ChangeGameState(PausedState);
                return;
            }
            if (ppc.currentPMState.name.Equals(ledgeHangingState.name))
            {
                if(ppc.FindCollision("LevelCompleteLedge"))
                {
                    gm.ChangeGameState(LevelCompleteState);
                }
            }
        }

        public void ResponseToSwimmerDiesEvent()
        {
            gm.ChangeGameState(GameOverState);
            return;
        }
    }
}

