using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InPlayState : GameState {

    private bool gamePaused;

    public UnityEvent PauseEvent;
    public UnityEvent ResumeEvent;

    public override void OnStateEnter(GameManager gm)
    {
        base.OnStateEnter(gm);
        gamePaused = false;
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override void Run()
    {
        base.Run();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
                ResumeEvent.Invoke();
            else
                PauseEvent.Invoke();
            gamePaused = !gamePaused;
        }
    }
}
