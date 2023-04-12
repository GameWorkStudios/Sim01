using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public abstract class ProgressState : State
{

    FloatReference tirednessProgress;
    FloatReference thirstProgress;
    FloatReference hungerProgress;
    FloatReference mateProgress;

    /*
    public override void StartState(StateMachine machine)
    {
    }

    public override void UpdateState(StateMachine machine)
    {
    }

    public override void LeaveState(StateMachine machine)
    {
    }*/
}

