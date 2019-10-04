using UnityEngine;
using UnityEngine.UI;
using cpioli.Events;

public class LevelCompleteMenuBehaviour : MonoBehaviour, ICommonGameEvents {

    private Button[] menuButtons;
    private Text[] menuTexts;
    private Image[] menuImages;

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
        throw new System.NotImplementedException();
    }

    public void GamePaused()
    {
        throw new System.NotImplementedException();
    }

    public void GameResumed()
    {
        throw new System.NotImplementedException();
    }

    public void LevelCompleted()
    {
        Show();
    }

    public void LevelStarted()
    {
        Hide();
    }
}
