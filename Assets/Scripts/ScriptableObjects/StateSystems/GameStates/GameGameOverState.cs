using UnityEngine;
using cpioli.Events;

namespace cpioli.States
{
    [CreateAssetMenu (menuName ="StateSystem/Game/GameOver", order = 3)]
    public class GameGameOverState : GameState
    {
        public GameEventListenerObj RestartListener;
        public GameEvent GameOverEvent;
        public GameState InPlayState;
        
        public override void OnStateEnter(GameManager gm)
        {
            Debug.Log("Entering GameOver State!");
            GameOverEvent.Raise();
            RestartListener.Event.RegisterListener(RestartListener);
            base.OnStateEnter(gm);
        }

        public override void OnStateExit()
        {
            Debug.Log("Exiting GameOverState!");
            RestartListener.Event.UnregisterListener(RestartListener);
            base.OnStateExit();
        }

        public override void Act()
        {
            //do nothing
        }

        public void ResponseToLevelBeginListener()
        {
            gm.ChangeGameState(InPlayState);
            return;
        }
    }
}

