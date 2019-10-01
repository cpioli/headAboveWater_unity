using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaterBehaviour : MonoBehaviour {

    public UnityEvent splashEvent;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Swimmer")
        {
            splashEvent.Invoke();
        }
    }
}
