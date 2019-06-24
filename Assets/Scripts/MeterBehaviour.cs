using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterBehaviour : MonoBehaviour {

    public GameObject dataPrefab; //the prefab containing the Resource
    public Image meter;

    private float maxImageLength;
    private Resource resourceRef;

    void Start()
    {
        maxImageLength = meter.rectTransform.rect.width;
        resourceRef = dataPrefab.GetComponent<Resource>();
    }
	
	// Update is called once per frame
	void Update () {
        ResizeBar();
	}

    private void ResizeBar()
    {
        float newWidth = resourceRef.GetCurrentValue() / resourceRef.maxValue.Value * maxImageLength;
        meter.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
    }
}
