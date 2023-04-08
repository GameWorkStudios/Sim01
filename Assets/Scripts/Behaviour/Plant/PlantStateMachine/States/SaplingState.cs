using UnityEngine;

public class SaplingState : State
{

    private float stateDuration;

    public override void StartState(StateMachine machine)
    {
        this.stateDuration = machine.PlantStateMachine().PlantSettings.PlantSaplingDuration;
        machine.PlantStateMachine().SaplingObject.SetActive(true);
        machine.PlantStateMachine().PlantObject.SetActive(false);        
    }

    public override void UpdateState(StateMachine machine)
    {
        if(stateDuration < 0)
        {   
            machine.PlantStateMachine().ChangeState(machine.PlantStateMachine().growingState);
        }
        stateDuration -= Time.deltaTime;
    }

    public override void LeaveState(StateMachine machine)
    {
        machine.PlantStateMachine().SaplingObject.SetActive(false);
        machine.PlantStateMachine().PlantObject.SetActive(true);
    }
}
