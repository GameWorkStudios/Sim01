using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// In this state two animals start for mate. 
/// with this state just stoping and waiting after the successfull mating process
/// we can pop new animal(s).
/// Population process is running on female side because of birth phenomenon is on the
/// female side.
/// Male side is only transfering genes.
/// TODO : Important!
/// </summary>
public class MatingState : State
{
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

