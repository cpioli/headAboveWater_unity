using UnityEngine;
using UnityEngine.Events;
using cpioli.Variables;

namespace cpioli
{
    [CreateAssetMenu (menuName = "Characters/UnderwaterMovement")]
    public class UnderwaterMovement : Movement
    {
        public FloatReference jumpTakeOffSpeed;
        public UnityEvent StrokeEvent;
        public UnityEvent UnderwaterStrokeEvent;
        public UnityEvent SurfaceStrokeEvent;

        private bool xMovement;
        private Animator anim;

        public override Vector2 ComputeVelocity(bool underwater, bool exhausted, ref Vector2 velocity)
        {
            Vector2 move = Vector2.zero;
            move.x = Input.GetAxis("Horizontal");
            if(!exhausted) ComputeYVelocity(underwater, ref velocity);
            return move;
        }

        private void ComputeYVelocity(bool underwater, ref Vector2 velocity)
        {
            if (Input.GetButtonDown("Jump"))
            {
                // do a swim stroke
                velocity.y = jumpTakeOffSpeed;
                anim.SetTrigger("strokePerformed");
                StrokeEvent.Invoke();
                if (underwater) UnderwaterStrokeEvent.Invoke();
                else SurfaceStrokeEvent.Invoke();
            }
        }

        public override void Initialize(GameObject obj)
        {
            base.Initialize(obj);
            anim = ppc.GetComponent<Animator>();
            anim.SetBool("inWater", true);
            ppc.jumpTakeOffSpeed = this.jumpTakeOffSpeed;
        }
        
    }
}

