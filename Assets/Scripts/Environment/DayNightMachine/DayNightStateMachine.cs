using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightStateMachine : StateMachine
{

    

    #region States
    protected MorningState morningState = new MorningState();
    protected AfternoonState afternoonState = new AfternoonState();
    protected EveningState eveningState = new EveningState();
    protected NightState nightState = new NightState();
    #endregion States

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        if(currentState == null){
            return;
        }
        currentState.UpdateState(this);
    }

    public override void ChangeState(State state)
    {
        if(currentState != null){
            currentState.LeaveState(this);
        }
        currentState = state;
    }
}
