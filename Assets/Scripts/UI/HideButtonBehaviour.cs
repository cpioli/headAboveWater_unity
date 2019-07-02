using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideButtonBehaviour : MonoBehaviour {

    private Text buttonText;
    private Image buttonImage;
    private Button button;

    void Awake()
    {
        buttonImage = GetComponent<Image>();
        buttonText = gameObject.GetComponentInChildren<Text>();
        button = GetComponent<Button>();

        buttonImage.enabled = false;
        buttonText.enabled = false;
        button.enabled = false;
    }

    //Listens to GameOverEvent
    public void GameOver()
    {
        buttonImage.enabled = true;
        buttonText.enabled = true;
        button.enabled = true;
    }

    public void LevelStart()
    {
        buttonImage.enabled = false;
        buttonText.enabled = false;
        button.enabled = false;
    }
}
