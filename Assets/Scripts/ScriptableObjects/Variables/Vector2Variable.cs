using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cpioli.Variables
{
    [CreateAssetMenu(menuName = "Variable/Vector2", order = 4)]
    public class Vector2Variable : ScriptableObject
    {
#if UNITY_EDITOR
        [TextArea(3,5)]
        public string DeveloperDescription = "";
#endif
        public Vector2 Value;

        public void SetValue(Vector2 value)
        {
            Value = value;
        }

        public void SetValue(Vector2Variable value)
        {
            Value = value.Value;
        }

        public void SetXValue(float x)
        {
            Value.x = x;
        }

        public void SetXValue(FloatVariable x)
        {
            Value.x = x.Value;
        }

        public void SetYValue(float y)
        {
            Value.y = y;
        }

        public void SetYValue(FloatVariable y)
        {
            Value.y = y.Value;
        }

        public void ApplyChange(Vector2 amount)
        {
            Value = Value + amount;
        }

        public void ApplyChange(Vector2Variable amount)
        {
            Value = Value + amount.Value;
        }

        public void ApplyXChange(float amount)
        {
            Value.x += amount;
        }
        
        public void ApplyXChange(FloatVariable amount)
        {
            Value.x += amount.Value;
        }

        public void ApplyYChange(float amount)
        {
            Value.y += amount;
        }

        public void ApplyYChange(FloatVariable amount)
        {
            Value.y += amount.Value;
        }

    }
}

