using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {

    private bool gamePaused;

    public UnityEvent PauseEvent;
    public UnityEvent ResumeEvent;

	// Use this for initialization
	void Start () {
        gamePaused = false;
	}
	
	// Update is called once per frame
	void Update () {
        HandleAllInput();
	}

    private void HandleAllInput()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (gamePaused)
                ResumeEvent.Invoke();
            else
                PauseEvent.Invoke();
            gamePaused = !gamePaused;
        }
    }
}
