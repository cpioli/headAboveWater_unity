using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cpioli.Variables;

namespace cpioli
{
    public abstract class Movement : ScriptableObject
    {

        public bool isDefault;
        public Vector2Reference moveSpeed;
        public FloatReference gravityModifier;

        protected PlayerPlatformController ppc;

        public abstract Vector2 ComputeVelocity(bool underwater, bool exhausted, ref Vector2 velocity);

        public virtual void Initialize(GameObject obj)
        {
            ppc = obj.GetComponent<PlayerPlatformController>();
            ppc.gravityModifier = this.gravityModifier;
            ppc.maxSpeed = this.moveSpeed.Value.x;
        }
        
    }
}

