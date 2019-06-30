using UnityEngine;
using UnityEngine.Events;
using cpioli.Variables;

public class Stamina : Resource {

    public IntReference strokesInABar;
    public FloatReference replenishmentTime;
    public FloatReference exhaustionDelay;
    public FloatReference strokeDelay;

    public UnityEvent exhaustionEvent;
    public UnityEvent startReplenishingEvent;

    private bool paused;
    private bool stoppingReplenishment;
    private float strokeCost;
    private float delayTimer;
    

	// Use this for initialization
	void Start () {
        stoppingReplenishment = false;
        currentValue = maxValue.Value;
        strokeCost = maxValue.Value / strokesInABar;
        paused = false;
        delayTimer = 0f;
	}

    // Update is called once per frame
    void Update()
    {
        if(paused) return;
        RestoreStamina(Time.deltaTime);
    }

    /*
     * Code to incrementally increase stamina over time once replenishment is available
     */
    private void RestoreStamina(float deltaTime)
    {
        if(delayTimer > 0f)
        {
            delayTimer -= deltaTime;
            if(delayTimer > 0f)
                return;
            delayTimer = 0f;
        }
        if (stoppingReplenishment) return;
        if(currentValue == minValue.Value)
        {
            BegunReplenishment();
        }
        if(currentValue < maxValue.Value)
        {
            currentValue += maxValue.Value * deltaTime / replenishmentTime;
        }

    }

    /*
     * Deducts an amount of stamina from the meter when a swim stroke is made
     */
    public void Stroke()
    {
        currentValue -= strokeCost;
        currentValue = Mathf.Clamp(currentValue, minValue.Value, maxValue.Value);
        if (currentValue == minValue.Value)
        {
            TriggerExhaustion();
        }
        delayTimer = strokeDelay.Value;
    }

    private void TriggerExhaustion()
    {
        delayTimer = exhaustionDelay.Value;
        exhaustionEvent.Invoke();
    }

    private void BegunReplenishment()
    {
        startReplenishingEvent.Invoke();
    }

    /*
     * riverbed walking cancels stamina restoration
     */
    public void SetStopReplenishment(bool stopping)
    {
        stoppingReplenishment = stopping;
    }

    public void SetPaused(bool paused)
    {
        this.paused = paused;
    }
}
