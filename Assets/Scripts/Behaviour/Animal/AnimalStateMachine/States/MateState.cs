using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class MateState : MoverState
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

    
    /// <summary>
    /// Overrided from base class.
    /// </summary>
    /// <param name="groundLayerMask"></param>
    /// <param name="oneTimeJumpDistance"></param>
    /// <param name="jumpHeight"></param>
    /// <param name="objectVerticalLength"></param>
    /// <param name="OnDestinationReached"></param>
    public override void initializeMoveSettings(LayerMask groundLayerMask, float oneTimeJumpDistance, float jumpHeight, float objectVerticalLength, Action OnDestinationReached)
    {

    }
}

