using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cpioli.States;

public class GameManager : MonoBehaviour {

    private GameManager instance;
    private cpioli.States.GameState currentGameState;

    public cpioli.States.GameState initialState;
    public GameObject Swimmer;

	// Use this for initialization
	void Awake () {
        if (instance == null) instance = this;
        else if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        initialState.OnStateEnter(this);
	}

	// Update is called once per frame
	void Update () {
        instance.currentGameState.Act();
    }

    public void ChangeGameState(cpioli.States.GameState newGameState)
    {
        instance.currentGameState.OnStateExit();
        instance.currentGameState = newGameState;
        instance.currentGameState.OnStateEnter(this);
    }
}
