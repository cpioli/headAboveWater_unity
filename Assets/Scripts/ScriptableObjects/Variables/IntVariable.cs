using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cpioli.Variables
{
    [CreateAssetMenu(menuName = "Variable/Int", order = 2)]
    public class IntVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [TextArea(3, 5)]
        public string DeveloperDescription = "";
#endif
        public int Value;

        public void SetValue(int value)
        {
            this.Value = value;
        }

        public void SetValue(IntVariable value)
        {
            this.Value = value.Value;
        }

        public void ApplyChange(int amount)
        {
            this.Value += amount;
        }

        public void ApplyChange(IntVariable amount)
        {
            this.Value += amount.Value;
        }
    }
}

