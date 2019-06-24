using UnityEngine;
using UnityEngine.Events;
using cpioli.Variables;

public class Oxygen : Resource {

    public FloatReference consumptionTime; //time in seconds to consume a full bar of Oxygen
    public FloatReference replenishmentTime; //time in seconds to replenish a full bar of Oxygen

    public UnityEvent emptyEvent;

    private bool submerged;

    private void Start()
    {
        currentValue = maxValue.Value;
        submerged = false;
    }

    // Update is called once per frame
    void Update () {
        if (submerged)
        {
            currentValue -= maxValue.Value * Time.deltaTime / consumptionTime;
        }
        else
        {
            currentValue += maxValue.Value * Time.deltaTime / replenishmentTime;
        }

        currentValue = Mathf.Clamp(currentValue, minValue.Value, maxValue.Value);
        if (currentValue == minValue.Value)
        {
            emptyEvent.Invoke();
        }
    }

    public void SetSwimmerSubmerged(bool submerged)
    {
        this.submerged = submerged;
    }
}
