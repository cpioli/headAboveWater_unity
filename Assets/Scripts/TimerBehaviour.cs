using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class TimerBehaviour : MonoBehaviour {

    private StringBuilder sb; //requires the "using System.Text" declaration
    private Text textObject;
    private float totalTimePassed;
    private float minutes;
    private float seconds;
    private bool paused;

	// Use this for initialization
	void Start () {
        sb = new StringBuilder();
        textObject = gameObject.GetComponent<Text>();
        totalTimePassed = 0.0f;
        minutes = 0.0f;
        seconds = 0.0f;
        paused = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (paused) return;
        ConvertToTime();
	}

    private void ConvertToTime()
    {
        totalTimePassed += Time.deltaTime;
        sb.Remove(0, sb.Length); //flush the StringBuilder
        sb.Append("0");
        minutes = Mathf.FloorToInt(totalTimePassed / 60.0f);
        minutes = Mathf.Clamp(minutes, 0.0f, 9.0f);
        sb.Append(minutes.ToString("N0") + ":");
        if (seconds > 600.0f) //over ten minutes have passed
            seconds = 59.99f;
        else
        {
            seconds = totalTimePassed % 60.0f;
            if (seconds < 10.0f)
                sb.Append("0");
        }

        sb.Append(seconds.ToString("N2"));
        textObject.text = sb.ToString();
    }

    //Listens to PauseEvent
    public void Pause()
    {
        paused = true;
    }

    //Listens to UnpauseEvent
    public void Resume()
    {
        paused = false;
    }

    //Listens to GameOverEvent
    public void Stop()
    {
        paused = true;
    }

    //Listens to LevelStartEvent
    public void Play()
    {
        paused = false;
    }

    //Listens to RestartLevelEvent
    public void Reset()
    {
        totalTimePassed = 0.0f;
    }
}
    