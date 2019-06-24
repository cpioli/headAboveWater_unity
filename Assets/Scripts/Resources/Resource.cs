using UnityEngine;
using cpioli.Variables;

/// <summary>
/// An object representing a resource with a quantifiable numeric value
/// Has two UnityEvents for "Full" and "Empty"
/// and is a container of rulesets that decrease or increase its value.
/// Must be placed onto a GameObject w/ EventListener scripts for values to update
/// </summary>
public abstract class Resource : MonoBehaviour {

    public FloatReference minValue;
    public FloatReference maxValue;

    protected float currentValue;

    /// <summary>
    /// tells the corresponding meter to retrieve the current value
    /// </summary>
    /// <returns>
    /// the current amount of the resource
    /// </returns>
    public float GetCurrentValue()
    {
        return currentValue;
    }
}
