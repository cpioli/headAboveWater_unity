using UnityEngine;
using UnityEngine.UI;
using System.Text;
using cpioli.Events;
using cpioli.Variables;

public class LevelCompleteMenuBehaviour : MonoBehaviour, ICommonGameEvents {

    private Button[] menuButtons;
    private Text[] menuTexts;
    private Text timeText;
    private Image[] menuImages;
    private StringBuilder sb; //requires the "using System.Text" declaration

    public FloatReference totalTimePassed;

    void Awake()
    {
        sb = new StringBuilder();
        timeText = GameObject.Find("TimeText").GetComponent<Text>();
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
        DisplayTime();
    }

    private void DisplayTime()
    {
        print("Bringing up the time:");
        float minutes = 0.0f;
        float seconds = 0.0f;
        sb.Remove(0, sb.Length); //flush the StringBuilder
        sb.Append("0");
        minutes = Mathf.FloorToInt(totalTimePassed.Value / 60.0f);
        minutes = Mathf.Clamp(minutes, 0.0f, 9.0f);
        sb.Append(minutes.ToString("N0") + ":");
        if (seconds > 600.0f) //over ten minutes have passed
            seconds = 59.99f;
        else
        {
            seconds = totalTimePassed.Value % 60.0f;
            if (seconds < 10.0f)
                sb.Append("0");
        }

        sb.Append(seconds.ToString("N2"));
        timeText.text = "Time: " + sb.ToString();
    }

    public void GameOver()
    {
    }

    public void GamePaused()
    {
    }

    public void GameResumed()
    {
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
