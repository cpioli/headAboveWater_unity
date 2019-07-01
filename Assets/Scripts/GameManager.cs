using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {

    private GameState currentGameState;
    private InPlayState inPlayState;
    


	// Use this for initialization
	void Start () {
        inPlayState = GetComponent<InPlayState>();
        currentGameState = inPlayState;
	}

	// Update is called once per frame
	void Update () {
        currentGameState.Run();
    }

    private void HandleAllInput()
    {
        
    }
}
