using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parent class of StateMachine tracking the state of the game for the player.
/// It's used mostly for the OnStateEnter and OnStateExit methods, but a
/// default "Run" method will be offered if a GameState needs a place to handle
/// non-gameplay input.
/// </summary>
public abstract class GameState : MonoBehaviour {

    protected GameManager gm;

    /// <summary>
    /// build the items needed for this state
    /// </summary>
	public virtual void OnStateEnter(GameManager gm)
    {
        this.gm = gm;
    }

    /// <summary>
    /// teardown the state before moving onto the next state
    /// </summary>
    public virtual void OnStateExit()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public virtual void Run()
    {

    }
}
