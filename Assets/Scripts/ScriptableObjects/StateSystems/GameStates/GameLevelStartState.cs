using UnityEngine;
using cpioli.Events;

namespace cpioli.States
{
    [CreateAssetMenu (menuName ="StateSystem/Game/LevelStart", order = 0)]
    public class GameLevelStartState : GameState
    {
        public GameEventListenerObj Play;
        public GameState InPlay;
        public GameObject StartPlug;

        public override void OnStateEnter(GameManager gm)
        {
            StartPlug.GetComponent<BoxCollider2D>().enabled = false;
            Debug.Log("Entered the GameState: LevelStart");
            base.OnStateEnter(gm);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        public override void Act()
        {
            if(!StartPlug.GetComponent<BoxCollider2D>().enabled)
            {

            }
        }

        public void RespondToInPlayEvent()
        {
            gm.ChangeGameState(InPlay);
            return;
        }
    }

}
