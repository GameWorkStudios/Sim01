using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This state is representing plant's growing status. 
/// In this state plant can produce (or increase) nutrient on plant.
/// </summary>
public class GrowingState : State
{
    private float growingDuration;
    private float consumableCreationDuration;
    private float elapsedTime = 0;
    private Transform plantObjectTransform; 
    private Vector3 startScale;    

    public override void StartState(StateMachine machine)
    {
        this.growingDuration = machine.PlantStateMachine().PlantSettings.PlantyGrowingDuration;
        this.consumableCreationDuration = machine.PlantStateMachine().PlantSettings.ConsumableCreationDuration;
        this.plantObjectTransform = machine.PlantStateMachine().PlantObject.transform;
        this.startScale = new Vector3(2,2,2);
        this.plantObjectTransform.localScale = this.startScale;
    }

    public override void UpdateState(StateMachine machine)
    {
        elapsedTime += Time.deltaTime;
        Vector3 lerped = Vector3.Lerp(this.startScale, new Vector3(5,5,5), elapsedTime / growingDuration);
        this.plantObjectTransform.localScale = lerped;
        if(this.elapsedTime >= this.growingDuration){
            machine.PlantStateMachine().ChangeState(machine.PlantStateMachine().grownState);
        }
        if(this.elapsedTime >= this.consumableCreationDuration){
            machine.PlantStateMachine().CreateNutrient();
            this.consumableCreationDuration += machine.PlantStateMachine().PlantSettings.ConsumableCreationDuration;
        }
    }

    public override void LeaveState(StateMachine machine)
    {
        elapsedTime = 0;        
    }
}
