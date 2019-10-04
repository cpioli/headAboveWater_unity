using UnityEngine;
using UnityEngine.UI;
using System.Text;
using cpioli.Events;
using cpioli.Variables;

public class TimerBehaviour : MonoBehaviour, ICommonGameEvents {

    private StringBuilder sb; //requires the "using System.Text" declaration
    private Text textObject;
    private float minutes;
    private float seconds;
    private bool paused;

    public FloatVariable totalTimePassed;

    // Use this for initialization
    void Start () {
        sb = new StringBuilder();
        textObject = gameObject.GetComponent<Text>();

        paused = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (paused) return;
        ConvertToTime();
	}

    private void ConvertToTime()
    {
        totalTimePassed.Value += Time.deltaTime;
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
        textObject.text = sb.ToString();
    }

    //Listens to RestartLevelEvent
    public void Reset()
    {
        totalTimePassed.Value = 0.0f;
    }

    public void GamePaused()
    {
        paused = true;
    }

    public void GameResumed()
    {
        paused = false;
    }

    public void GameOver()
    {
        paused = true;
    }

    public void LevelStarted()
    {
        paused = false;
        totalTimePassed.SetValue(0.0f);
        minutes = 0.0f;
        seconds = 0.0f;
    }

    public void LevelCompleted()
    {
        paused = true;
    }
}
    