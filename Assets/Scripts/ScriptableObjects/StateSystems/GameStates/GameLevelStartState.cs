using UnityEngine;
using cpioli.Events;

namespace cpioli.States
{
    [CreateAssetMenu (menuName ="StateSystem/Game/LevelStart", order = 0)]
    public class GameLevelStartState : GameState
    {
        private BoxCollider2D startPlugCollider;

        public GameEvent LevelBeginEvent;
        public GameEventListenerObj SubmergedEventListener;
        public GameState InPlay;

        public override void OnStateEnter(GameManager gm)
        {
            Debug.Log("Entered the GameState: LevelStart");
            SubmergedEventListener.Event.RegisterListener(SubmergedEventListener);
            LevelBeginEvent.Raise();
            base.OnStateEnter(gm);
        }

        public override void OnStateExit()
        {
            Debug.Log("Exiting the GameState: LevelStart");
            SubmergedEventListener.Event.UnregisterListener(SubmergedEventListener);
            base.OnStateExit();
        }

        public override void Act()
        {

        }

        public void RespondToSubmergedEvent()
        {
            Debug.Log("LevelStartState is responding to the submerged event!");
            gm.ChangeGameState(InPlay);
            return;
        }
    }

}
