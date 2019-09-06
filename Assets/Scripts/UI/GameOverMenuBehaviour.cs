using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cpioli.Events;
using UnityEngine.UI;

public class GameOverMenuBehaviour : MonoBehaviour, ICommonGameEvents {

    private Button[] menuButtons;
    private Text[] menuTexts;
    private Image[] menuImages;

    void Awake()
    {
        menuButtons = GetComponentsInChildren<Button>();
        menuTexts = GetComponentsInChildren<Text>();
        menuImages = GetComponentsInChildren<Image>();
        Hide();
    }

    private void Hide()
    {
        foreach (Button b in menuButtons)
        {
            b.enabled = false;
        }
        foreach (Text t in menuTexts)
        {
            t.enabled = false;
        }
        foreach (Image i in menuImages)
        {
            i.enabled = false;
        }
    }

    private void Show()
    {
        foreach (Button b in menuButtons)
        {
            b.enabled = true;
        }
        foreach (Text t in menuTexts)
        {
            t.enabled = true;
        }
        foreach (Image i in menuImages)
        {
            i.enabled = true;
        }
    }

    public void GameOver()
    {
        Show();
    }

    public void GamePaused()
    {
    }

    public void GameResumed()
    {
    }

    public void LevelCompleted()
    {
    }

    public void LevelStarted()
    {
        Hide();
    }
}
