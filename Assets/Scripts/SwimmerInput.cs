using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace cpioli
{
    public class SwimmerInput
    {
        Touch[] touchList;
        SwimmerInput()
        {

        }
        public bool GetButtonDown(string buttonName)
        {
            return Input.GetButtonDown(buttonName);
        }

        public bool GetButtonUp(string buttonName)
        {
            return Input.GetButtonUp(buttonName);
        }

        public bool GetKey(KeyCode key)
        {
            return Input.GetKey(key);
        }

        public bool GetKeyDown(KeyCode key)
        {
            if(key == KeyCode.Space)
            {

            }
            return Input.GetKeyDown(key);
        }

        public float GetXAxis()
        {            
#if UNITY_STANDALONE
                return Input.GetAxis("Horizontal");
#endif

#if UNITY_IOS || UNITY_ANDROID
                return GetTouchXAxis();
#endif
        }

        private float GetTouchXAxis()
        {
            float axisValue = 0.0f;
            touchList = Input.touches;
            Vector2 touchPosition;
            for(int i = 0; i < touchList.Length; i++)
            {
                touchPosition = touchList[i].position;
            }
            return axisValue;
        }
    }
}

