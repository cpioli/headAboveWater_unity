using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchInputMenuBehaviour : MonoBehaviour {

#if UNITY_STANDALONE
    void Awake () {
        this.gameObject.SetActive(false);
	}
#endif
}
