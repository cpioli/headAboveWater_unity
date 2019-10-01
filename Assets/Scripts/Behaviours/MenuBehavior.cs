using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MenuBehavior : MonoBehaviour {

    GameObject panel;
    [HideInInspector]
    public bool isPaused;

    public UnityEvent pauseEvent;
    public UnityEvent unpauseEvent;
	// Use this for initialization
	void Start () {
        panel = GameObject.Find("Pause Menu");
        panel.SetActive(false);
	}

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if(!isPaused)
            {
                panel.SetActive(true);
                pauseEvent.Invoke();
                Time.timeScale = 0.0f;
            }
            else
            {
                print("Set menu to inactive now!");
                unpauseEvent.Invoke();
                panel.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
    }

    public void ResumeButtonPressed()
    {
        panel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void RestartButtonPressed()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Scene1");
    }
}
