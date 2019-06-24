using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cpioli.Variables
{
    [CreateAssetMenu(menuName = "Variable/Float", order = 3)]
    public class FloatVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [TextArea(3, 5)]
        public string DeveloperDescription = "";
#endif
        public float Value;

        public void SetValue(float value)
        {
            Value = value;
        }

        public void SetValue(FloatVariable value)
        {
            Value = value.Value;
        }

        public void ApplyChange(float amount)
        {
            Value += amount;
        }

        public void ApplyChange(FloatVariable amount)
        {
            Value += amount.Value;
        }
    }
}