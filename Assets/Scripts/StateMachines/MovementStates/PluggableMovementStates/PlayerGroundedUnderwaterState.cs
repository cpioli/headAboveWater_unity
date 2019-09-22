using UnityEngine;
using cpioli.Events;

[CreateAssetMenu (menuName ="StateSystem/Swimmer/GroundedUnderwater", order = 4)]
public class PlayerGroundedUnderwaterState : PlayerMovementState {

    private bool isWalking;

    public GameEvent WalkingOnRiverbedEvent;
    public GameEvent StillOnRiverbedEvent;

    public PlayerMovementState playerUnderwaterState;

    public override void OnStateEnter(PlayerPlatformController ppc)
    {
        base.OnStateEnter(ppc);
        ppc.animator.SetBool("grounded", true);
        Debug.Log("Player has entered the grounded state");
        isWalking = false;
    }

    public override void OnStateExit(PlayerPlatformController ppc)
    {
        base.OnStateExit(ppc);
        StillOnRiverbedEvent.Raise();
        Debug.Log("Player has exited the grounded state");
    }
    public override void ComputeVelocity(PlayerPlatformController ppc, ref Vector2 velocity)
    {
        ppc.move = Vector2.zero;
        ppc.move.x = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = ppc.jumpTakeOffSpeed;
            ppc.SetState(playerUnderwaterState);
            
        } else if(velocity.y < -0.01f) //if we've just walked off a ledge
        {
            ppc.SetState(playerUnderwaterState);
        }

        if (!isWalking && Mathf.Abs(ppc.move.x) > 0.01f)
        {
            Debug.Log("Broadcasting Walking-OnRiverbed Event!");
            WalkingOnRiverbedEvent.Raise();
            isWalking = !isWalking;
        } else if(isWalking && Mathf.Abs(ppc.move.x) < 0.01f)
        {
            Debug.Log("Broadcasting Standing Still on Riverbed Event!");
            StillOnRiverbedEvent.Raise();
            isWalking = !isWalking;
        }
    }
}
