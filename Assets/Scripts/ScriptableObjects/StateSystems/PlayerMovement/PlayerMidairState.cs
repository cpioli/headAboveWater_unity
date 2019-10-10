using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateSystem/Swimmer/Midair", order = 1)]
public class PlayerMidairState : PlayerMovementState
{
    public PlayerMovementState PlayerAbovewaterState;

    public override void OnStateEnter(PlayerPlatformController ppc)
    {
        base.OnStateEnter(ppc);
        Debug.Log("Program has entered PlayerMidairState!");
    }

    public override void OnStateExit(PlayerPlatformController ppc)
    {
        base.OnStateExit(ppc);
        Debug.Log("Program has exited PlayerMidairState!");
    }

    public override void ComputeVelocity(PlayerPlatformController ppc, ref Vector2 velocity)
    {
        if(ppc.FindCollision("water"))
        {
            ppc.SetState(PlayerAbovewaterState);
            return;
        }
        ppc.move = Vector2.zero;
        ppc.move.x = SwimmerInput.GetAxis("Horizontal");
    }
}