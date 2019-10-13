using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwimmerInput : MonoBehaviour {

    /// <summary>
    /// Houses data related the button's position on-screen and its interaction state.
    /// Methods equivalent to Input.GetKey can be called on each button.
    /// </summary>
    struct ButtonData
    {
        private RawImage image;
        enum ButtonState { UNTOUCHED, PRESSED, HELD_DOWN, RELEASED };
        ButtonState state;
        public float radius;
        public Vector2 position;

        public ButtonData(RawImage img, float wa, float ha)
        {
            image = img;
            state = ButtonState.UNTOUCHED;
            radius = img.rectTransform.sizeDelta.x / 2.0f;
            position = new Vector2(
                img.rectTransform.localPosition.x + wa, img.rectTransform.localPosition.y + ha);
        }

        public bool InRange(Vector2 position)
        {
            return Vector2.Distance(this.position, position) <= radius;
        }

        public void print()
        {
            Debug.Log(this.position + " " + this.radius);
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
                        image.color = Color.gray;
                    }
                    break;
                case ButtonState.PRESSED:
                    if(isPressed)
                    {
                        state = ButtonState.HELD_DOWN;
                    } else
                    {
                        state = ButtonState.RELEASED;
                        image.color = Color.white;
                    }
                    break;
                case ButtonState.HELD_DOWN:
                    if(!isPressed)
                    {
                        state = ButtonState.RELEASED;
                        image.color = Color.white;
                    } //no need to check the alternative: the state will remain the same
                    break;
                case ButtonState.RELEASED:
                    if(isPressed)
                    {
                        state = ButtonState.PRESSED;
                        image.color = Color.gray;
                    } else
                    {
                        state = ButtonState.UNTOUCHED;
                    }
                    break;
            }
        }
    }

    private static SwimmerInput instance;
    private ButtonData leftMoveData, rightMoveData, leftJumpData, rightJumpData;
    private Touch[] touchList;
    private Camera mainCamera;

    public RawImage rightMovementImage;
    public RawImage leftMovementImage;
    public RawImage rightJumpImage;
    public RawImage leftJumpImage;

#if UNITY_IOS || UNITY_ANDROID
    void Awake()
    {
        if (instance == null)
            instance = this;

        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        float widthAdjustment = mainCamera.pixelWidth / 2.0f;
        float heightAdjustment = mainCamera.pixelHeight / 2.0f;
        instance.leftMoveData = new ButtonData(leftMovementImage, widthAdjustment, heightAdjustment);
        instance.leftJumpData = new ButtonData(leftJumpImage, widthAdjustment, heightAdjustment);
        instance.rightMoveData = new ButtonData(rightMovementImage, widthAdjustment, heightAdjustment);
        instance.rightJumpData = new ButtonData(rightJumpImage, widthAdjustment, heightAdjustment);
        touchList = new Touch[10];
    }

    private void FixedUpdate()
    {
        bool leftMoveTouched, leftJumpTouched, rightMoveTouched, rightJumpTouched;
        leftMoveTouched = leftJumpTouched = rightMoveTouched = rightJumpTouched = false;
        instance.touchList = Input.touches;
        Vector2 touchPosition;
        //if (touchList.Length > 0) print(touchList[0].position);
        for (int i = 0; i < touchList.Length; i++)
        {
            touchPosition = touchList[i].position;
            if (instance.leftMoveData.InRange(touchPosition))
            {
                leftMoveTouched = true;
                //print("Left move button is pressed!");
                continue;
            }
            else if (instance.rightMoveData.InRange(touchPosition))
            {
                //print("Right move button is pressed!");
                rightMoveTouched = true;
                continue;
            }
            else if (instance.leftJumpData.InRange(touchPosition))
            {
                leftJumpTouched = true;
                //print("Left jump button is pressed!");
                continue;
            }
            else if (instance.rightJumpData.InRange(touchPosition))
            {
                rightJumpTouched = true;
                //print("Right jump button is pressed!");
                continue;
            }
        }
        leftMoveData.UpdateData(leftMoveTouched);
        leftJumpData.UpdateData(leftJumpTouched);
        rightMoveData.UpdateData(rightMoveTouched);
        rightJumpData.UpdateData(rightJumpTouched);
    }
#endif

    public static bool GetKey(KeyCode key)
    {
#if UNITY_STANDALONE
        return Input.GetKey(key);
#elif UNITY_IOS || UNITY_ANDROID
        return instance.GetTouchInput(key);
#endif
    }

    private bool GetTouchInput(KeyCode key)
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
    }

    public static bool GetButtonDown(string buttonName)
    {
#if UNITY_STANDALONE
        return Input.GetButtonDown(buttonName);
#elif UNITY_IOS || UNITY_ANDROID
        return instance.GetTouchInputDown(buttonName);
#endif
    }

    //TODO: implement GetTouchInputDown(string buttonName)
    private bool GetTouchInputDown(string buttonName)
    {
        if (buttonName.Equals("Jump"))
            return leftJumpData.GetKeyDown() || rightJumpData.GetKeyDown();
        else
            return false;
    }

    public static bool GetKeyDown(KeyCode key)
    {
#if UNITY_STANDALONE
        return Input.GetKeyDown(key);
#elif UNITY_IOS || UNITY_ANDROID
        return instance.GetTouchInputDown(key);
#endif
    }

    protected bool GetTouchInputDown(KeyCode key)
    {
        switch(key)
        {
            case KeyCode.A:
                return leftMoveData.GetKeyDown();
            case KeyCode.D:
                return rightMoveData.GetKeyDown();
            case KeyCode.Space:
                return leftJumpData.GetKeyDown() || rightJumpData.GetKeyDown();
            case KeyCode.Escape:
            case KeyCode.Menu:
                if (Input.GetKeyDown(key))
                {
                    print(key.ToString());
                }
                return Input.GetKeyDown(key);
            
        }
        return false;
    }

    public static bool GetKeyUp(KeyCode key)
    {
#if UNITY_STANDALONE
        return Input.GetKeyUp(key);
#elif UNITY_IOS || UNITY_ANDROID
        return instance.GetTouchInputUp(key);
#endif
    }

    private bool GetTouchInputUp(KeyCode key)
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
    }

    public static float GetAxis(string axisName)
    {
#if UNITY_STANDALONE
        return Input.GetAxis(axisName);
#endif

#if UNITY_IOS || UNITY_ANDROID
        return instance.GetTouchXAxis();
#endif
    }

    private float GetTouchXAxis()
    {
        float axisValue = 0.0f;
        if (leftMoveData.GetKey()) axisValue -= 1.0f;
        if (rightMoveData.GetKey()) axisValue += 1.0f;
        return axisValue;
    }
}
