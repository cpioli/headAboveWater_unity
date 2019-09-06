using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using cpioli.Events;

public class PauseMenuBehaviour : MonoBehaviour, ICommonGameEvents {

    private Text text;

    void Awake()
    {
        text = GetComponentInChildren<Text>();
        text.enabled = false;
    }

    public void GamePaused()
    {
        text.enabled = true;
    }

    public void GameResumed()
    {
        text.enabled = false;
    }

    public void GameOver()
    {
    }

    public void LevelStarted()
    {
    }

    public void LevelCompleted()
    {
    }

}
