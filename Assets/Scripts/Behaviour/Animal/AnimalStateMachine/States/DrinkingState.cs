using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class DrinkingState : State
{

    private AnimalStateMachine animalStateMachine;
    private const float DRINK_DURATION = 1.5f;
    private float elapsedTime = 0; 


    public override void StartState(StateMachine machine)
    {
        Debug.Log("DRINKING STATE");
        animalStateMachine = machine.AnimalStateMachine();
        animalStateMachine.SetIdentifier(StateIdentifier.DRINKING);
    }

    public override void UpdateState(StateMachine machine)
    {
        if(elapsedTime >= DRINK_DURATION){
            animalStateMachine.Drink();
        }
        elapsedTime += Time.deltaTime;
    }

    public override void LeaveState(StateMachine machine)
    {
        elapsedTime = 0;
    }
}

