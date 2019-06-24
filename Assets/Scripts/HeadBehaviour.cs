using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HeadBehaviour : MonoBehaviour {

    public UnityEvent submergedEvent;
    public UnityEvent surfacedEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "water")
        {
            submergedEvent.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "water")
        {
            surfacedEvent.Invoke();
        }
    }
}
