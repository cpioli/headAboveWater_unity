using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimatorUpdate : ScriptableObject {

    public string parameterName;
    public abstract void UpdateAnimator(PlayerPlatformController ppc);
}

[CreateAssetMenu(menuName = "Instructions/UpdateAnimator/Bool",order = 0)]
public class UpdateAnimatorBool : AnimatorUpdate
{
    public bool parameterValue;
    public override void UpdateAnimator(PlayerPlatformController ppc) 
    {
        ppc.animator.SetBool(parameterName, parameterValue);
    }
}

[CreateAssetMenu(menuName = "Instructions/UpdateAnimator/Float", order = 1)]
public class UpdateAnimatorFloat : AnimatorUpdate
{
    public float parameterValue;
    public override void UpdateAnimator(PlayerPlatformController ppc)
    {
        ppc.animator.SetFloat(parameterName, parameterValue);
    }
}

[CreateAssetMenu(menuName = "Instructions/UpdateAnimator/Integer", order = 2)]
public class UpdateAnimatorInteger : AnimatorUpdate
{
    public int parameterValue;
    public override void UpdateAnimator(PlayerPlatformController ppc)
    {
        ppc.animator.SetInteger(parameterName, parameterValue);
    }
}

[CreateAssetMenu(menuName = "Instructions/UpdateAnimator/Trigger", order = 3)]
public class UpdateAnimatorTrigger : AnimatorUpdate
{
    public override void UpdateAnimator(PlayerPlatformController ppc)
    {
        ppc.animator.SetTrigger(parameterName);
    }
}
