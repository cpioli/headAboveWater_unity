using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterruptedBehaviour : MonoBehaviour {

    private Text text;

	void Start () {
        text = gameObject.GetComponent<Text>();
        text.enabled = false;
	}

    //Listens to PausedEvent
    public void Pause()
    {
        text.enabled = true;
    }

    //Listens to ResumeEvent
    public void Resume()
    {
        text.enabled = false;
    }
}
