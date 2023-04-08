using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrownState : State
{

    private float lifeTimeDuration;
    private float consumableCreationDuration;
    private float elapsedTime;

    public override void StartState(StateMachine machine)
    {
        this.lifeTimeDuration = machine.PlantStateMachine().PlantSettings.PlantLifeTimeDuration;
        this.consumableCreationDuration = machine.PlantStateMachine().PlantSettings.ConsumableCreationDuration;
    }

    public override void UpdateState(StateMachine machine)
    {        
        elapsedTime += Time.deltaTime;
        if(elapsedTime >= lifeTimeDuration){
            machine.PlantStateMachine().ChangeState(machine.PlantStateMachine().saplingState);            
        }
        if(this.elapsedTime >= this.consumableCreationDuration){
            machine.PlantStateMachine().CreateNutrient();
            this.consumableCreationDuration += machine.PlantStateMachine().PlantSettings.ConsumableCreationDuration;     
        }
    }

    public override void LeaveState(StateMachine machine)
    {
        elapsedTime = 0;
        machine.PlantStateMachine().transform.localScale = new Vector3(2,2,2);
        machine.PlantStateMachine().Die();
    }
}
