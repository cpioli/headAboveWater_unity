using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "StateSystem/Swimmer/Abovewater", order = 2)]
public class SwimmingAbovewaterState : PlayerSwimState
{
    public UnityEvent AbovewaterStrokeEvent;

    public override void OnStateEnter(PlayerPlatformController ppc)
    {
        base.OnStateEnter(ppc);
        ppc.animator.SetBool("inWater", true);
        Debug.Log("Player has entered the SwimmingAbovewaterState");
    }

    public override void OnStateExit(PlayerPlatformController ppc)
    {
        base.OnStateExit(ppc);
        Debug.Log("Player has exited the SwimmingAbovewaterState");
    }

    public override void ComputeVelocity(PlayerPlatformController ppc, ref Vector2 velocity)
    {
        base.ComputeVelocity(ppc, ref velocity);
        if (Input.GetButtonDown("Jump")) AbovewaterStrokeEvent.Invoke();
    }
}