using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private GameState currentGameState;
    private InPlayState inPlayState;
    private GameOverState gameOverState;
    


	// Use this for initialization
	void Start () {
        inPlayState = GetComponent<InPlayState>();
        gameOverState = GetComponent<GameOverState>();
        currentGameState = inPlayState;
	}

	// Update is called once per frame
	void Update () {
        currentGameState.Run();
    }

    private void ChangeGameState(GameState newGameState)
    {
        currentGameState.OnStateExit();
        currentGameState = newGameState;
        currentGameState.OnStateEnter(this);
    }

    public void SwimmerDies()
    {
        ChangeGameState(gameOverState);
    }
}
