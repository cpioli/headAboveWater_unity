using UnityEngine;
using UnityEngine.Events;
using cpioli.Variables;

namespace cpioli
{
    [CreateAssetMenu (menuName ="Characters/GroundMovement")]
    public class GroundMovement : Movement
    {
        public FloatReference jumpTakeOffSpeed;
        public UnityEvent jumpEvent;

        public override Vector2 ComputeVelocity(bool underwater, bool exhausted, ref Vector2 velocity)
        {

            Vector2 move = Vector2.zero;
            move.x = Input.GetAxis("Horizontal");

            if (exhausted) return move;

            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = jumpTakeOffSpeed;
                jumpEvent.Invoke();
            }
            else if (Input.GetButtonUp("Jump"))
            {
                if (velocity.y > 0f) velocity.y = velocity.y * .5f;
            }

            return move;
        }

        private void ComputeYVelocity()
        {

        }

        public override void Initialize(GameObject obj)
        {
            base.Initialize(obj);
            Animator anim = ppc.GetComponent<Animator>();
            anim.SetBool("inWater", false);
            ppc.GetComponent<PlayerPlatformController>().jumpTakeOffSpeed = this.jumpTakeOffSpeed;

        }


    }
}

