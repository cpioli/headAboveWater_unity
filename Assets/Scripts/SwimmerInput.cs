using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwimmerInput : MonoBehaviour {

    /// <summary>
    /// Houses data related the button's position on-screen and its interaction state.
    /// Methods equivalent to Input.GetKey can be called on each button.
    /// </summary>
    /*struct ButtonData
    {
        enum ButtonState { UNTOUCHED, PRESSED, HELD_DOWN, RELEASED };
        ButtonState state;
        public float radius;
        public Vector2 position;

        public ButtonData(RawImage img)
        {
            state = ButtonState.UNTOUCHED;
            radius = img.rectTransform.sizeDelta.x / 2.0f;
            position = new Vector2(img.rectTransform.localPosition.x, img.rectTransform.localPosition.y);
        }

        public bool InRange(Vector2 position)
        {
            return Vector2.Distance(this.position, position) <= radius;
        }

        public bool GetKeyDown()
        {
            return (state == ButtonState.PRESSED);
        }

        public bool GetKey()
        {
            return state == ButtonState.PRESSED || state == ButtonState.HELD_DOWN;
        }

        public bool GetKeyUp()
        {
            return state == ButtonState.RELEASED;
        }

        public void UpdateData(bool isPressed)
        {
            switch(state)
            {
                case ButtonState.UNTOUCHED:
                    if(isPressed)
                    {
                        state = ButtonState.PRESSED;
                    }
                    break;
                case ButtonState.PRESSED:
                    if(isPressed)
                    {
                        state = ButtonState.HELD_DOWN;
                    } else
                    {
                        state = ButtonState.RELEASED;
                    }
                    break;
                case ButtonState.HELD_DOWN:
                    if(!isPressed)
                    {
                        state = ButtonState.RELEASED;
                    } //no need to check the alternative: the state will remain the same
                    break;
                case ButtonState.RELEASED:
                    if(isPressed)
                    {
                        state = ButtonState.PRESSED;
                    } else
                    {
                        state = ButtonState.UNTOUCHED;
                    }
                    break;
            }
        }
    }*/

    /*private ButtonData leftMoveData, rightMoveData, leftJumpData, rightJumpData;
    private Touch[] touchList;

    public RawImage rightMovementImage;
    public RawImage leftMovementImage;
    public RawImage rightJumpImage;
    public RawImage leftJumpImage;

    void Awake()
    {
        leftMoveData = new ButtonData(leftMovementImage);
        leftJumpData = new ButtonData(leftJumpImage);
        rightMoveData = new ButtonData(rightMovementImage);
        rightJumpData = new ButtonData(rightJumpImage);
    }

    private void FixedUpdate()
    {
        bool leftMoveTouched, leftJumpTouched, rightMoveTouched, rightJumpTouched;
        leftMoveTouched = leftJumpTouched = rightMoveTouched = rightJumpTouched = false;
        touchList = Input.touches;
        Vector2 touchPosition;
        for (int i = 0; i < touchList.Length; i++)
        {
            touchPosition = touchList[i].position;
            if (leftMoveData.InRange(touchPosition))
            {
                leftMoveTouched = true;
                continue;
            }
            else if (rightMoveData.InRange(touchPosition))
            {
                rightMoveTouched = true;
                continue;
            }
            else if (leftJumpData.InRange(touchPosition))
            {
                leftJumpTouched = true;
                continue;
            }
            else if (rightJumpData.InRange(touchPosition))
            {
                rightJumpTouched = true;
                continue;
            }
        }
        leftMoveData.UpdateData(leftMoveTouched);
        leftJumpData.UpdateData(leftJumpTouched);
        rightMoveData.UpdateData(rightMoveTouched);
        rightJumpData.UpdateData(rightJumpTouched);
    }*/

    public static bool GetKey(KeyCode key)
    {
#if UNITY_STANDALONE
        return Input.GetKey(key);
#elif UNITY_IOS || UNITY_ANDROID
        return GetButton(key);
#endif
    }

   /* private bool GetTouchInput(KeyCode key)
    {
        switch(key)
        {
            case KeyCode.A:
                return leftMoveData.GetKey();
            case KeyCode.D:
                return rightMoveData.GetKey();
            case KeyCode.Space:
                return leftJumpData.GetKey() || rightJumpData.GetKey();
        }
        return false;
    }*/

    public static bool GetButtonDown(string buttonName)
    {
#if UNITY_STANDALONE
        return Input.GetButtonDown(buttonName);
#elif UNITY_IOS || UNITY_ANDROID
        return GetButtonDown(key);
#endif
    }

    //TODO: implement GetTouchInputDown(string buttonName)

    public static bool GetKeyDown(KeyCode key)
    {
#if UNITY_STANDALONE
        return Input.GetKeyDown(key);
#elif UNITY_IOS || UNITY_ANDROID
        return GetButtonDown(key);
#endif
    }

    /*private bool GetTouchInputDown(KeyCode key)
    {
        switch(key)
        {
            case KeyCode.A:
                return leftMoveData.GetKeyDown();
            case KeyCode.D:
                return rightMoveData.GetKeyDown();
            case KeyCode.Space:
                return leftJumpData.GetKeyDown() || rightJumpData.GetKeyDown();
            
        }
        return false;
    }*/

    public static bool GetKeyUp(KeyCode key)
    {
#if UNITY_STANDALONE
        return Input.GetKeyUp(key);
#elif UNITY_IOS || UNITY_ANDROID
        return GetButtonUp(key);
#endif
    }

    /*private bool GetTouchInputUp(KeyCode key)
    {
        switch(key)
        {
            case KeyCode.A:
                return leftMoveData.GetKeyUp();
            case KeyCode.D:
                return rightMoveData.GetKeyUp();
            case KeyCode.Space:
                return leftJumpData.GetKeyUp() || rightJumpData.GetKeyUp();
        }
        return false;
    }*/

    public static float GetAxis(string axisName)
    {
#if UNITY_STANDALONE
        return Input.GetAxis(axisName);
#endif

#if UNITY_IOS || UNITY_ANDROID
            return GetTouchXAxis();
#endif
    }

   /* private float GetTouchXAxis()
    {
        float axisValue = 0.0f;
        if (leftMoveData.GetKeyDown()) axisValue -= 1.0f;
        if (rightMoveData.GetKeyDown()) axisValue += 1.0f;
        return axisValue;
    }*/
}
