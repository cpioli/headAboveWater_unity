using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private GameState currentGameState;
    private InPlayState inPlayState;
    private GameOverState gameOverState;

    private GameManager instance;

	// Use this for initialization
	void Awake () {
        if (instance == null) instance = this;
        else if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        inPlayState = GetComponent<InPlayState>();
        gameOverState = GetComponent<GameOverState>();
        currentGameState = inPlayState;
        currentGameState.OnStateEnter(this);
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

    public void RestartLevel()
    {
        ChangeGameState(inPlayState);
    }
}
