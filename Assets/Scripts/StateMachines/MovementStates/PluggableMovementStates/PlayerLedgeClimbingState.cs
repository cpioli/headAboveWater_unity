using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cpioli.Events;

[CreateAssetMenu (menuName = "StateSystem/Swimmer/ClimbingLedgeState", order = 6)]
public class PlayerLedgeClimbingState : PlayerMovementState {

    private float timePassed, step1Duration, step2Duration, t, n; //time tracking variables for lerping
    private float step1YPos, step2XPos; //the final positions to lerp to
    private Vector3 ledgePosition, currentPlayerPos;

    public float stateDuration = 0.5f; //length of animation duration
    public GameEvent StrokeEvent;
    public GameEvent UnderwaterStrokeEvent;
    public PlayerMovementState UnderwaterSwimState;
    public PlayerMovementState GroundedState;

    public override void OnStateEnter(PlayerPlatformController ppc)
    {
        base.OnStateEnter(ppc);
        ppc.animator.SetTrigger("climb");
        timePassed = 0.0f;
        step1Duration = stateDuration * 0.33f;
        step2Duration = stateDuration - step1Duration;

        currentPlayerPos = ppc.transform.position;
        ledgePosition = ppc.gameObject.transform.position;
        step1YPos = currentPlayerPos.y + 0.5f;
        step2XPos = currentPlayerPos.x + 0.5f;
        Debug.Log("Entered the Climbing State");
    }

    public override void OnStateExit(PlayerPlatformController ppc)
    {
        base.OnStateExit(ppc);
        Debug.Log("Exited the Climbing State");
    }

    public override void ComputeVelocity(PlayerPlatformController ppc, ref Vector2 velocity)
    {
        /*ppc.move = Vector2.zero;
        ppc.move.x = Input.GetAxis("Horizontal");
        if (ppc.exhausted) return;*/
        /*if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jumping!");
            velocity.y = ppc.jumpTakeOffSpeed;
            //ppc.move.y = ppc.jumpTakeOffSpeed;
            ppc.animator.SetTrigger("strokePerformed");
            StrokeEvent.Raise();
        }

        if (Vector3.Distance(ppc.transform.position, ledgePosition) > 1.50f) {
            ppc.SetState(UnderwaterSwimState);
        }*/

        timePassed += Time.deltaTime;
        if(timePassed > stateDuration)
        {
            ppc.SetState(GroundedState);
            return;
        } else if(timePassed < step1Duration)
        {
            t = timePassed / step1Duration;
            currentPlayerPos.y = Mathf.Lerp(ppc.transform.position.y, step1YPos, t);
            ppc.transform.position = currentPlayerPos;
        } else if(timePassed < step2Duration)
        {
            t = (timePassed - step1Duration);
            t /= step2Duration;
            currentPlayerPos.x = Mathf.Lerp(ppc.transform.position.x, step2XPos, t);
            ppc.transform.position = currentPlayerPos;
        }

    }
}
