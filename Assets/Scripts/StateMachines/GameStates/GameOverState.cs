using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class GameOverState : GameState {

    public UnityEvent GameOverEvent;

    public override void OnStateEnter(GameManager gm)
    {
        base.OnStateEnter(gm);
        GameOverEvent.Invoke();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override void Run()
    {
        base.Run();
    }
}
