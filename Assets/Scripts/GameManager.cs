using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cpioli.States;

public class GameManager : MonoBehaviour {

    private cpioli.States.GameState currentGameState;

    public cpioli.States.GameState initialState;
    public GameObject Swimmer;

	// Use this for initialization
	void Awake () {

        ChangeGameState(initialState);
        //initialState.OnStateEnter(this);
	}

	// Update is called once per frame
	void Update () {
        currentGameState.Act();
    }

    public void ChangeGameState(cpioli.States.GameState newGameState)
    {
        if(currentGameState != null)
            currentGameState.OnStateExit();
        currentGameState = newGameState;
        currentGameState.OnStateEnter(this);
    }
}
