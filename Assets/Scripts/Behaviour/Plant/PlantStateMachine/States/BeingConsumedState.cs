using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class BeingConsumedState : State
{

    private State lastStateOfBeforeConsume;

    public BeingConsumedState(State lastStateOfBeforeConsume){
        this.lastStateOfBeforeConsume = lastStateOfBeforeConsume;
    }

    public override void StartState(StateMachine machine)
    {
    }

    public override void UpdateState(StateMachine machine)
    {
    }

    public override void LeaveState(StateMachine machine)
    {
    }
}

