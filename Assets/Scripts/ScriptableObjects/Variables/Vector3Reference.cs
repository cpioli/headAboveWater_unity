using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cpioli.Variables
{
    [System.Serializable]
    public class Vector3Reference
    {
        public bool UseConstant = true;
        public Vector3 ConstantValue;
        public Vector3Variable Variable;

        public Vector3Reference()
        { }

        public Vector2 Value
        {
            get { return UseConstant ? ConstantValue : Variable.Value; }
        }

        public static implicit operator Vector3(Vector3Reference reference)
        {
            return reference.Value;
        }
    }
}

