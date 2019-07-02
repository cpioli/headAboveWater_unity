using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cpioli.Variables
{
    public class Vector3Variable : ScriptableObject
    {
#if UNITY_EDITOR
        [TextArea(3, 5)]
        public string DeveloperDescription = "";
#endif
        public Vector3 Value;

        public void SetValue(Vector3 value)
        {
            Value = value;
        }

        public void SetValue(Vector3Variable value)
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

        public void SetZValue(float z)
        {
            Value.z = z;
        }

        public void SetZValue(FloatVariable z)
        {
            Value.z = z.Value;
        }

        public void ApplyChange(Vector3 amount)
        {
            Value = Value + amount;
        }

        public void ApplyChange(Vector3Variable amount)
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

        public void ApplyZChange(float amount)
        {
            Value.z += amount;
        }

        public void ApplyZChange(FloatVariable amount)
        {
            Value.z += amount.Value;
        }
    }
}

